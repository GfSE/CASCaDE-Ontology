// SPDX-FileCopyrightText: 2026 prostep ivip Association
// SPDX-FileCopyrightText: 2026 Michael Kirsch <michael.kirsch@em.ag>

using Microsoft.VisualBasic.Logging;
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

    private FileSelection _sourceFileSelection;
    private FileSelection _transformationFileSelection;
    private Button _removeButton;

    private XDocument _sourceDocument;
    private XslCompiledTransform _compiledTransform;

    public RdfGraph Graph { get; private set; }

    public event EventHandler<EventArgs> GraphLoaded;
    public event EventHandler<EventArgs> SourceRemoved;

    public XmlSource(int index, RegistryKey registryGroupKey, TableLayoutPanel parentPanel, int row, int column)
    {
        // Create a table layout panel for this source
        _sourceTableLayoutPanel = new TableLayoutPanel();
        _sourceTableLayoutPanel.Height = 28;
        _sourceTableLayoutPanel.Dock = DockStyle.Fill;
        _sourceTableLayoutPanel.Margin = new Padding(0);
        _sourceTableLayoutPanel.ColumnCount = 3;
        _sourceTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        _sourceTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        _sourceTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
        parentPanel.Controls.Add(_sourceTableLayoutPanel, column, row);

        // Create remove button
        _removeButton = new Button();
        _removeButton.Text = "X";
        _removeButton.TextAlign = ContentAlignment.MiddleCenter;
        _removeButton.FlatStyle = FlatStyle.Flat;
        _removeButton.FlatAppearance.BorderSize = 0;
        _removeButton.BackColor = SystemColors.Control;
        _removeButton.Dock = DockStyle.Fill;
        _removeButton.Click += RemoveButton_Click;
        _removeButton.Visible = false;
        _sourceTableLayoutPanel.Controls.Add(_removeButton, 2, 0);

        // Create and assign the XML source selection
        _sourceFileSelection = new FileSelection("Source" + index, registryGroupKey, _xmlFileSelectionFilter, _sourceTableLayoutPanel, 0, 0);
        _sourceFileSelection.FileSelected += SourceFileSelected;

        if (File.Exists(_sourceFileSelection.FilePath))
        {
            SourceFileSelected(_sourceFileSelection, new EventArgs());
        }

        // Create and assign the XML stylesheet source selection
        _transformationFileSelection = new FileSelection("Transformation" + index, registryGroupKey, _xsltFileSelectionFilter, _sourceTableLayoutPanel, 0, 1);
        _sourceFileSelection.FileSelected += SourceFileSelected;

        if (File.Exists(_transformationFileSelection.FilePath))
        {
            TransformationSelected(_transformationFileSelection.FilePath, new EventArgs());
        }
    }

    // Loads source document after file selected
    private void SourceFileSelected(object sender, EventArgs e)
    {
        _sourceDocument = XDocument.Load(_sourceFileSelection.FilePath);

        LoadGraph();
    }

    // Load transformation after file selected
    private void TransformationSelected(object sender, EventArgs e)
    {
        _compiledTransform = new XslCompiledTransform();
        XsltSettings xsltSettings = new XsltSettings(enableDocumentFunction: true, enableScript: false);
        _compiledTransform.Load(_transformationFileSelection.FilePath, xsltSettings, new XmlUrlResolver());

        LoadGraph();
    }

    // Removes the source
    private void RemoveButton_Click(object sender, EventArgs e)
    {
        SourceRemoved?.Invoke(this, EventArgs.Empty);
    }

    // Transforms source and loads graph
    private void LoadGraph()
    {
        Graph = null;
        if (_sourceDocument != null && _compiledTransform != null)
        {
            using (XmlReader xmlReader = _sourceDocument.CreateReader())
            {
                XDocument transformedDocument = new XDocument();
                using (XmlWriter xmlWriter = transformedDocument.CreateWriter())
                {
                    try
                    {
                        _compiledTransform.Transform(xmlReader, xmlWriter);

                        Graph = new RdfGraph(transformedDocument);

                        _removeButton.Visible = true;
                        GraphLoaded?.Invoke(this, EventArgs.Empty);
                    }
                    catch
                    {
                        
                    }
                }
            }
        }
        else
        {
            _removeButton.Visible = false;
        }
    }
}
