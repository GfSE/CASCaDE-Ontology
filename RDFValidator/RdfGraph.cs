// SPDX-FileCopyrightText: 2026 prostep ivip Association
// SPDX-FileCopyrightText: 2026 Michael Kirsch <michael.kirsch@em.ag>
// SPDX-FileCopyrightText: 2026 René Bielert

using System.Xml.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace CascaraRdfValidator;

/// <summary>
/// Handles an RDF graph including the UI controls to select and update it
/// </summary>
public class RdfGraph
{
    private TurtleParser _ttlParser = new TurtleParser();
    private RdfXmlParser _rdfXmlParser = new RdfXmlParser();
    private JsonLdParser _jsonLdParser = new JsonLdParser();

    private bool _instantiateGraph;
    private FileSelection _fileSelection;

    public event EventHandler<EventArgs> GraphAssigned;

    public Graph Graph { get; private set; }

    public RdfGraph(XDocument rdfXmlDocument)
    {
        Graph = new Graph();
        using (StringReader stringReader = new StringReader(rdfXmlDocument.ToString()))
        {
            _rdfXmlParser.Load(Graph, stringReader);
        }
    }

    public RdfGraph(FileSelection fileSelection, bool instantiateGraph = false)
    {
        _instantiateGraph = instantiateGraph;
        _fileSelection = fileSelection;
        _fileSelection.FileSelected += FileSelection_FileSelected;

        if (File.Exists(_fileSelection.FilePath))
        {
            FileSelection_FileSelected(_fileSelection, new EventArgs());
        }
    }

    // Handles the file selected event
    private void FileSelection_FileSelected(object source, EventArgs e)
    {
        // Reset graph and try parse as Turtle
        Graph = new Graph();
        try
        {
            _ttlParser.Load(Graph, _fileSelection.FilePath);
            Controller.Logger.Debug("Graph loaded from Turtle file " + _fileSelection.FilePath);
        }
        catch (RdfParseException turtleParseException)
        {
            // Reset graph and try parse as RDF XML
            Graph.Clear();
            try
            {
                _rdfXmlParser.Load(Graph, _fileSelection.FilePath);
                Controller.Logger.Debug("Graph loaded from RDF XML file " + _fileSelection.FilePath);
            }
            catch (RdfParseException rdfXmlParseException)
            {
                // Reset graph and try parse as JSON-LD
                Graph.Clear();
                try
                {
                    ITripleStore tripleStore = new TripleStore();
                    tripleStore.Add(Graph);

                    _jsonLdParser.Load(tripleStore, _fileSelection.FilePath);
                    Controller.Logger.Debug("Graph loaded from JSON-LD file " + _fileSelection.FilePath);
                }

                // Set graph 
                catch (RdfParseException jsonLdParseException)
                {
                    Graph = null;
                    Controller.Logger.Debug("Unable to load graph from file " + _fileSelection.FilePath);
                }
            }
        }

        // Continue if graph has been parsed
        if (Graph != null)
        {
            // Instantiate graph
            if (_instantiateGraph)
            {
                InstantiateGraph();
            }

            GraphAssigned?.Invoke(this, EventArgs.Empty);
        }
    }

    // Instantiates the reference graph
    private void InstantiateGraph()
    {
        IUriNode typeUriNode = Graph.CreateUriNode(new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#type"));
        IUriNode subClassOfUriNode = Graph.CreateUriNode(new Uri("http://www.w3.org/2000/01/rdf-schema#subClassOf"));

        // Traverse all subClassOf triples and assert rdf:type tripe
        List<Triple> subClassOfTriples = Graph.GetTriplesWithPredicate(subClassOfUriNode).ToList();
        foreach (Triple subClassOfTriple in subClassOfTriples)
        {
            Graph.Assert(subClassOfTriple.Subject, typeUriNode, subClassOfTriple.Object);
        }

        //
        bool graphModified = true;
        while (graphModified)
        {
            // Traverse type triples
            graphModified = false;
            List<Triple> typeTriples = Graph.GetTriplesWithPredicate(typeUriNode).ToList();
            foreach (Triple typeTriple in typeTriples)
            {
                // If X rdf:type Y and Y rdfs:subClassOf Z, then X rdf:type Z
                foreach (Triple subClassTriple in Graph.GetTriplesWithSubjectPredicate(typeTriple.Object, subClassOfUriNode))
                {
                    if (!Graph.ContainsTriple(new Triple(typeTriple.Subject, typeUriNode, subClassTriple.Object)))
                    {
                        Graph.Assert(typeTriple.Subject, typeUriNode, subClassTriple.Object);
                        graphModified = true;
                    }
                }
            }
        }
    }
}