using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // open save read write files

namespace Editor_Base_Class
{
    /// <summary>
    /// <para>base class for various forms that edit pokemon generation 2 ROMs</para>
    /// <para>contains data on offsets; data structures; and loading, saving, </para>
    /// <para>and version selection methods; as well as other shared methods</para>
    /// </summary>
    public partial class Gen2Editor : Form
    {
        private MenuStrip menuStrip1;

        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openOffsets_TSMI;
        private ToolStripMenuItem openROM_TSMI;
        protected ToolStripMenuItem saveROM_TSMI;
        protected ToolStripMenuItem importData_TSMI;
        protected ToolStripMenuItem exportData_TSMI;
        protected ToolStripMenuItem managePtrs_TSMI;

        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem about_TSMI;

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openOffsets_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.openROM_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.saveROM_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.managePtrs_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.importData_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.exportData_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.about_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openOffsets_TSMI,
            this.openROM_TSMI,
            this.saveROM_TSMI,
            this.importData_TSMI,
            this.exportData_TSMI,
            this.managePtrs_TSMI});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(62, 26);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openOffsets_TSMI
            // 
            this.openOffsets_TSMI.Name = "openOffsets_TSMI";
            this.openOffsets_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.openOffsets_TSMI.Size = new System.Drawing.Size(300, 26);
            this.openOffsets_TSMI.Text = "Open offsets";
            this.openOffsets_TSMI.Click += new System.EventHandler(this.OpenOffsets_TSMI_Click);
            // 
            // openROM_TSMI
            // 
            this.openROM_TSMI.Enabled = false;
            this.openROM_TSMI.Name = "openROM_TSMI";
            this.openROM_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openROM_TSMI.Size = new System.Drawing.Size(300, 26);
            this.openROM_TSMI.Text = "Open ROM";
            this.openROM_TSMI.Click += new System.EventHandler(this.OpenROM_TSMI_Click);
            // 
            // saveROM_TSMI
            // 
            this.saveROM_TSMI.Enabled = false;
            this.saveROM_TSMI.Name = "saveROM_TSMI";
            this.saveROM_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveROM_TSMI.Size = new System.Drawing.Size(300, 26);
            this.saveROM_TSMI.Text = "Save ROM";
            this.saveROM_TSMI.Click += new System.EventHandler(this.SaveROM_TSMI_Click);
            // 
            // managePtrs_TSMI
            // 
            this.managePtrs_TSMI.Enabled = false;
            this.managePtrs_TSMI.Name = "managePtrs_TSMI";
            this.managePtrs_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.managePtrs_TSMI.Size = new System.Drawing.Size(300, 26);
            this.managePtrs_TSMI.Text = "Manage pointers";
            this.managePtrs_TSMI.Click += new System.EventHandler(this.ManagePtrs_TSMI_Click);
            // 
            // importData_TSMI
            // 
            this.importData_TSMI.Name = "importData_TSMI";
            this.importData_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importData_TSMI.Size = new System.Drawing.Size(300, 26);
            this.importData_TSMI.Text = "Import data";
            this.importData_TSMI.Click += new System.EventHandler(this.ImportData_TSMI_Click);
            // 
            // exportData_TSMI
            // 
            this.exportData_TSMI.Enabled = false;
            this.exportData_TSMI.Name = "exportData_TSMI";
            this.exportData_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportData_TSMI.Size = new System.Drawing.Size(300, 26);
            this.exportData_TSMI.Text = "Export data";
            this.exportData_TSMI.Click += new System.EventHandler(this.ExportData_TSMI_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.about_TSMI});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(62, 26);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // about_TSMI
            // 
            this.about_TSMI.Name = "about_TSMI";
            this.about_TSMI.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.about_TSMI.Size = new System.Drawing.Size(160, 26);
            this.about_TSMI.Text = "About";
            this.about_TSMI.Click += new System.EventHandler(this.About_TSMI_Click);
            // 
            // Gen2Editor
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Gen2Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void OpenOffsets_TSMI_Click(object sender, EventArgs e)
        {
            LoadOffsets();
        }

        private void OpenROM_TSMI_Click(object sender, EventArgs e)
        {
            LoadFromROM();
        }

        private void SaveROM_TSMI_Click(object sender, EventArgs e)
        {
            SaveToROM();
        }

        // requires ROM data for immutable data
        // eg for move editor, must load type names
        // for moveset editor, must load pokemon and move names
        // these data are not saved in the data.txt and should not be
        // for portability
        // eg moveset data, when transfered, should not CAP or unCAP names
        private void ImportData_TSMI_Click(object sender, EventArgs e)
        {
            if (openROM_TSMI.Enabled) LoadFromTxt();
        }

        private void ExportData_TSMI_Click(object sender, EventArgs e)
        {
            SaveToTxt();
        }

        private void About_TSMI_Click(object sender, EventArgs e)
        {
            new FormMessage("https://hax.iimarck.us/topic/6848/").Show();
        }

        private void ManagePtrs_TSMI_Click(object sender, EventArgs e)
        {
            ManagePointers();
        }
    }

    public class FormMessage : Form
    {
        private TextBox txtMessage;

        public FormMessage(string msg)
        {
            Font = new System.Drawing.Font("Consolas", 14F);
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Name = "formMessage";
            ResumeLayout(false);
            PerformLayout();

            txtMessage = new TextBox
            {
                Font = new System.Drawing.Font("Consolas", 14.25F),
                Location = new System.Drawing.Point(10, 10),

                Multiline = true,
                BorderStyle = BorderStyle.None,
                Name = "message",
                Text = msg,
                ReadOnly = true
            };

            int maxLength = 0;
            foreach (string s in txtMessage.Lines)
            {
                if (s.Length > maxLength) maxLength = s.Length;
            }

            int height = txtMessage.Lines.Length;

            ClientSize = new System.Drawing.Size(maxLength * 12 + 10, height * 28 + 15); // todo establish char dimensions
            txtMessage.Size = new System.Drawing.Size(maxLength * 12, height * 28);

            Controls.Add(txtMessage);
        }
    }

    public class SortingString
    {
        public int sortValue;
        public string me;
    }

    public class FormAnalysis : Form
    {
        private RichTextBox textAnalysis;

        public FormAnalysis(List<SortingString> L_ss)
        {
            List<SortingString> sortedL_ss = L_ss.OrderBy(o => -o.sortValue).ToList();
            var analysis = new List<string>();
            foreach (SortingString ss in sortedL_ss) analysis.Add(ss.me);

            Font = new System.Drawing.Font("Consolas", 14F);
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            ResumeLayout(false);
            PerformLayout();

            textAnalysis = new RichTextBox
            {
                Font = new System.Drawing.Font("Consolas", 14.25F),
                Location = new System.Drawing.Point(10, 10),
                ReadOnly = true,

                ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical,
                Multiline = true
            };
            foreach (string s in analysis) textAnalysis.Text += s + Environment.NewLine;

            // dimensions
            int maxLength = 0;
            foreach (string s in textAnalysis.Lines)
            {
                if (s.Length > maxLength) maxLength = s.Length;
            }
            int height = Math.Min(textAnalysis.Lines.Length * 28, 400);
            ClientSize = new System.Drawing.Size(maxLength * 12 + 30, height + 15);
            textAnalysis.Size = new System.Drawing.Size(maxLength * 12 + 20, height);

            Controls.Add(textAnalysis);
        }
    }
}