// SPDX-FileCopyrightText: 2026 prostep ivip Association
// SPDX-FileCopyrightText: 2026 Michael Kirsch <michael.kirsch@em.ag>

using Microsoft.Win32;
using NLog;

namespace CascaraRdfValidator;

/// <summary>
/// Static main class of the CASCaRA RDF validator
/// </summary>
public static class Controller
{
    private const string _registryKey = "FilePaths";
    private const string _rdfFileSelectionFilter =
        "RDF files (*.ttl;*.rdf;*.xml;*.json;*.jsonld)|*.ttl;*.rdf;*.xml;*.json;*.jsonld|" +
        "Turtle (*.ttl)|*.ttl|" +
        "RDF/XML (*.rdf;*.xml)|*.rdf;*.xml|" +
        "JSON-LD (*.json;*.jsonld)|*.json;*.jsonld";

    private static RegistryKey _registryGroupKey;
    private static ControllerDialog _dialog = new ControllerDialog();

    private static List<XmlSource> _sources = [];

    private static RdfGraph _individualsGraph;
    private static RdfGraph _ontologyGraph;
    private static RdfGraph _metamodelGraph;

    private static RdfValidation _individualsValidation;
    private static RdfValidation _ontologyValidation;

    public static Logger Logger = LogManager.GetCurrentClassLogger();

    // Handles the graph loaded event
    public static void GraphLoaded(object sender, EventArgs e)
    {

    }

    // Handles the source removed event
    public static void SourceRemoved(object sender, EventArgs e)
    {

    }

    // Handles the analyze button click event
    private static void AnalyzeButton_Click(object sender, EventArgs e)
    {
        // Analyze individuals
        _individualsValidation.Analyze();

        // Analyze ontology
        _ontologyValidation.Analyze();
    }

    // Handles the validation button click event
    private static void ValidateButton_Click(object sender, EventArgs e)
    {
        // Validate individuals
        _individualsValidation.Validate();

        // Validate ontology
        _ontologyValidation.Validate();
    }

    // Main function for the CASCaRA validator
    [STAThread]
    public static void Main()
    {
        // Initialize logger
        LogManager.Configuration.Variables["productName"] = Application.ProductName;

        // Try assign dialog values from registry
        string registryGroupKeyPath = "SOFTWARE\\" + Application.ProductName + "\\" + _registryKey;
        _registryGroupKey = Registry.CurrentUser.OpenSubKey(registryGroupKeyPath, true);
        if (_registryGroupKey == null)
        {
            _registryGroupKey = Registry.CurrentUser.CreateSubKey(registryGroupKeyPath, true);
        }

        // Try initialize sources
        XmlSource source = null;
        do
        {
            source = new XmlSource(_sources.Count, _registryGroupKey, _dialog.SourcesPanel, _sources.Count, 0);
            source.GraphLoaded += GraphLoaded;
            _sources.Add(source);

            if (source.Graph != null)
            {
                _dialog.SourcesPanel.RowCount = _sources.Count;
                _dialog.SourcesPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            }

        } while (source.Graph != null);

        // Initialize RDF individuals graphs
        _individualsGraph = new RdfGraph(new FileSelection("Individuals", _registryGroupKey, _rdfFileSelectionFilter, _dialog.MainPanel, 1, 1));

        // Initialize RDF ontology graph
        _ontologyGraph = new RdfGraph(new FileSelection("Ontology", _registryGroupKey, _rdfFileSelectionFilter, _dialog.MainPanel, 2, 1), true);

        // Initialize RDF metamodel graph
        _metamodelGraph = new RdfGraph(new FileSelection("Metamodel", _registryGroupKey, _rdfFileSelectionFilter, _dialog.MainPanel, 3, 1));

        // Initialize RDF validations
        _individualsValidation = new RdfValidation(_individualsGraph, _ontologyGraph, _dialog.IndividualsValidationTreeView);
        _ontologyValidation = new RdfValidation(_ontologyGraph, _metamodelGraph, _dialog.OntologyValidationTreeView);

        // Assign dialog behavior
        _dialog.AnalyzeButton.Click += AnalyzeButton_Click;
        new ToolTip().SetToolTip(_dialog.AnalyzeButton, "Shows all shapes and align the subject nodes");
        _dialog.ValidateButton.Click += ValidateButton_Click;
        new ToolTip().SetToolTip(_dialog.ValidateButton, "Validates graph against shapes graph");

        // Show dialog
        _dialog.ShowDialog();
    }
}