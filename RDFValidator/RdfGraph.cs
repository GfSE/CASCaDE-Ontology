// SPDX-FileCopyrightText: 2026 prostep ivip Association
// SPDX-FileCopyrightText: 2026 Michael Kirsch <michael.kirsch@em.ag>

using Microsoft.Win32;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace CascaraValidator;

/// <summary>
/// Handles an RDF graph including the UI controls to select and update it
/// </summary>
public class RdfGraph
{
    private const string _rdfFileSelectionFilter =
        "RDF files (*.ttl;*.rdf;*.xml;*.json;*.jsonld)|*.ttl;*.rdf;*.xml;*.json;*.jsonld|" +
        "Turtle (*.ttl)|*.ttl|" +
        "RDF/XML (*.rdf;*.xml)|*.rdf;*.xml|" +
        "JSON-LD (*.json;*.jsonld)|*.json;*.jsonld";

    private TurtleParser _ttlParser = new TurtleParser();
    private RdfXmlParser _rdfXmlParser = new RdfXmlParser();
    private JsonLdParser _jsonLdParser = new JsonLdParser();

    private string _graphFilePath;
    private string _graphGraphKey;
    private RegistryKey _registryGroupKey;
    private TextBox _filePathTextBox;
    private Button _fileSelectionButton;

    public Graph Graph { get; private set; }

    public RdfGraph(string graphKey, RegistryKey registryGroupKey, TextBox filePathTextBox, Button fileSelectionButton)
    {
        _graphGraphKey = graphKey;
        _registryGroupKey = registryGroupKey;
        _filePathTextBox = filePathTextBox;
        _fileSelectionButton = fileSelectionButton;

        ReadFilePathFromRegistry();

        _fileSelectionButton.Click += FileSelectionButton_Click;
    }

    // Selects the graph from file system
    private void FileSelectionButton_Click(object sender, EventArgs e)
    {
        LoadGraphFromSelectedFile();
    }

    // Reads file path from registry
    private void ReadFilePathFromRegistry()
    {
        string graphFilePath = _registryGroupKey.GetValue(_graphGraphKey)?.ToString();
        LoadGraphFromFilePath(graphFilePath);
    }

    // Selects file path using file open dialog
    private void LoadGraphFromSelectedFile()
    {
        string graphFilePath = GetFilePathByDialog("Select " + _graphGraphKey.ToLower() + " ontology file", _rdfFileSelectionFilter, Path.GetDirectoryName(_graphFilePath));
        LoadGraphFromFilePath(graphFilePath);
    }

    private void LoadGraphFromFilePath(string graphFilePath)
    {
        if (File.Exists(graphFilePath))
        {
            // Reset graph and try parse as Turtle
            Graph = new Graph();
            try
            {
                _ttlParser.Load(Graph, graphFilePath);
                Controller.Logger.Debug(_graphGraphKey + " graph loaded from Turtle file " + graphFilePath);
            }
            catch(RdfParseException turtleParseException)
            {
                // Reset graph and try parse as RDF XML
                Graph.Clear();
                try
                {
                    _rdfXmlParser.Load(Graph, graphFilePath);
                    Controller.Logger.Debug(_graphGraphKey + " graph loaded from RDF XML file " + graphFilePath);
                }
                catch(RdfParseException rdfXmlParseException)
                {
                    // Reset graph and try parse as JSON-LD
                    Graph.Clear();
                    try
                    {
                        ITripleStore tripleStore = new TripleStore();
                        tripleStore.Add(Graph);

                        _jsonLdParser.Load(tripleStore, graphFilePath);
                        Controller.Logger.Debug(_graphGraphKey + " graph loaded from JSON-LD file " + graphFilePath);
                    }

                    // Set graph 
                    catch(RdfParseException jsonLdParseException)
                    {
                        Graph = null;
                        Controller.Logger.Debug("Unable to load " + _graphGraphKey + " graph from file " + graphFilePath);
                    }
                }
            }
        }

        if (Graph != null)
        {
            _graphFilePath = graphFilePath;
            _filePathTextBox.Text = _graphFilePath;
            _registryGroupKey.SetValue(_graphGraphKey, _graphFilePath);
        }
    }

    // Returns a file path using file selection dialog
    public static string GetFilePathByDialog(string title, string filter, string defaultFolder = null)
    {
        // Create file dialog
        FileDialog fileDialog = new OpenFileDialog();

        // Define dialog title
        fileDialog.Title = title;

        // Define file type filter
        fileDialog.Filter = filter;

        // Initial folder if defined
        if (defaultFolder != null && File.Exists(defaultFolder))
        {
            fileDialog.InitialDirectory = defaultFolder;
        }

        // Open dialog
        fileDialog.ShowDialog();

        return fileDialog.FileName;
    }
}