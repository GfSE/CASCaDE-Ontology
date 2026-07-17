// SPDX-FileCopyrightText: 2026 prostep ivip Association
// SPDX-FileCopyrightText: 2026 Michael Kirsch <michael.kirsch@em.ag>

using Microsoft.Win32;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace CascaraRdfValidator;

/// <summary>
/// Handles a source file with transformation
/// </summary>
public class XmlSource
{
    private const string _xmlFileSelectionFilter = "XML files (*.xml;*.reqif;*.sysml;*.qif;*.stpx)|*.xml;*.reqif;*.sysml;*.qif;*.stpx";
    private const string _xsltFileSelectionFilter = "XSLT files (*.xsl;*.xslt)|*.xsl;*.xslt";

    private TableLayoutPanel _sourceTableLayoutPanel;

    private ResourceSelection _sourceSelection;
    private ResourceSelection _transformationSelection;

    private XDocument _sourceDocument;
    private XDocument _transformedDocument;

    public RdfGraph Graph { get; init; }

    public XmlSource(int index, RegistryKey registryGroupKey, TableLayoutPanel parentPanel, int row, int column)
    {
        _sourceTableLayoutPanel = new TableLayoutPanel();
        _sourceTableLayoutPanel.ColumnCount = 1;
        _sourceTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        _sourceTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        parentPanel.Controls.Add(_sourceTableLayoutPanel, column, row);

        _sourceSelection = new ResourceSelection("Source" + index, registryGroupKey, _xmlFileSelectionFilter, _sourceTableLayoutPanel, 0, 0);
        _transformationSelection = new ResourceSelection("Transformation" + index, registryGroupKey, _xsltFileSelectionFilter, _sourceTableLayoutPanel, 0, 1);

        if (!string.IsNullOrEmpty(_sourceSelection.FilePath))
        {
            _sourceDocument = XDocument.Load(_sourceSelection.FilePath);
        }

        if (!string.IsNullOrEmpty(_transformationSelection.FilePath))
        {
            using (XmlReader xmlReader = _sourceDocument.CreateReader())
            {
                XslCompiledTransform xsltCompiledTransform = new XslCompiledTransform();
                XsltSettings xsltSettings = new XsltSettings(enableDocumentFunction: true, enableScript: false);
                xsltCompiledTransform.Load(_transformationSelection.FilePath, xsltSettings, new XmlUrlResolver());

                _transformedDocument = new XDocument();
                using (XmlWriter xmlWriter = _transformedDocument.CreateWriter())
                {
                    xsltCompiledTransform.Transform(xmlReader, xmlWriter);
                }

                Graph = new RdfGraph(_transformedDocument);
            }
        }
    }
}
