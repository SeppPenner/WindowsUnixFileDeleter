partial class Main
{
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.buttonChooseDirectory = new System.Windows.Forms.Button();
            this.richTextBoxDirectory = new System.Windows.Forms.RichTextBox();
            this.labelDirectory = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonChooseDirectory
            // 
            this.buttonChooseDirectory.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonChooseDirectory.Location = new System.Drawing.Point(3, 3);
            this.buttonChooseDirectory.Name = "buttonChooseDirectory";
            this.buttonChooseDirectory.Size = new System.Drawing.Size(110, 24);
            this.buttonChooseDirectory.TabIndex = 0;
            this.buttonChooseDirectory.Text = "Verzeichnis wählen";
            this.buttonChooseDirectory.UseVisualStyleBackColor = true;
            this.buttonChooseDirectory.Click += new System.EventHandler(this.buttonChooseDirectory_Click);
            // 
            // richTextBoxDirectory
            // 
            this.richTextBoxDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDirectory.Location = new System.Drawing.Point(3, 63);
            this.richTextBoxDirectory.Name = "richTextBoxDirectory";
            this.richTextBoxDirectory.ReadOnly = true;
            this.richTextBoxDirectory.Size = new System.Drawing.Size(253, 139);
            this.richTextBoxDirectory.TabIndex = 1;
            this.richTextBoxDirectory.Text = "";
            // 
            // labelDirectory
            // 
            this.labelDirectory.AutoSize = true;
            this.labelDirectory.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelDirectory.Location = new System.Drawing.Point(3, 47);
            this.labelDirectory.Name = "labelDirectory";
            this.labelDirectory.Size = new System.Drawing.Size(253, 13);
            this.labelDirectory.TabIndex = 2;
            this.labelDirectory.Text = "Verzeichnis:";
            // 
            // buttonStart
            // 
            this.buttonStart.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStart.Location = new System.Drawing.Point(262, 33);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(106, 24);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.buttonChooseDirectory, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelDirectory, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxDirectory, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonStart, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxLanguage, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(371, 205);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(262, 3);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(106, 21);
            this.comboBoxLanguage.TabIndex = 4;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 205);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(350, 244);
            this.Name = "Main";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonChooseDirectory;
    private System.Windows.Forms.RichTextBox richTextBoxDirectory;
    private System.Windows.Forms.Label labelDirectory;
    private System.Windows.Forms.Button buttonStart;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.ComboBox comboBoxLanguage;
}