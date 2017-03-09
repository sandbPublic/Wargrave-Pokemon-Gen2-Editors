using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // open save read write files

namespace Editor_Base_Class {
    /// <summary>
    /// <para>base class for various forms that edit pokemon generation 2 ROMs</para>
    /// <para>contains data on offsets; data structures; and loading, saving, </para>
    /// <para>and version selection methods; as well as other shared methods</para>
    /// </summary>
    public partial class Gen2Editor : Form {
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
    
        private void InitializeComponent() {
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
            this.menuStrip1.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openOffsets_TSMI,
            this.openROM_TSMI,
            this.saveROM_TSMI,
            this.managePtrs_TSMI,
            this.importData_TSMI,
            this.exportData_TSMI});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(66, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openOffsets_TSMI
            // 
            this.openOffsets_TSMI.Name = "openOffsets_TSMI";
            this.openOffsets_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.openOffsets_TSMI.Size = new System.Drawing.Size(321, 26);
            this.openOffsets_TSMI.Text = "Open offsets";
            this.openOffsets_TSMI.Click += new System.EventHandler(this.openOffsets_TSMI_Click);
            // 
            // openROM_TSMI
            // 
            this.openROM_TSMI.Name = "openROM_TSMI";
            this.openROM_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openROM_TSMI.Size = new System.Drawing.Size(321, 26);
            this.openROM_TSMI.Text = "Open ROM";
            this.openROM_TSMI.Click += new System.EventHandler(this.openROM_TSMI_Click);
            // 
            // saveROM_TSMI
            // 
            this.saveROM_TSMI.Enabled = false;
            this.saveROM_TSMI.Name = "saveROM_TSMI";
            this.saveROM_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveROM_TSMI.Size = new System.Drawing.Size(321, 26);
            this.saveROM_TSMI.Text = "Save ROM";
            this.saveROM_TSMI.Click += new System.EventHandler(this.saveROM_TSMI_Click);
            // 
            // managePtrs_TSMI
            // 
            this.managePtrs_TSMI.Enabled = false;
            this.managePtrs_TSMI.Name = "managePtrs_TSMI";
            this.managePtrs_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.managePtrs_TSMI.Size = new System.Drawing.Size(321, 26);
            this.managePtrs_TSMI.Text = "Manage pointers";
            this.managePtrs_TSMI.Click += new System.EventHandler(this.managePtrs_TSMI_Click);
            // 
            // importData_TSMI
            // 
            this.importData_TSMI.Name = "importData_TSMI";
            this.importData_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importData_TSMI.Size = new System.Drawing.Size(321, 26);
            this.importData_TSMI.Text = "Import data";
            this.importData_TSMI.Click += new System.EventHandler(this.importData_TSMI_Click);
            // 
            // exportData_TSMI
            // 
            this.exportData_TSMI.Enabled = false;
            this.exportData_TSMI.Name = "exportData_TSMI";
            this.exportData_TSMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportData_TSMI.Size = new System.Drawing.Size(321, 26);
            this.exportData_TSMI.Text = "Export data";
            this.exportData_TSMI.Click += new System.EventHandler(this.exportData_TSMI_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.about_TSMI});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(66, 25);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // about_TSMI
            // 
            this.about_TSMI.Name = "about_TSMI";
            this.about_TSMI.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.about_TSMI.Size = new System.Drawing.Size(167, 26);
            this.about_TSMI.Text = "About";
            this.about_TSMI.Click += new System.EventHandler(this.about_TSMI_Click);
            // 
            // GSCEditor
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GSCEditor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void openOffsets_TSMI_Click(object sender, EventArgs e) {
            loadOffsets();
        }

        private void openROM_TSMI_Click(object sender, EventArgs e) {
            if (offsets_FilePath == null) {
                openOffsets_TSMI_Click(sender, e);
            }
            if (offsets_FilePath != null) {
                loadFrom(ofdROM, true);
            }
        }

        // requires ROM data for immutable data
        // eg for move editor, must load type names
        // for moveset editor, must load pokemon and move names
        // these data are not saved in the data.txt and should not be
        // for portability
        // eg moveset data, when transfered, should not CAP or unCAP names
        private void importData_TSMI_Click(object sender, EventArgs e) {
            if (ROM_FilePath == null) {
                openROM_TSMI_Click(sender, e);
            }
            if (ROM_FilePath != null) {
                loadFrom(ofdData, false);
            } 
        }

