// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WindowsUnixFileDeleter;

/// <summary>
/// The main form.
/// </summary>
public partial class Main : Form
{
    /// <summary>
    /// The language manager.
    /// </summary>
    private readonly ILanguageManager languageManager = new LanguageManager();

    /// <summary>
    /// The background worker.
    /// </summary>
    private readonly BackgroundWorker worker = new();

    /// <summary>
    /// The files counter.
    /// </summary>
    private double filesCounter;

    /// <summary>
    /// The language.
    /// </summary>
    private ILanguage? language;

    /// <summary>
    /// Initializes a new instance of the <see cref="Main"/> class.
    /// </summary>
    public Main()
    {
        this.Initialize();
    }

    /// <summary>
    /// Moves a file to the recycle bin.
    /// </summary>
    /// <param name="path">The path.</param>
    private static void MoveFileToRecycleBin(string path)
    {
        FileSystem.DeleteFile(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
    }

    /// <summary>
    /// Initializes the language manager.
    /// </summary>
    private void InitializeLanguageManager()
    {
        this.languageManager.SetCurrentLanguage("de-DE");
        this.languageManager.OnLanguageChanged += this.OnLanguageChanged;
    }

    /// <summary>
    /// Loads the languages to a combo box.
    /// </summary>
    private void LoadLanguagesToCombo()
    {
        foreach (var lang in this.languageManager.GetLanguages())
        {
            this.comboBoxLanguage.Items.Add(lang.Name);
        }

        this.comboBoxLanguage.SelectedIndex = 0;
    }

    /// <summary>
    /// Handles the language changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void ComboBoxLanguageSelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItem = this.comboBoxLanguage.SelectedItem.ToString();

        if (string.IsNullOrWhiteSpace(selectedItem))
        {
            return;
        }

        this.languageManager.SetCurrentLanguageFromName(selectedItem);
    }

    /// <summary>
    /// Handles the language changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void OnLanguageChanged(object sender, EventArgs e)
    {
        this.language = this.languageManager.GetCurrentLanguage();
        this.buttonChooseDirectory.Text = this.language.GetWord("ChooseDirectory");
        this.buttonStart.Text = this.language.GetWord("Start");
        this.labelDirectory.Text = this.language.GetWord("Directory");
    }

    /// <summary>
    /// Initializes the data.
    /// </summary>
    private void Initialize()
    {
        this.InitializeComponent();
        this.InitializeCaption();
        this.InitializeLanguageManager();
        this.LoadLanguagesToCombo();
        this.InitializeBackgroundWorker();
    }

    /// <summary>
    /// Initializes the caption.
    /// </summary>
    private void InitializeCaption()
    {
        this.Text = Application.ProductName + @" " + Application.ProductVersion;
    }

    /// <summary>
    /// Initializes the background worker.
    /// </summary>
    private void InitializeBackgroundWorker()
    {
        this.worker.DoWork += this.SearchDirectoryBackground;
        this.worker.RunWorkerCompleted += this.EvaluateResult;
    }

    /// <summary>
    /// Searches the directory in the background.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void SearchDirectoryBackground(object sender, DoWorkEventArgs e)
    {
        var directory = string.Empty;
        this.UiThreadInvoke(() => { directory = this.richTextBoxDirectory.Text; });
        this.SearchDirectory(directory);
    }

    /// <summary>
    /// Searches the directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    private void SearchDirectory(string directory)
    {
        try
        {
            this.SearchFilesAndDirectories(directory);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Searches the files and directories in the directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    private void SearchFilesAndDirectories(string directory)
    {
        this.DeleteFiles(directory);
        this.SearchDirectories(directory);
    }

    /// <summary>
    /// Deletes the files in the directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    private void DeleteFiles(string directory)
    {
        foreach (var file in Directory.EnumerateFiles(directory))
        {
            try
            {
                this.CheckFileForDelete(file);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    /// <summary>
    /// Searches the directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    private void SearchDirectories(string directory)
    {
        foreach (var dir in Directory.EnumerateDirectories(directory))
        {
            try
            {
                this.SearchDirectory(dir);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    /// <summary>
    /// Checks a file for deletion and deletes it if necessary.
    /// </summary>
    /// <param name="file">The file.</param>
    private void CheckFileForDelete(string file)
    {
        var fileName = Path.GetFileName(file);

        if (fileName != null && !fileName.StartsWith("."))
        {
            return;
        }

        MoveFileToRecycleBin(file);
        this.filesCounter++;
    }

    /// <summary>
    /// Evaluates the result.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void EvaluateResult(object sender, RunWorkerCompletedEventArgs e)
    {
        if (this.language is null)
        {
            throw new ArgumentNullException(nameof(this.language), "The language wasn't properly set.");
        }

        var filesDeletedCaption = this.language.GetWord("FilesDeletedCaption");
        var filesDeletedText = this.language.GetWord("FilesDeletedText");
        MessageBox.Show(this.filesCounter + @" " + filesDeletedText, filesDeletedCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        this.LockGui(false);
    }

    /// <summary>
    /// Starts a background scan.
    /// </summary>
    private void StartBackgroundScan()
    {
        if (!this.CheckFolderSelected())
        {
            return;
        }

        this.ResetCounter();
        this.LockGui(true);
        this.worker.RunWorkerAsync();
    }

    /// <summary>
    /// Resets the counter.
    /// </summary>
    private void ResetCounter()
    {
        this.filesCounter = 0;
    }

    /// <summary>
    /// Checks whether a folder is selected or not.
    /// </summary>
    /// <returns><c>true</c> if a folder is selected, <c>false</c> else.</returns>
    private bool CheckFolderSelected()
    {
        if (!string.IsNullOrWhiteSpace(this.richTextBoxDirectory.Text))
        {
            return true;
        }

        if (this.language is null)
        {
            throw new ArgumentNullException(nameof(this.language), "The language wasn't properly set.");
        }

        var noFolderSelectedCaption = this.language.GetWord("NoFolderSelectedCaption");
        var noFolderSelectedText = this.language.GetWord("NoFolderSelectedText");
        MessageBox.Show(noFolderSelectedText, noFolderSelectedCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }

    /// <summary>
    /// Locks or unlocks the GUI.
    /// </summary>
    /// <param name="locked">A value indicating whether the GUI should be locked or not.</param>
    private void LockGui(bool locked)
    {
        this.buttonChooseDirectory.Enabled = !locked;
        this.buttonStart.Enabled = !locked;
    }

    /// <summary>
    /// Handles the button click to choose the directory.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void ButtonChooseDirectoryClick(object sender, EventArgs e)
    {
        var dialog = new FolderBrowserDialog();
        var result = dialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            this.richTextBoxDirectory.Text = dialog.SelectedPath;
        }
    }

    /// <summary>
    /// Handles the button click to start the scan process.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void ButtonStartClick(object sender, EventArgs e)
    {
        try
        {
            this.StartBackgroundScan();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
