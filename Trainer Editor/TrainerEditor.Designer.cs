namespace Gen2_Trainer_Editor {
    partial class TrainerEditor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeArrayComponents() {
            // construct
            spinLevels = new System.Windows.Forms.NumericUpDown[6];
            cboxSpecies = new System.Windows.Forms.ComboBox[6]; 
            cboxItems = new System.Windows.Forms.ComboBox[6];
            cboxMoves = new System.Windows.Forms.ComboBox[6,4];

            for (int init_i = 0; init_i < 6; init_i++) {
                spinLevels[init_i] = new System.Windows.Forms.NumericUpDown();
                cboxSpecies[init_i] = new System.Windows.Forms.ComboBox();
                cboxItems[init_i] = new System.Windows.Forms.ComboBox();
                for (int init_j = 0; init_j < 4; init_j++) {
                    cboxMoves[init_i, init_j] = new System.Windows.Forms.ComboBox();
                }
            }

            // include in group box?

            // position etc
            for (int pos_i = 0; pos_i < 6; pos_i++) {
                int rowHeight = 35;
                int yCoord = 76 + pos_i * rowHeight;

                spinLevels[pos_i].Name = "spinLevels" + pos_i.ToString();
                spinLevels[pos_i].Location = new System.Drawing.Point(7, yCoord);
                spinLevels[pos_i].Size = new System.Drawing.Size(53, rowHeight-6);
                spinLevels[pos_i].Maximum = new decimal(new int[] {255,0,0,0});
                spinLevels[pos_i].Enabled = false;
                spinLevels[pos_i].TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                spinLevels[pos_i].ValueChanged += new System.EventHandler(SpinLevels_ValueChanged);
                gboxTrTeam.Controls.Add(spinLevels[pos_i]);

                cboxSpecies[pos_i].FormattingEnabled = true;
                cboxSpecies[pos_i].Name = "cboxSpecies" + pos_i.ToString();
                cboxSpecies[pos_i].Location = new System.Drawing.Point(66, yCoord);
                cboxSpecies[pos_i].Size = new System.Drawing.Size(133, rowHeight-6);
                cboxSpecies[pos_i].Enabled = false;
                cboxSpecies[pos_i].SelectedIndexChanged += 
                    new System.EventHandler(CboxSpecies_SelectedIndexChanged);
                gboxTrTeam.Controls.Add(cboxSpecies[pos_i]);

                cboxItems[pos_i].FormattingEnabled = true;
                cboxItems[pos_i].Name = "cboxItems" + pos_i.ToString();
                cboxItems[pos_i].Location = new System.Drawing.Point(205, yCoord);
                cboxItems[pos_i].Size = new System.Drawing.Size(153, rowHeight-6);
                cboxItems[pos_i].Enabled = false;
                cboxItems[pos_i].SelectedIndexChanged +=
                    new System.EventHandler(CboxItems_SelectedIndexChanged);
                gboxTrTeam.Controls.Add(cboxItems[pos_i]);

                for (int pos_j = 0; pos_j < 4; pos_j++) {
                    cboxMoves[pos_i,pos_j].FormattingEnabled = true;
                    cboxMoves[pos_i, pos_j].Name = "cboxMoves" + pos_i.ToString() + pos_j.ToString();
                    cboxMoves[pos_i, pos_j].Location = new System.Drawing.Point(364 + pos_j*164, yCoord);                    
                    cboxMoves[pos_i, pos_j].Size = new System.Drawing.Size(157, 29);
                    cboxMoves[pos_i, pos_j].Enabled = false;
                    cboxMoves[pos_i, pos_j].SelectedIndexChanged +=
                        new System.EventHandler(CboxMoves_SelectedIndexChanged);
                    gboxTrTeam.Controls.Add(cboxMoves[pos_i, pos_j]);
                }
            }

            // add to group box
            //groupTrTeam.Controls.Add(this.checkMoveset);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainerEditor));
            this.tboxFreeTrBytes = new System.Windows.Forms.TextBox();
            this.spinReward = new System.Windows.Forms.NumericUpDown();
            this.spinDVs1 = new System.Windows.Forms.NumericUpDown();
            this.spinDVs0 = new System.Windows.Forms.NumericUpDown();
            this.cboxItems1 = new System.Windows.Forms.ComboBox();
            this.cboxItems0 = new System.Windows.Forms.ComboBox();
            this.gboxTrGroup = new System.Windows.Forms.GroupBox();
            this.buttonAnalyze = new System.Windows.Forms.Button();
            this.txtGroupPtr = new System.Windows.Forms.TextBox();
            this.gboxDVs = new System.Windows.Forms.GroupBox();
            this.tboxDVlabel = new System.Windows.Forms.TextBox();
            this.tboxDVinfo = new System.Windows.Forms.TextBox();
            this.tboxHPDV = new System.Windows.Forms.TextBox();
            this.buttonRemoveTrainer = new System.Windows.Forms.Button();
            this.tboxReward = new System.Windows.Forms.TextBox();
            this.buttonAddTrainer = new System.Windows.Forms.Button();
            this.tboxGroupName = new System.Windows.Forms.TextBox();
            this.spinGroup = new System.Windows.Forms.NumericUpDown();
            this.tboxFreeGroupBytes = new System.Windows.Forms.TextBox();
            this.tboxTrainerCount = new System.Windows.Forms.TextBox();
            this.gboxTrTeam = new System.Windows.Forms.GroupBox();
            this.checkMoveset = new System.Windows.Forms.CheckBox();
            this.checkItems = new System.Windows.Forms.CheckBox();
            this.tboxTrainerName = new System.Windows.Forms.TextBox();
            this.spinTrainerTeamID = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.spinReward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDVs1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDVs0)).BeginInit();
            this.gboxTrGroup.SuspendLayout();
            this.gboxDVs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinGroup)).BeginInit();
            this.gboxTrTeam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinTrainerTeamID)).BeginInit();
            this.SuspendLayout();
            // 
            // tboxFreeTrBytes
            // 
            this.tboxFreeTrBytes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tboxFreeTrBytes.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxFreeTrBytes.Location = new System.Drawing.Point(570, 29);
            this.tboxFreeTrBytes.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tboxFreeTrBytes.Name = "tboxFreeTrBytes";
            this.tboxFreeTrBytes.ReadOnly = true;
            this.tboxFreeTrBytes.Size = new System.Drawing.Size(300, 22);
            this.tboxFreeTrBytes.TabIndex = 12;
            this.tboxFreeTrBytes.Text = "0 trainer bytes free";
            // 
            // spinReward
            // 
            this.spinReward.Enabled = false;
            this.spinReward.Location = new System.Drawing.Point(671, 24);
            this.spinReward.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.spinReward.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinReward.Name = "spinReward";
            this.spinReward.Size = new System.Drawing.Size(58, 29);
            this.spinReward.TabIndex = 0;
            this.spinReward.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinReward.ValueChanged += new System.EventHandler(this.SpinReward_ValueChanged);
            // 
            // spinDVs1
            // 
            this.spinDVs1.Enabled = false;
            this.spinDVs1.Hexadecimal = true;
            this.spinDVs1.Location = new System.Drawing.Point(95, 51);
            this.spinDVs1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinDVs1.Name = "spinDVs1";
            this.spinDVs1.Size = new System.Drawing.Size(48, 29);
            this.spinDVs1.TabIndex = 1;
            this.spinDVs1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinDVs1.ValueChanged += new System.EventHandler(this.SpinDVs1_ValueChanged);
            // 
            // spinDVs0
            // 
            this.spinDVs0.Enabled = false;
            this.spinDVs0.Hexadecimal = true;
            this.spinDVs0.Location = new System.Drawing.Point(41, 51);
            this.spinDVs0.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinDVs0.Name = "spinDVs0";
            this.spinDVs0.Size = new System.Drawing.Size(48, 29);
            this.spinDVs0.TabIndex = 0;
            this.spinDVs0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinDVs0.ValueChanged += new System.EventHandler(this.SpinDVs0_ValueChanged);
            // 
            // cboxItems1
            // 
            this.cboxItems1.Enabled = false;
            this.cboxItems1.FormattingEnabled = true;
            this.cboxItems1.Location = new System.Drawing.Point(570, 95);
            this.cboxItems1.Name = "cboxItems1";
            this.cboxItems1.Size = new System.Drawing.Size(159, 29);
            this.cboxItems1.TabIndex = 1;
            this.cboxItems1.SelectedIndexChanged += new System.EventHandler(this.CboxItems1_SelectedIndexChanged);
            // 
            // cboxItems0
            // 
            this.cboxItems0.Enabled = false;
            this.cboxItems0.FormattingEnabled = true;
            this.cboxItems0.Location = new System.Drawing.Point(570, 60);
            this.cboxItems0.Name = "cboxItems0";
            this.cboxItems0.Size = new System.Drawing.Size(159, 29);
            this.cboxItems0.TabIndex = 0;
            this.cboxItems0.SelectedIndexChanged += new System.EventHandler(this.CboxItems0_SelectedIndexChanged);
            // 
            // gboxTrGroup
            // 
            this.gboxTrGroup.Controls.Add(this.buttonAnalyze);
            this.gboxTrGroup.Controls.Add(this.txtGroupPtr);
            this.gboxTrGroup.Controls.Add(this.gboxDVs);
            this.gboxTrGroup.Controls.Add(this.buttonRemoveTrainer);
            this.gboxTrGroup.Controls.Add(this.tboxReward);
            this.gboxTrGroup.Controls.Add(this.cboxItems0);
            this.gboxTrGroup.Controls.Add(this.buttonAddTrainer);
            this.gboxTrGroup.Controls.Add(this.cboxItems1);
            this.gboxTrGroup.Controls.Add(this.tboxGroupName);
            this.gboxTrGroup.Controls.Add(this.spinReward);
            this.gboxTrGroup.Controls.Add(this.spinGroup);
            this.gboxTrGroup.Controls.Add(this.tboxFreeGroupBytes);
            this.gboxTrGroup.Controls.Add(this.tboxTrainerCount);
            this.gboxTrGroup.Location = new System.Drawing.Point(12, 32);
            this.gboxTrGroup.Name = "gboxTrGroup";
            this.gboxTrGroup.Size = new System.Drawing.Size(1039, 140);
            this.gboxTrGroup.TabIndex = 18;
            this.gboxTrGroup.TabStop = false;
            this.gboxTrGroup.Text = "Trainer Group";
            // 
            // buttonAnalyze
            // 
            this.buttonAnalyze.Enabled = false;
            this.buttonAnalyze.Location = new System.Drawing.Point(897, 35);
            this.buttonAnalyze.Name = "buttonAnalyze";
            this.buttonAnalyze.Size = new System.Drawing.Size(117, 80);
            this.buttonAnalyze.TabIndex = 39;
            this.buttonAnalyze.Text = "Analyze Pokemon usage";
            this.buttonAnalyze.UseVisualStyleBackColor = true;
            this.buttonAnalyze.Click += new System.EventHandler(this.ButtonAnalyze_Click);
            // 
            // txtGroupPtr
            // 
            this.txtGroupPtr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGroupPtr.Location = new System.Drawing.Point(14, 102);
            this.txtGroupPtr.Name = "txtGroupPtr";
            this.txtGroupPtr.ReadOnly = true;
            this.txtGroupPtr.Size = new System.Drawing.Size(234, 29);
            this.txtGroupPtr.TabIndex = 38;
            this.txtGroupPtr.TabStop = false;
            // 
            // gboxDVs
            // 
            this.gboxDVs.Controls.Add(this.tboxDVlabel);
            this.gboxDVs.Controls.Add(this.tboxDVinfo);
            this.gboxDVs.Controls.Add(this.tboxHPDV);
            this.gboxDVs.Controls.Add(this.spinDVs0);
            this.gboxDVs.Controls.Add(this.spinDVs1);
            this.gboxDVs.Location = new System.Drawing.Point(257, 28);
            this.gboxDVs.Name = "gboxDVs";
            this.gboxDVs.Size = new System.Drawing.Size(300, 87);
            this.gboxDVs.TabIndex = 24;
            this.gboxDVs.TabStop = false;
            this.gboxDVs.Text = "DVs";
            // 
            // tboxDVlabel
            // 
            this.tboxDVlabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tboxDVlabel.Enabled = false;
            this.tboxDVlabel.Location = new System.Drawing.Point(7, 23);
            this.tboxDVlabel.Name = "tboxDVlabel";
            this.tboxDVlabel.Size = new System.Drawing.Size(136, 22);
            this.tboxDVlabel.TabIndex = 4;
            this.tboxDVlabel.Text = "HP A,D S,Spc";
            // 
            // tboxDVinfo
            // 
            this.tboxDVinfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tboxDVinfo.Location = new System.Drawing.Point(155, 26);
            this.tboxDVinfo.Multiline = true;
            this.tboxDVinfo.Name = "tboxDVinfo";
            this.tboxDVinfo.ReadOnly = true;
            this.tboxDVinfo.Size = new System.Drawing.Size(136, 50);
            this.tboxDVinfo.TabIndex = 3;
            // 
            // tboxHPDV
            // 
            this.tboxHPDV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tboxHPDV.Location = new System.Drawing.Point(7, 51);
            this.tboxHPDV.Name = "tboxHPDV";
            this.tboxHPDV.ReadOnly = true;
            this.tboxHPDV.Size = new System.Drawing.Size(28, 29);
            this.tboxHPDV.TabIndex = 2;
            // 
            // buttonRemoveTrainer
            // 
            this.buttonRemoveTrainer.Enabled = false;
            this.buttonRemoveTrainer.Location = new System.Drawing.Point(751, 93);
            this.buttonRemoveTrainer.Name = "buttonRemoveTrainer";
            this.buttonRemoveTrainer.Size = new System.Drawing.Size(119, 33);
            this.buttonRemoveTrainer.TabIndex = 23;
            this.buttonRemoveTrainer.Text = "-Trainer";
            this.buttonRemoveTrainer.UseVisualStyleBackColor = true;
            this.buttonRemoveTrainer.Click += new System.EventHandler(this.ButtonRemoveTrainer_Click);
            // 
            // tboxReward
            // 
            this.tboxReward.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tboxReward.Enabled = false;
            this.tboxReward.Location = new System.Drawing.Point(570, 26);
            this.tboxReward.Name = "tboxReward";
            this.tboxReward.Size = new System.Drawing.Size(92, 22);
            this.tboxReward.TabIndex = 20;
            this.tboxReward.Text = "Reward:";
            // 
            // buttonAddTrainer
            // 
            this.buttonAddTrainer.Enabled = false;
            this.buttonAddTrainer.Location = new System.Drawing.Point(751, 53);
            this.buttonAddTrainer.Name = "buttonAddTrainer";
            this.buttonAddTrainer.Size = new System.Drawing.Size(119, 34);
            this.buttonAddTrainer.TabIndex = 18;
            this.buttonAddTrainer.Text = "+Trainer";
            this.buttonAddTrainer.UseVisualStyleBackColor = true;
            this.buttonAddTrainer.Click += new System.EventHandler(this.ButtonAddTrainer_Click);
            // 
            // tboxGroupName
            // 
            this.tboxGroupName.Enabled = false;
            this.tboxGroupName.Location = new System.Drawing.Point(79, 28);
            this.tboxGroupName.Name = "tboxGroupName";
            this.tboxGroupName.Size = new System.Drawing.Size(169, 29);
            this.tboxGroupName.TabIndex = 22;
            this.tboxGroupName.TextChanged += new System.EventHandler(this.TboxGroupName_TextChanged);
            // 
            // spinGroup
            // 
            this.spinGroup.Enabled = false;
            this.spinGroup.Location = new System.Drawing.Point(14, 28);
            this.spinGroup.Name = "spinGroup";
            this.spinGroup.Size = new System.Drawing.Size(58, 29);
            this.spinGroup.TabIndex = 21;
            this.spinGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinGroup.ValueChanged += new System.EventHandler(this.SpinGroup_ValueChanged);
            // 
            // tboxFreeTGBytes
            // 
            this.tboxFreeGroupBytes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tboxFreeGroupBytes.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxFreeGroupBytes.Location = new System.Drawing.Point(14, 65);
            this.tboxFreeGroupBytes.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tboxFreeGroupBytes.Name = "tboxFreeTGBytes";
            this.tboxFreeGroupBytes.ReadOnly = true;
            this.tboxFreeGroupBytes.Size = new System.Drawing.Size(234, 29);
            this.tboxFreeGroupBytes.TabIndex = 18;
            this.tboxFreeGroupBytes.Text = "0 group bytes free";
            // 
            // tboxTrainerCount
            // 
            this.tboxTrainerCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tboxTrainerCount.Location = new System.Drawing.Point(751, 19);
            this.tboxTrainerCount.Name = "tboxTrainerCount";
            this.tboxTrainerCount.ReadOnly = true;
            this.tboxTrainerCount.Size = new System.Drawing.Size(119, 29);
            this.tboxTrainerCount.TabIndex = 18;
            this.tboxTrainerCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gboxTrTeam
            // 
            this.gboxTrTeam.Controls.Add(this.checkMoveset);
            this.gboxTrTeam.Controls.Add(this.checkItems);
            this.gboxTrTeam.Controls.Add(this.tboxFreeTrBytes);
            this.gboxTrTeam.Controls.Add(this.tboxTrainerName);
            this.gboxTrTeam.Controls.Add(this.spinTrainerTeamID);
            this.gboxTrTeam.Location = new System.Drawing.Point(12, 189);
            this.gboxTrTeam.Name = "gboxTrTeam";
            this.gboxTrTeam.Size = new System.Drawing.Size(1039, 286);
            this.gboxTrTeam.TabIndex = 19;
            this.gboxTrTeam.TabStop = false;
            this.gboxTrTeam.Text = "Trainer Team";
            // 
            // checkMoveset
            // 
            this.checkMoveset.AutoSize = true;
            this.checkMoveset.Enabled = false;
            this.checkMoveset.Location = new System.Drawing.Point(364, 29);
            this.checkMoveset.Name = "checkMoveset";
            this.checkMoveset.Size = new System.Drawing.Size(194, 25);
            this.checkMoveset.TabIndex = 17;
            this.checkMoveset.Text = "custom movesets";
            this.checkMoveset.UseVisualStyleBackColor = true;
            this.checkMoveset.CheckedChanged += new System.EventHandler(this.CheckMoveset_CheckedChanged);
            // 
            // checkItems
            // 
            this.checkItems.AutoSize = true;
            this.checkItems.Enabled = false;
            this.checkItems.Location = new System.Drawing.Point(221, 29);
            this.checkItems.Name = "checkItems";
            this.checkItems.Size = new System.Drawing.Size(128, 25);
            this.checkItems.TabIndex = 16;
            this.checkItems.Text = "has items";
            this.checkItems.UseVisualStyleBackColor = true;
            this.checkItems.CheckedChanged += new System.EventHandler(this.CheckItems_CheckedChanged);
            // 
            // tboxTrainerName
            // 
            this.tboxTrainerName.Enabled = false;
            this.tboxTrainerName.Location = new System.Drawing.Point(67, 29);
            this.tboxTrainerName.Name = "tboxTrainerName";
            this.tboxTrainerName.Size = new System.Drawing.Size(148, 29);
            this.tboxTrainerName.TabIndex = 1;
            this.tboxTrainerName.TextChanged += new System.EventHandler(this.TboxTrainerName_TextChanged);
            // 
            // spinTrainerTeamID
            // 
            this.spinTrainerTeamID.Enabled = false;
            this.spinTrainerTeamID.Location = new System.Drawing.Point(7, 29);
            this.spinTrainerTeamID.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinTrainerTeamID.Name = "spinTrainerTeamID";
            this.spinTrainerTeamID.Size = new System.Drawing.Size(53, 29);
            this.spinTrainerTeamID.TabIndex = 0;
            this.spinTrainerTeamID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinTrainerTeamID.ValueChanged += new System.EventHandler(this.SpinTrainerTeamID_ValueChanged);
            // 
            // TrainerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 487);
            this.Controls.Add(this.gboxTrTeam);
            this.Controls.Add(this.gboxTrGroup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "TrainerEditor";
            this.Text = "Wargrave Gen2 Trainer Editor";
            this.Controls.SetChildIndex(this.gboxTrGroup, 0);
            this.Controls.SetChildIndex(this.gboxTrTeam, 0);
            ((System.ComponentModel.ISupportInitialize)(this.spinReward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDVs1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDVs0)).EndInit();
            this.gboxTrGroup.ResumeLayout(false);
            this.gboxTrGroup.PerformLayout();
            this.gboxDVs.ResumeLayout(false);
            this.gboxDVs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinGroup)).EndInit();
            this.gboxTrTeam.ResumeLayout(false);
            this.gboxTrTeam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinTrainerTeamID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tboxFreeTrBytes;

        private System.Windows.Forms.GroupBox gboxTrGroup;
        private System.Windows.Forms.TextBox tboxTrainerCount;
        private System.Windows.Forms.TextBox tboxReward;
        private System.Windows.Forms.NumericUpDown spinReward;
        private System.Windows.Forms.NumericUpDown spinDVs1;
        private System.Windows.Forms.NumericUpDown spinDVs0;
        private System.Windows.Forms.ComboBox cboxItems0;
        private System.Windows.Forms.ComboBox cboxItems1;

        private System.Windows.Forms.GroupBox gboxTrTeam;
        private System.Windows.Forms.TextBox tboxTrainerName;
        private System.Windows.Forms.NumericUpDown spinTrainerTeamID;
        private System.Windows.Forms.CheckBox checkItems;
        private System.Windows.Forms.CheckBox checkMoveset;

        private System.Windows.Forms.NumericUpDown[] spinLevels;
        private System.Windows.Forms.ComboBox[] cboxSpecies;
        private System.Windows.Forms.ComboBox[] cboxItems;
        private System.Windows.Forms.ComboBox[,] cboxMoves;
        private System.Windows.Forms.TextBox tboxFreeGroupBytes;
        private System.Windows.Forms.TextBox tboxGroupName;
        private System.Windows.Forms.NumericUpDown spinGroup;
        private System.Windows.Forms.Button buttonRemoveTrainer;
        private System.Windows.Forms.Button buttonAddTrainer;
        private System.Windows.Forms.GroupBox gboxDVs;
        private System.Windows.Forms.TextBox tboxDVlabel;
        private System.Windows.Forms.TextBox tboxDVinfo;
        private System.Windows.Forms.TextBox tboxHPDV;
        private System.Windows.Forms.TextBox txtGroupPtr;
        private System.Windows.Forms.Button buttonAnalyze;
    }
}