        private void saveROM_TSMI_Click(object sender, EventArgs e) {
            try {
                ROM_File = new ROM_FileStream(ROM_FilePath, FileMode.Open);
                #region ITEM
                if (jumpToIfSaving(ITEM_STRUCT_I)) {
                    for (int item_i = 1; item_i <= offset[NUM_OF_ITEMS_I]; item_i++) {
                        for (int byte_i = 0; byte_i < 7; byte_i++) {
                            ROM_File.WriteByte(items[item_i, byte_i]);
                        }
                    }
                }

                if (jumpToIfSaving(ITEM_ASM_I)) {
                    for (int asm_i = 1; asm_i <= offset[LAST_NON_TM_ITEM_I]; asm_i++) {
                        ROM_File.WriteByte((byte)(itemASM[asm_i] / 0x100));
                        ROM_File.WriteByte((byte)(itemASM[asm_i] % 0x100));
                    }
                }
                if (jumpToIfSaving(ITEM_NAME_I)) {
                    itemNames.writeToFile(ROM_File);
                }
                if (jumpToIfSaving(ITEM_DESC_PTR_I)) {
                    itemDescs.writeToFile(ROM_File);
                }
                #endregion
                #region MOVE
                if (jumpToIfSaving(MOVE_STRUCT_I)) {
                    for (int move_i = 1; move_i <= offset[NUM_OF_MOVES_I]; move_i++) {
                        for (int byte_j = 0; byte_j < 7; byte_j++) {
                            ROM_File.WriteByte(moves[move_i, byte_j]);
                        }
                    }
                }

                if (jumpToIfSaving(MOVE_NAME_I)) {
                    moveNames.writeToFile(ROM_File);
                }
                if (jumpToIfSaving(MOVE_DESC_PTR_I)) {
                    moveDescs.writeToFile(ROM_File);
                }

                if (jumpToIfSaving(CRIT_LIST_PTR_I)) {
                    ROM_File.writeLocalGBCPtr(offset[NEW_CRIT_LIST_I]);

                    ROM_File.Position = offset[NEW_CRIT_LIST_I];
                    for (int move_i = 0; move_i < offset[NUM_OF_MOVES_I]; move_i++) {
                        if (moveIsCrit[move_i]) {
                            ROM_File.WriteByte((byte)move_i);
                        }
                    }
                    ROM_File.WriteByte(0xFF);
                }
                #endregion
                #region MOVESET
                if (jumpToIfSaving(MOVESET_PTR_I)) {
                    movesets.writeToFile(ROM_File);
                }

                if (jumpToIfSaving(TM_SET_I)) {
                    for (int pkmn_i = 1; pkmn_i <= offset[NUM_OF_PKMN_I]; pkmn_i++) {
                        bool[] TMsetBools = new bool[64];
                        for (int bool_j = 0; bool_j < 64; bool_j++) {
                            TMsetBools[bool_j] = TMSets[pkmn_i, bool_j];
                        }
                        byte[] TMsetBytes = ROM_FileStream.TMBytesFromBools(TMsetBools);
                        ROM_File.writeBytes(TMsetBytes, offset[TM_SET_I] + 0x20 * (pkmn_i - 1));
                    }
                }
                #endregion
                #region TRAINER
                if (jumpToIfSaving(TR_CLASS_NAME_I)) {
                    trClassNames.writeToFile(ROM_File);
                }

                if (jumpToIfSaving(TR_CLASS_DV_I)) {
                    foreach (byte DV in trClassDVs) {
                        ROM_File.WriteByte(DV);
                    }
                }

                if (jumpToIfSaving(TR_CLASS_ATTRIBUTE_I)) {
                    for (int tc_i = trClassNames.start_i; tc_i <= trClassNames.end_i; tc_i++) {
                        ROM_File.WriteByte(trClassItems[2 * tc_i]);
                        ROM_File.WriteByte(trClassItems[2 * tc_i + 1]);
                        ROM_File.WriteByte(trClassRewards[tc_i]);
                        ROM_File.Position += 4; // skip AI behavior bytes
                    }
                }

                if (jumpToIfSaving(TRAINER_PTR_I)) {
                    trainerLists.writeToFile(ROM_File);
                }
                #endregion
                if (jumpToIfSaving(ANIM_PTR_I)) {
                    animations.writeToFile(ROM_File);
                }
                #region WILD
                if (jumpToIfSaving(WILD_I)) {
                    ROM_File.writeWildAreaList(johtoLand);
                    ROM_File.writeWildAreaList(johtoWater);
                    ROM_File.writeWildAreaList(kantoLand);
                    ROM_File.writeWildAreaList(kantoWater);
                    ROM_File.writeWildAreaList(swarm);
                }
                #endregion

                ROM_File.Dispose();

                FormMessage saved = new FormMessage("Saved to " + ROM_FilePath);
                saved.Show();
            } catch (FileNotFoundException) {
                FormMessage exception = new FormMessage("Must select existing ROM file");
                exception.Show();
            }

        }

