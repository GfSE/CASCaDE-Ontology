// SPDX-FileCopyrightText: 2026 prostep ivip Association
// SPDX-FileCopyrightText: 2026 Michael Kirsch <michael.kirsch@em.ag>

using Microsoft.Win32;
using NLog;

namespace CascaraValidator;

/// <summary>
/// Static main class of the CASCaRA RDF validator
/// </summary>
public static class Controller
{
    private static RegistryKey _registryGroupKey;
    private static ControllerDialog _dialog = new ControllerDialog();

    private static RdfGraph _individualsGraph;
    private static RdfGraph _ontologyGraph;
    private static RdfGraph _metamodelGraph;

    private static RdfValidation _individualsValidation;
    private static RdfValidation _ontologyValidation;

    public static Logger Logger = LogManager.GetCurrentClassLogger();

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

        // Initialize RDF graphs
        _individualsGraph = new RdfGraph("Individuals", _registryGroupKey, _dialog.IndividualsTextBox, _dialog.SelectIndividualsButton);
        _ontologyGraph = new RdfGraph("Ontology", _registryGroupKey, _dialog.OntologyTextBox, _dialog.SelectOntologyButton);
        _metamodelGraph = new RdfGraph("Metamodel", _registryGroupKey, _dialog.MetamodelTextBox, _dialog.SelectMetamodelButton);

        // Initialize RDF validations
        _individualsValidation = new RdfValidation(_individualsGraph, _ontologyGraph, _dialog.IndividualsValidationListBox);
        _ontologyValidation = new RdfValidation(_ontologyGraph, _metamodelGraph, _dialog.OntologyValidationListBox);

        // Assign dialog behavior
        _dialog.ValidateButton.Click += ValidateButton_Click;

        // Show dialog
        _dialog.ShowDialog();
    }
}