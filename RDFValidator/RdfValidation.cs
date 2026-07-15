// SPDX-FileCopyrightText: 2026 prostep ivip Association
// SPDX-FileCopyrightText: 2026 Michael Kirsch <michael.kirsch@em.ag>

using VDS.RDF;
using VDS.RDF.Shacl;
using VDS.RDF.Shacl.Validation;

namespace CascaraValidator;

/// <summary>
/// Handles the validation of an RDF graph against a SHACL shapes graph
/// </summary>
public class RdfValidation
{
    private RdfGraph _graphToValidate;
    private RdfGraph _referenceGraph;
    private ListBox _resultsListBox;

    public RdfValidation(RdfGraph graphToValidate, RdfGraph referenceGraph, ListBox resultsListBox)
    {
        _graphToValidate = graphToValidate;
        _referenceGraph = referenceGraph;

        _resultsListBox = resultsListBox;
    }

    // Perform validation
    public void Validate()
    {
        _resultsListBox.Items.Clear();

        if (_graphToValidate.Graph != null && _referenceGraph.Graph != null)
        {
            // Get shapes from reference and validate graph
            ShapesGraph referenceShapesGraph = new ShapesGraph(_referenceGraph.Graph);
            Report validationReport = referenceShapesGraph.Validate(_graphToValidate.Graph);

            _resultsListBox.Items.Add($"Conforms: {validationReport.Conforms}");

            if (!validationReport.Conforms)
            {
                _resultsListBox.Items.Add($"Validation failed. Results: {validationReport.Results.Count()}");

                foreach (Result instanceValidationReportResult in validationReport.Results)
                {
                    _resultsListBox.Items.Add("----------------------------------------");

                    _resultsListBox.Items.Add($"Severity: " + GetResolveNodeValue(referenceShapesGraph, instanceValidationReportResult.Severity));

                    if (instanceValidationReportResult.FocusNode != null)
                    {
                        _resultsListBox.Items.Add($"Focus node: {instanceValidationReportResult.FocusNode}");
                    }

                    if (instanceValidationReportResult.SourceShape != null)
                    {
                        _resultsListBox.Items.Add($"Source shape: {instanceValidationReportResult.SourceShape}");
                    }

                    if (instanceValidationReportResult.ResultPath != null)
                    {
                        _resultsListBox.Items.Add($"Property path: {instanceValidationReportResult.ResultPath}");
                    }

                    if (!string.IsNullOrEmpty(instanceValidationReportResult.Message.Value))
                    {
                        _resultsListBox.Items.Add($"Message: {instanceValidationReportResult.Message}");
                    }
                }
            }
            else
            {
                _resultsListBox.Items.Add("Validation successful. Instance conforms to SHACL shapes.");
            }
        }
    }

    private string GetResolveNodeValue(ShapesGraph referenceShapesGraph, INode node)
    {
        string resolvedNodeValue = node.ToString();
        if (node is UriNode uriNode)
        {
            if (uriNode.Uri.OriginalString.StartsWith("http://www.w3.org/ns/shacl#"))
            {
                resolvedNodeValue = resolvedNodeValue.Replace("http://www.w3.org/ns/shacl#", "sh:");
            }
            else
            {
                foreach (string namespacePrefix in referenceShapesGraph.NamespaceMap.Prefixes)
                {
                    if (_graphToValidate.Graph.NamespaceMap.HasNamespace(namespacePrefix))
                    {
                        Uri namespaceUri = _graphToValidate.Graph.NamespaceMap.GetNamespaceUri(namespacePrefix);
                        if (uriNode.Uri.OriginalString.StartsWith(namespaceUri.OriginalString))
                        {
                            resolvedNodeValue = resolvedNodeValue.Replace(namespaceUri.OriginalString, namespacePrefix);
                            break;
                        }
                    }
                }
            }
        }
        return resolvedNodeValue;
    }
}