        private void exportData_TSMI_Click(object sender, EventArgs e) {
            if (sfdData.ShowDialog() == DialogResult.OK) {
                data_FilePath = sfdData.FileName;
                sfdData.Dispose();

                exportData();
            }
        }

        private void about_TSMI_Click(object sender, EventArgs e) {
            FormMessage info = new FormMessage("https://hax.iimarck.us/topic/6848/");
            info.Show();
        }

        private void managePtrs_TSMI_Click(object sender, EventArgs e) {
            managePointers();
        }
    }

    public class FormMessage : Form {
        private TextBox txtMessage;

        public FormMessage(string msg) {
            Font = new System.Drawing.Font("Courier New", 14F);
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Name = "formMessage";
            ResumeLayout(false);
            PerformLayout();

            txtMessage = new TextBox();
            txtMessage.Font = new System.Drawing.Font("Courier New", 14.25F);
            txtMessage.Location = new System.Drawing.Point(10, 10);
            
            txtMessage.Multiline = true;
            txtMessage.BorderStyle = BorderStyle.None;
            txtMessage.Name = "message";
            txtMessage.Text = msg;
            txtMessage.ReadOnly = true;

            int maxLength = 0;
            foreach (string s in txtMessage.Lines) {
                if (s.Length > maxLength) maxLength = s.Length;
            }

            int height = txtMessage.Lines.Length;

            ClientSize = new System.Drawing.Size(maxLength * 12 + 10, height*28 + 15);
            txtMessage.Size = new System.Drawing.Size(maxLength * 12, height*28);

            Controls.Add(txtMessage);
        }
    }

    public class SortingString {
        public int sortValue;
        public string me;
    }

    public class FormAnalysis : Form {
        private RichTextBox textAnalysis;

        public FormAnalysis(List<SortingString> L_ss) {
            List<SortingString> sortedL_ss = L_ss.OrderBy(o => -o.sortValue).ToList();
            List<string> analysis = new List<string>();
            foreach (SortingString ss in sortedL_ss) {
                analysis.Add(ss.me);
            }

            Font = new System.Drawing.Font("Courier New", 14F);
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            ResumeLayout(false);
            PerformLayout();

            textAnalysis = new RichTextBox();
            textAnalysis.Font = new System.Drawing.Font("Courier New", 14.25F);
            textAnalysis.Location = new System.Drawing.Point(10, 10);
            textAnalysis.ReadOnly = true;
            
            textAnalysis.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            textAnalysis.Multiline = true;
            foreach (string s in analysis) {
                textAnalysis.Text += s + Environment.NewLine;
            }

            // dimensions
            int maxLength = 0;
            foreach (string s in textAnalysis.Lines) {
                if (s.Length > maxLength) maxLength = s.Length;
            }
            int height = Math.Min(textAnalysis.Lines.Length*28, 400);
            ClientSize = new System.Drawing.Size(maxLength * 12 + 30, height + 15);
            textAnalysis.Size = new System.Drawing.Size(maxLength * 12 + 20, height);

            Controls.Add(textAnalysis);
        }
    }
}