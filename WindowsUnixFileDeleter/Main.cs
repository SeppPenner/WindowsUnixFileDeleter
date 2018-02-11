using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Languages.Implementation;
using Languages.Interfaces;
using Microsoft.VisualBasic.FileIO;
using UiThreadInvoke;

public partial class Main : Form
{
    private readonly ILanguageManager _lm = new LanguageManager();
    private readonly BackgroundWorker _worker = new BackgroundWorker();
    private double _countFiles;
    private ILanguage _lang;

    public Main()
    {
        Initialize();
    }

    private void InitializeLanguageManager()
    {
        _lm.SetCurrentLanguage("de-DE");
        _lm.OnLanguageChanged += OnLanguageChanged;
    }

    private void LoadLanguagesToCombo()
    {
        foreach (var lang in _lm.GetLanguages())
            comboBoxLanguage.Items.Add(lang.Name);
        comboBoxLanguage.SelectedIndex = 0;
    }

    private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        _lm.SetCurrentLanguageFromName(comboBoxLanguage.SelectedItem.ToString());
    }

    private void OnLanguageChanged(object sender, EventArgs eventArgs)
    {
        _lang = _lm.GetCurrentLanguage();
        buttonChooseDirectory.Text = _lang.GetWord("ChooseDirectory");
        buttonStart.Text = _lang.GetWord("Start");
        labelDirectory.Text = _lang.GetWord("Directory");
    }

    private void Initialize()
    {
        InitializeComponent();
        InitializeCaption();
        InitializeLanguageManager();
        LoadLanguagesToCombo();
        InitializeBackgroundWorker();
    }

    private void InitializeCaption()
    {
        Text = Application.ProductName + @" " + Application.ProductVersion;
    }

    private void InitializeBackgroundWorker()
    {
        _worker.DoWork += SearchDirectoryBackground;
        _worker.RunWorkerCompleted += EvaluateResult;
    }

    private void SearchDirectoryBackground(object sender, DoWorkEventArgs e)
    {
        var directory = "";
        this.UiThreadInvoke(() => { directory = richTextBoxDirectory.Text; });
        SearchDirectory(directory);
    }

    private void SearchDirectory(string directory)
    {
        try
        {
            SearchFilesAndDirectories(directory);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SearchFilesAndDirectories(string directory)
    {
        DeleteFiles(directory);
        SearchDirectories(directory);
    }

    private void DeleteFiles(string directory)
    {
        foreach (var file in Directory.EnumerateFiles(directory))
            try
            {
                CheckFileForDelete(file);
            }
            catch (Exception)
            {
                // ignored
            }
    }

    private void SearchDirectories(string directory)
    {
        foreach (var dir in Directory.EnumerateDirectories(directory))
            try
            {
                SearchDirectory(dir);
            }
            catch (Exception)
            {
                // ignored
            }
    }

    private void CheckFileForDelete(string file)
    {
        var fileName = Path.GetFileName(file);
        if (fileName != null && !fileName.StartsWith(".")) return;
        MoveFileToRecycleBin(file);
        _countFiles++;
    }

    private void MoveFileToRecycleBin(string path)
    {
        FileSystem.DeleteFile(path,
            UIOption.OnlyErrorDialogs,
            RecycleOption.SendToRecycleBin);
    }

    private void EvaluateResult(object sender, RunWorkerCompletedEventArgs e)
    {
        var filesDeletedCaption = _lang.GetWord("FilesDeletedCaption");
        var filesDeletedText = _lang.GetWord("FilesDeletedText");
        MessageBox.Show(_countFiles + @" " + filesDeletedText, filesDeletedCaption, MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        LockGui(false);
    }

    private void StartBackgroundScan()
    {
        if (!CheckFolderSelected()) return;
        ResetCounter();
        LockGui(true);
        _worker.RunWorkerAsync();
    }

    private void ResetCounter()
    {
        _countFiles = 0;
    }

    private bool CheckFolderSelected()
    {
        if (!string.IsNullOrWhiteSpace(richTextBoxDirectory.Text)) return true;
        var noFolderSelectedCaption = _lang.GetWord("NoFolderSelectedCaption");
        var noFolderSelectedText = _lang.GetWord("NoFolderSelectedText");
        MessageBox.Show(noFolderSelectedText, noFolderSelectedCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }

    private void LockGui(bool locked)
    {
        buttonChooseDirectory.Enabled = !locked;
        buttonStart.Enabled = !locked;
    }

    private void buttonChooseDirectory_Click(object sender, EventArgs e)
    {
        var dialog = new FolderBrowserDialog();
        var result = dialog.ShowDialog();
        if (result == DialogResult.OK)
            richTextBoxDirectory.Text = dialog.SelectedPath;
    }

    private void buttonStart_Click(object sender, EventArgs e)
    {
        try
        {
            StartBackgroundScan();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}