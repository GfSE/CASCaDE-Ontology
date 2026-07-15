namespace CascaraValidator;

partial class ControllerDialog
{
/// <summary>
/// Required designer variable.
/// </summary>
private System.ComponentModel.IContainer components = null;

/// <summary>
/// Clean up any resources being used.
/// </summary>
/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
protected override void Dispose(bool disposing)
{
    if (disposing && (components != null))
    {
        components.Dispose();
    }
    base.Dispose(disposing);
}

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        MainPanel = new TableLayoutPanel();
        IndividualsLabel = new Label();
        OntologyLabel = new Label();
        SelectIndividualsButton = new Button();
        SelectOntologyButton = new Button();
        SelectMetamodelButton = new Button();
        Metamodel = new Label();
        IndividualsTextBox = new TextBox();
        OntologyTextBox = new TextBox();
        MetamodelTextBox = new TextBox();
        IndividualsValidationLabel = new Label();
        OntologyValidationLbl = new Label();
        IndividualsValidationListBox = new ListBox();
        OntologyValidationListBox = new ListBox();
        ValidateButton = new Button();
        MainPanel.SuspendLayout();
        SuspendLayout();
        // 
        // MainPanel
        // 
        MainPanel.ColumnCount = 3;
        MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
        MainPanel.Controls.Add(IndividualsLabel, 0, 0);
        MainPanel.Controls.Add(OntologyLabel, 0, 1);
        MainPanel.Controls.Add(SelectIndividualsButton, 2, 0);
        MainPanel.Controls.Add(SelectOntologyButton, 2, 1);
        MainPanel.Controls.Add(SelectMetamodelButton, 2, 2);
        MainPanel.Controls.Add(Metamodel, 0, 2);
        MainPanel.Controls.Add(IndividualsTextBox, 1, 0);
        MainPanel.Controls.Add(OntologyTextBox, 1, 1);
        MainPanel.Controls.Add(MetamodelTextBox, 1, 2);
        MainPanel.Controls.Add(IndividualsValidationLabel, 0, 3);
        MainPanel.Controls.Add(OntologyValidationLbl, 0, 4);
        MainPanel.Controls.Add(IndividualsValidationListBox, 1, 3);
        MainPanel.Controls.Add(OntologyValidationListBox, 1, 4);
        MainPanel.Controls.Add(ValidateButton, 1, 5);
        MainPanel.Dock = DockStyle.Fill;
        MainPanel.Location = new Point(0, 0);
        MainPanel.Name = "MainPanel";
        MainPanel.Padding = new Padding(3);
        MainPanel.RowCount = 6;
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        MainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        MainPanel.Size = new Size(800, 450);
        MainPanel.TabIndex = 0;
        // 
        // IndividualsLabel
        // 
        IndividualsLabel.AutoSize = true;
        IndividualsLabel.Dock = DockStyle.Fill;
        IndividualsLabel.Location = new Point(6, 3);
        IndividualsLabel.Name = "IndividualsLabel";
        IndividualsLabel.Size = new Size(94, 28);
        IndividualsLabel.TabIndex = 0;
        IndividualsLabel.Text = "Individuals";
        IndividualsLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // OntologyLabel
        // 
        OntologyLabel.AutoSize = true;
        OntologyLabel.Dock = DockStyle.Fill;
        OntologyLabel.Location = new Point(6, 31);
        OntologyLabel.Name = "OntologyLabel";
        OntologyLabel.Size = new Size(94, 28);
        OntologyLabel.TabIndex = 1;
        OntologyLabel.Text = "Ontology";
        OntologyLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // SelectIndividualsButton
        // 
        SelectIndividualsButton.BackColor = SystemColors.Control;
        SelectIndividualsButton.Dock = DockStyle.Fill;
        SelectIndividualsButton.FlatAppearance.BorderSize = 0;
        SelectIndividualsButton.FlatStyle = FlatStyle.Flat;
        SelectIndividualsButton.Location = new Point(770, 6);
        SelectIndividualsButton.Name = "SelectIndividualsButton";
        SelectIndividualsButton.Size = new Size(24, 22);
        SelectIndividualsButton.TabIndex = 2;
        SelectIndividualsButton.Text = "...";
        SelectIndividualsButton.UseVisualStyleBackColor = false;
        // 
        // SelectOntologyButton
        // 
        SelectOntologyButton.BackColor = SystemColors.Control;
        SelectOntologyButton.Dock = DockStyle.Fill;
        SelectOntologyButton.FlatAppearance.BorderSize = 0;
        SelectOntologyButton.FlatStyle = FlatStyle.Flat;
        SelectOntologyButton.Location = new Point(770, 34);
        SelectOntologyButton.Name = "SelectOntologyButton";
        SelectOntologyButton.Size = new Size(24, 22);
        SelectOntologyButton.TabIndex = 3;
        SelectOntologyButton.Text = "...";
        SelectOntologyButton.UseVisualStyleBackColor = false;
        // 
        // SelectMetamodelButton
        // 
        SelectMetamodelButton.BackColor = SystemColors.Control;
        SelectMetamodelButton.Dock = DockStyle.Fill;
        SelectMetamodelButton.FlatAppearance.BorderSize = 0;
        SelectMetamodelButton.FlatStyle = FlatStyle.Flat;
        SelectMetamodelButton.Location = new Point(770, 62);
        SelectMetamodelButton.Name = "SelectMetamodelButton";
        SelectMetamodelButton.Size = new Size(24, 22);
        SelectMetamodelButton.TabIndex = 4;
        SelectMetamodelButton.Text = "...";
        SelectMetamodelButton.UseVisualStyleBackColor = false;
        // 
        // Metamodel
        // 
        Metamodel.AutoSize = true;
        Metamodel.Dock = DockStyle.Left;
        Metamodel.Location = new Point(6, 59);
        Metamodel.Name = "Metamodel";
        Metamodel.Size = new Size(68, 28);
        Metamodel.TabIndex = 5;
        Metamodel.Text = "Metamodel";
        Metamodel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // IndividualsTextBox
        // 
        IndividualsTextBox.BackColor = SystemColors.Control;
        IndividualsTextBox.BorderStyle = BorderStyle.None;
        IndividualsTextBox.Dock = DockStyle.Fill;
        IndividualsTextBox.Enabled = false;
        IndividualsTextBox.Location = new Point(106, 6);
        IndividualsTextBox.Name = "IndividualsTextBox";
        IndividualsTextBox.Size = new Size(658, 16);
        IndividualsTextBox.TabIndex = 6;
        // 
        // OntologyTextBox
        // 
        OntologyTextBox.BackColor = SystemColors.Control;
        OntologyTextBox.BorderStyle = BorderStyle.None;
        OntologyTextBox.Cursor = Cursors.No;
        OntologyTextBox.Dock = DockStyle.Fill;
        OntologyTextBox.Enabled = false;
        OntologyTextBox.Location = new Point(106, 34);
        OntologyTextBox.Name = "OntologyTextBox";
        OntologyTextBox.Size = new Size(658, 16);
        OntologyTextBox.TabIndex = 7;
        // 
        // MetamodelTextBox
        // 
        MetamodelTextBox.BackColor = SystemColors.Control;
        MetamodelTextBox.BorderStyle = BorderStyle.None;
        MetamodelTextBox.Dock = DockStyle.Fill;
        MetamodelTextBox.Enabled = false;
        MetamodelTextBox.Location = new Point(106, 62);
        MetamodelTextBox.Name = "MetamodelTextBox";
        MetamodelTextBox.Size = new Size(658, 16);
        MetamodelTextBox.TabIndex = 8;
        // 
        // IndividualsValidationLabel
        // 
        IndividualsValidationLabel.AutoSize = true;
        IndividualsValidationLabel.Dock = DockStyle.Fill;
        IndividualsValidationLabel.Location = new Point(6, 87);
        IndividualsValidationLabel.Name = "IndividualsValidationLabel";
        IndividualsValidationLabel.Size = new Size(94, 166);
        IndividualsValidationLabel.TabIndex = 9;
        IndividualsValidationLabel.Text = "Individuals > Ontology";
        IndividualsValidationLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // OntologyValidationLbl
        // 
        OntologyValidationLbl.AutoSize = true;
        OntologyValidationLbl.Dock = DockStyle.Fill;
        OntologyValidationLbl.ImageAlign = ContentAlignment.MiddleLeft;
        OntologyValidationLbl.Location = new Point(6, 253);
        OntologyValidationLbl.Name = "OntologyValidationLbl";
        OntologyValidationLbl.Size = new Size(94, 166);
        OntologyValidationLbl.TabIndex = 10;
        OntologyValidationLbl.Text = "Ontology > Metamodel";
        OntologyValidationLbl.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // IndividualsValidationListBox
        // 
        IndividualsValidationListBox.BackColor = SystemColors.Control;
        IndividualsValidationListBox.BorderStyle = BorderStyle.None;
        IndividualsValidationListBox.Dock = DockStyle.Fill;
        IndividualsValidationListBox.FormattingEnabled = true;
        IndividualsValidationListBox.ItemHeight = 15;
        IndividualsValidationListBox.Location = new Point(106, 90);
        IndividualsValidationListBox.Name = "IndividualsValidationListBox";
        IndividualsValidationListBox.Size = new Size(658, 160);
        IndividualsValidationListBox.TabIndex = 11;
        // 
        // OntologyValidationListBox
        // 
        OntologyValidationListBox.BackColor = SystemColors.Control;
        OntologyValidationListBox.BorderStyle = BorderStyle.None;
        OntologyValidationListBox.Dock = DockStyle.Fill;
        OntologyValidationListBox.FormattingEnabled = true;
        OntologyValidationListBox.ItemHeight = 15;
        OntologyValidationListBox.Location = new Point(106, 256);
        OntologyValidationListBox.Name = "OntologyValidationListBox";
        OntologyValidationListBox.Size = new Size(658, 160);
        OntologyValidationListBox.TabIndex = 12;
        // 
        // ValidateButton
        // 
        ValidateButton.BackColor = SystemColors.Control;
        ValidateButton.Dock = DockStyle.Left;
        ValidateButton.FlatAppearance.BorderSize = 0;
        ValidateButton.FlatStyle = FlatStyle.Flat;
        ValidateButton.Location = new Point(106, 422);
        ValidateButton.Name = "ValidateButton";
        ValidateButton.Size = new Size(75, 22);
        ValidateButton.TabIndex = 13;
        ValidateButton.Text = "Validate";
        ValidateButton.UseVisualStyleBackColor = false;
        // 
        // ControllerDialog
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.Window;
        ClientSize = new Size(800, 450);
        Controls.Add(MainPanel);
        Name = "ControllerDialog";
        Text = "CASCaRA RDF Validator";
        MainPanel.ResumeLayout(false);
        MainPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel MainPanel;
private Label IndividualsLabel;
private Label OntologyLabel;
private Label Metamodel;
private TextBox textBox2;
private Label IndividualsValidationLabel;
private Label OntologyValidationLbl;
public TextBox IndividualsTextBox;
public TextBox OntologyTextBox;
public TextBox MetamodelTextBox;
public Button SelectIndividualsButton;
public Button SelectOntologyButton;
public Button SelectMetamodelButton;
public Button ValidateButton;
public ListBox IndividualsValidationListBox;
public ListBox OntologyValidationListBox;
}