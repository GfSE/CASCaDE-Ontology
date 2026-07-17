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
        string registryGroupKeyPath = "SOFTWARE\\" + Application.ProductName + "\\GraphFilePath";
        _registryGroupKey = Registry.CurrentUser.OpenSubKey(registryGroupKeyPath, true);
        if (_registryGroupKey == null)
        {
            _registryGroupKey = Registry.CurrentUser.CreateSubKey(registryGroupKeyPath, true);
        }

        // Initialize source and transformation
        int index = 0;
        XmlSource source = null;
        do
        {
            source = new XmlSource(1, _registryGroupKey, _dialog.MainPanel, 0, 1);
            _sources.Add(source);
        } while (source.Graph != null);

        // Initialize RDF graphs
        _individualsGraph = new RdfGraph(new ResourceSelection("Individuals", _registryGroupKey, _rdfFileSelectionFilter, _dialog.MainPanel, 1, 1));
        _ontologyGraph = new RdfGraph(new ResourceSelection("Ontology", _registryGroupKey, _rdfFileSelectionFilter, _dialog.MainPanel, 2, 1), true);
        _metamodelGraph = new RdfGraph(new ResourceSelection("Metamodel", _registryGroupKey, _rdfFileSelectionFilter, _dialog.MainPanel, 3, 1));

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