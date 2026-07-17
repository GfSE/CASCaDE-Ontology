// SPDX-FileCopyrightText: 2026 prostep ivip Association
// SPDX-FileCopyrightText: 2026 Michael Kirsch <michael.kirsch@em.ag>

using Microsoft.Win32;

namespace CascaraRdfValidator;

/// <summary>
/// Handles file selections in the dialog
/// </summary>
public class ResourceSelection
{
    private string _registryValueKey;
    private RegistryKey _registryGroupKey;
    private string _fileFileSelectionFilter;
    private TableLayoutPanel _tableLayoutPanel;
    private TextBox _filePathTextBox;
    private Button _fileSelectionButton;

    public string FilePath { get; set; }

    public event EventHandler<EventArgs> FileSelected;

    public ResourceSelection(string registryValueKey, RegistryKey registryGroupKey, string fileSelectionFilter, TableLayoutPanel parentPanel, int row, int column)
    {
        _registryValueKey = registryValueKey;
        _registryGroupKey = registryGroupKey;
        _fileFileSelectionFilter = fileSelectionFilter;

        _tableLayoutPanel = new TableLayoutPanel();
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Margin = new Padding(0);
        _tableLayoutPanel.ColumnCount = 2;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
        parentPanel.Controls.Add(_tableLayoutPanel, column, row);

        _filePathTextBox = new TextBox();
        _filePathTextBox.ReadOnly = true;
        _filePathTextBox.Multiline = true;
        _filePathTextBox.BorderStyle = BorderStyle.None;
        _filePathTextBox.BackColor = SystemColors.Control;
        _filePathTextBox.Dock = DockStyle.Fill;
        _tableLayoutPanel.Controls.Add(_filePathTextBox, 0, 0);

        _fileSelectionButton = new Button();
        _fileSelectionButton.Text = "...";
        _fileSelectionButton.TextAlign = ContentAlignment.MiddleCenter;
        _fileSelectionButton.FlatStyle = FlatStyle.Flat;
        _fileSelectionButton.FlatAppearance.BorderSize = 0;
        _fileSelectionButton.BackColor = SystemColors.Control;
        _fileSelectionButton.Dock = DockStyle.Fill;
        _fileSelectionButton.Click += FileSelectionButton_Click;
        _tableLayoutPanel.Controls.Add(_fileSelectionButton, 1, 0);

        string graphFilePath = _registryGroupKey.GetValue(_registryValueKey)?.ToString();
        if (File.Exists(graphFilePath))
        {
            FilePath = graphFilePath;
            new ToolTip().SetToolTip(_filePathTextBox, FilePath);
            _filePathTextBox.Text = Path.GetFileName(FilePath);
        }
    }

    // Selects the graph from file system
    private void FileSelectionButton_Click(object sender, EventArgs e)
    {
        string graphFilePath = GetFilePathByDialog("Select " + _registryValueKey.ToLower() + " ontology file", _fileFileSelectionFilter, Path.GetDirectoryName(FilePath));
        if (File.Exists(graphFilePath))
        {
            FilePath = graphFilePath;
            _filePathTextBox.Text = FilePath;
            _registryGroupKey.SetValue(_registryValueKey, FilePath);
            FileSelected?.Invoke(this, new EventArgs());
        }
    }

    // Returns a file path using file selection dialog
    private string GetFilePathByDialog(string title, string filter, string defaultFolder = null)
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
