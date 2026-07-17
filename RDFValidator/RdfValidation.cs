// SPDX-FileCopyrightText: 2026 prostep ivip Association
// SPDX-FileCopyrightText: 2026 Michael Kirsch <michael.kirsch@em.ag>
// SPDX-FileCopyrightText: 2026 René Bielert

using VDS.RDF;
using VDS.RDF.Shacl;
using VDS.RDF.Shacl.Validation;

namespace CascaraRdfValidator;

/// <summary>
/// Handles the validation of an RDF graph against a SHACL shapes graph
/// </summary>
public class RdfValidation
{
    private RdfGraph _graphToValidate;
    private RdfGraph _referenceGraph;
    private TreeView _resultsTreeView;

    public RdfValidation(RdfGraph graphToValidate, RdfGraph referenceGraph, TreeView resultsTreeView)
    {
        _graphToValidate = graphToValidate;
        _referenceGraph = referenceGraph;

        _resultsTreeView = resultsTreeView;
    }

    // Analyze applicable nodes
    public void Analyze()
    {
        _resultsTreeView.Nodes.Clear();

        if (_graphToValidate.Graph != null && _referenceGraph.Graph != null)
        {
            _resultsTreeView.Visible = false;

            // Get shapes from reference and validate graph
            ShapesGraph referenceShapesGraph = new ShapesGraph(_referenceGraph.Graph);

            List<IUriNode> shaclPredicateNodes = new List<IUriNode>
            {
                referenceShapesGraph.CreateUriNode(new Uri("http://www.w3.org/ns/shacl#path")),
                referenceShapesGraph.CreateUriNode(new Uri("http://www.w3.org/ns/shacl#targetClass"))
            };

            IUriNode typeUriNode = _graphToValidate.Graph.CreateUriNode(new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#type"));
            IUriNode labelUriNode = _graphToValidate.Graph.CreateUriNode(new Uri("http://www.w3.org/2000/01/rdf-schema#label"));

            foreach (IUriNode shaclPredicateNode in shaclPredicateNodes)
            {
                foreach (Triple shapeTriple in referenceShapesGraph.GetTriplesWithPredicate(shaclPredicateNode))
                {
                    if (shapeTriple.Subject is IUriNode shapeUriNode)
                    {
                        if (shapeTriple.Object is IUriNode targetUriNode)
                        {
                            TreeNode shapeTreeNode = _resultsTreeView.Nodes.Add(GetResolveNodeValue(referenceShapesGraph, shapeUriNode) + " > " + GetResolveNodeValue(referenceShapesGraph, targetUriNode));
                            foreach (Triple targetNodeTriple in _graphToValidate.Graph.GetTriplesWithPredicateObject(typeUriNode, shapeTriple.Object))
                            {
                                if (targetNodeTriple.Subject is IUriNode targetNodeUriNode)
                                {
                                    List<string> labels = [];
                                    foreach (Triple targetNodeLabelTriple in _graphToValidate.Graph.GetTriplesWithSubjectPredicate(targetNodeUriNode, labelUriNode))
                                    {
                                        if (targetNodeLabelTriple.Object is LiteralNode labelLiteralNode)
                                        {
                                            labels.Add(GetResolveNodeValue(_graphToValidate.Graph, labelLiteralNode));
                                        }
                                    }
                                    string nodeLabel = GetResolveNodeValue(_graphToValidate.Graph, targetNodeUriNode);
                                    if (labels.Count > 0)
                                    {
                                        nodeLabel = string.Join(", ", labels.ToArray()) + " (" + nodeLabel + ")";
                                    }
                                    shapeTreeNode.Nodes.Add(nodeLabel);
                                }
                            }
                            if (shapeTreeNode.Nodes.Count > 0)
                            {
                                shapeTreeNode.Expand();
                            }
                            else
                            {
                                shapeTreeNode.Remove();
                            }
                        }
                    }
                }
            }

            _resultsTreeView.Visible = true;
        }
    }

    // Perform validation
    public void Validate()
    {
        _resultsTreeView.Nodes.Clear();

        if (_graphToValidate.Graph != null && _referenceGraph.Graph != null)
        {
            // Get shapes from reference and validate graph
            ShapesGraph referenceShapesGraph = new ShapesGraph(_referenceGraph.Graph);
            Report validationReport = referenceShapesGraph.Validate(_graphToValidate.Graph);

            if (!validationReport.Conforms)
            {
                foreach (Result instanceValidationReportResult in validationReport.Results)
                {
                    TreeNode resultNode = _resultsTreeView.Nodes.Add(GetResolveNodeValue(referenceShapesGraph, instanceValidationReportResult.Severity) + " > " + GetResolveNodeValue(referenceShapesGraph, instanceValidationReportResult.ResultPath));
                    resultNode.Nodes.Add("Focus node: " + GetResolveNodeValue(referenceShapesGraph, instanceValidationReportResult.FocusNode));
                    resultNode.Nodes.Add("Source shape: " + GetResolveNodeValue(referenceShapesGraph, instanceValidationReportResult.SourceShape));
                    resultNode.Nodes.Add("Message: " + GetResolveNodeValue(referenceShapesGraph, instanceValidationReportResult.Message));
                }
            }
            else
            {
                _resultsTreeView.Nodes.Add("Validation successful. Instance conforms to SHACL shapes.");
            }
        }
    }

    // Returns a node value, replaces namespace URI with prefix
    private string GetResolveNodeValue(IGraph graph, INode node)
    {
        string resolvedNodeValue = node.ToString();
        if (node is LiteralNode literalNode)
        {
            resolvedNodeValue = literalNode.Value;
            if (!string.IsNullOrEmpty(literalNode.Language))
            {
                resolvedNodeValue += "@" + literalNode.Language;
            }
        }
        else if (node is IUriNode uriNode)
        {
            string matchUri = "";
            string matchPrefix = "";
            foreach (string namespacePrefix in graph.NamespaceMap.Prefixes)
            {
                if (graph.NamespaceMap.HasNamespace(namespacePrefix))
                {
                    Uri namespaceUri = graph.NamespaceMap.GetNamespaceUri(namespacePrefix);
                    if (uriNode.Uri.OriginalString.StartsWith(namespaceUri.OriginalString))
                    {
                        if (namespaceUri.OriginalString.Length > matchUri.Length)
                        {
                            matchUri = namespaceUri.OriginalString;
                            matchPrefix = namespacePrefix;
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(matchPrefix))
            {
                matchPrefix += ":";
            }
            if (!string.IsNullOrEmpty(matchUri))
            {
                resolvedNodeValue = resolvedNodeValue.Replace(matchUri, matchPrefix);
            }
        }
        return resolvedNodeValue;
    }
}
