namespace CascaraRdfValidator;

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
        Metamodel = new Label();
        IndividualsValidationLabel = new Label();
        OntologyValidationLbl = new Label();
        SourcesLabel = new Label();
        ButtonsTable = new TableLayoutPanel();
        ValidateButton = new Button();
        AnalyzeButton = new Button();
        IndividualsValidationTreeView = new TreeView();
        OntologyValidationTreeView = new TreeView();
        SourcesPanel = new TableLayoutPanel();
        MainPanel.SuspendLayout();
        ButtonsTable.SuspendLayout();
        SuspendLayout();
        // 
        // MainPanel
        // 
        MainPanel.ColumnCount = 2;
        MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        MainPanel.Controls.Add(IndividualsLabel, 0, 1);
        MainPanel.Controls.Add(OntologyLabel, 0, 2);
        MainPanel.Controls.Add(Metamodel, 0, 3);
        MainPanel.Controls.Add(IndividualsValidationLabel, 0, 4);
        MainPanel.Controls.Add(OntologyValidationLbl, 0, 5);
        MainPanel.Controls.Add(SourcesLabel, 0, 0);
        MainPanel.Controls.Add(SourcesPanel, 1, 0);
        MainPanel.Controls.Add(ButtonsTable, 1, 6);
        MainPanel.Controls.Add(IndividualsValidationTreeView, 1, 4);
        MainPanel.Controls.Add(OntologyValidationTreeView, 1, 5);
        MainPanel.Dock = DockStyle.Fill;
        MainPanel.Location = new Point(0, 0);
        MainPanel.Margin = new Padding(0);
        MainPanel.Name = "MainPanel";
        MainPanel.RowCount = 7;
        MainPanel.RowStyles.Add(new RowStyle());
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
        IndividualsLabel.Location = new Point(3, 100);
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
        OntologyLabel.Location = new Point(3, 128);
        OntologyLabel.Name = "OntologyLabel";
        OntologyLabel.Size = new Size(94, 28);
        OntologyLabel.TabIndex = 1;
        OntologyLabel.Text = "Ontology";
        OntologyLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // Metamodel
        // 
        Metamodel.AutoSize = true;
        Metamodel.Dock = DockStyle.Left;
        Metamodel.Location = new Point(3, 156);
        Metamodel.Name = "Metamodel";
        Metamodel.Size = new Size(68, 28);
        Metamodel.TabIndex = 5;
        Metamodel.Text = "Metamodel";
        Metamodel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // IndividualsValidationLabel
        // 
        IndividualsValidationLabel.AutoSize = true;
        IndividualsValidationLabel.Dock = DockStyle.Fill;
        IndividualsValidationLabel.Location = new Point(3, 184);
        IndividualsValidationLabel.Name = "IndividualsValidationLabel";
        IndividualsValidationLabel.Size = new Size(94, 119);
        IndividualsValidationLabel.TabIndex = 9;
        IndividualsValidationLabel.Text = "Individuals > Ontology";
        IndividualsValidationLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // OntologyValidationLbl
        // 
        OntologyValidationLbl.AutoSize = true;
        OntologyValidationLbl.Dock = DockStyle.Fill;
        OntologyValidationLbl.ImageAlign = ContentAlignment.MiddleLeft;
        OntologyValidationLbl.Location = new Point(3, 303);
        OntologyValidationLbl.Name = "OntologyValidationLbl";
        OntologyValidationLbl.Size = new Size(94, 119);
        OntologyValidationLbl.TabIndex = 10;
        OntologyValidationLbl.Text = "Ontology > Metamodel";
        OntologyValidationLbl.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // SourcesLabel
        // 
        SourcesLabel.AutoSize = true;
        SourcesLabel.Dock = DockStyle.Fill;
        SourcesLabel.Location = new Point(3, 0);
        SourcesLabel.Name = "SourcesLabel";
        SourcesLabel.Size = new Size(94, 100);
        SourcesLabel.TabIndex = 14;
        SourcesLabel.Text = "Sources";
        SourcesLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // ButtonsTable
        // 
        ButtonsTable.ColumnCount = 2;
        ButtonsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        ButtonsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        ButtonsTable.Controls.Add(ValidateButton, 1, 0);
        ButtonsTable.Controls.Add(AnalyzeButton, 0, 0);
        ButtonsTable.Location = new Point(100, 422);
        ButtonsTable.Margin = new Padding(0);
        ButtonsTable.Name = "ButtonsTable";
        ButtonsTable.RowCount = 1;
        ButtonsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        ButtonsTable.Size = new Size(200, 28);
        ButtonsTable.TabIndex = 17;
        // 
        // ValidateButton
        // 
        ValidateButton.BackColor = SystemColors.Control;
        ValidateButton.Dock = DockStyle.Fill;
        ValidateButton.FlatAppearance.BorderSize = 0;
        ValidateButton.FlatStyle = FlatStyle.Flat;
        ValidateButton.Location = new Point(103, 3);
        ValidateButton.Name = "ValidateButton";
        ValidateButton.Size = new Size(94, 22);
        ValidateButton.TabIndex = 18;
        ValidateButton.Text = "Validate";
        ValidateButton.UseVisualStyleBackColor = false;
        // 
        // AnalyzeButton
        // 
        AnalyzeButton.BackColor = SystemColors.Control;
        AnalyzeButton.Dock = DockStyle.Fill;
        AnalyzeButton.FlatAppearance.BorderSize = 0;
        AnalyzeButton.FlatStyle = FlatStyle.Flat;
        AnalyzeButton.Location = new Point(3, 3);
        AnalyzeButton.Name = "AnalyzeButton";
        AnalyzeButton.Size = new Size(94, 22);
        AnalyzeButton.TabIndex = 17;
        AnalyzeButton.Text = "Analyze";
        AnalyzeButton.UseVisualStyleBackColor = false;
        // 
        // IndividualsValidationTreeView
        // 
        IndividualsValidationTreeView.BackColor = SystemColors.Control;
        IndividualsValidationTreeView.BorderStyle = BorderStyle.None;
        IndividualsValidationTreeView.Dock = DockStyle.Fill;
        IndividualsValidationTreeView.Location = new Point(103, 187);
        IndividualsValidationTreeView.Name = "IndividualsValidationTreeView";
        IndividualsValidationTreeView.Size = new Size(694, 113);
        IndividualsValidationTreeView.TabIndex = 18;
        // 
        // OntologyValidationTreeView
        // 
        OntologyValidationTreeView.BackColor = SystemColors.Control;
        OntologyValidationTreeView.BorderStyle = BorderStyle.None;
        OntologyValidationTreeView.Dock = DockStyle.Fill;
        OntologyValidationTreeView.Location = new Point(103, 306);
        OntologyValidationTreeView.Name = "OntologyValidationTreeView";
        OntologyValidationTreeView.Size = new Size(694, 113);
        OntologyValidationTreeView.TabIndex = 19;
        // 
        // SourcesPanel
        // 
        SourcesPanel.ColumnCount = 2;
        SourcesPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        SourcesPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        SourcesPanel.Dock = DockStyle.Fill;
        SourcesPanel.Location = new Point(100, 0);
        SourcesPanel.Margin = new Padding(0);
        SourcesPanel.Name = "SourcesPanel";
        SourcesPanel.RowCount = 2;
        SourcesPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        SourcesPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        SourcesPanel.Size = new Size(700, 100);
        SourcesPanel.TabIndex = 15;
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
        ButtonsTable.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    private Label IndividualsLabel;
private Label OntologyLabel;
private Label Metamodel;
private TextBox textBox2;
private Label IndividualsValidationLabel;
private Label OntologyValidationLbl;
    private Label SourcesLabel;
    public TableLayoutPanel MainPanel;
    private TableLayoutPanel ButtonsTable;
    public Button ValidateButton;
    public Button AnalyzeButton;
    public TreeView IndividualsValidationTreeView;
    public TreeView OntologyValidationTreeView;
    public TableLayoutPanel SourcesPanel;
}