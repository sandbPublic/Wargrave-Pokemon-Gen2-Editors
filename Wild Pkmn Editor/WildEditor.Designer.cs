namespace Gen2_Wild_Pkmn_Editor {
    partial class WildEditor {
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WildEditor));
            this.comboRegion = new System.Windows.Forms.ComboBox();
            this.comboArea = new System.Windows.Forms.ComboBox();
            this.textPkmnMorn = new System.Windows.Forms.TextBox();
            this.spinMornFreq = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupMorning = new System.Windows.Forms.GroupBox();
            this.buttonDecLevelsMorn = new System.Windows.Forms.Button();
            this.buttonIncLevelsMorn = new System.Windows.Forms.Button();
            this.groupDay = new System.Windows.Forms.GroupBox();
            this.buttonDecLevelsDay = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonIncLevelsDay = new System.Windows.Forms.Button();
            this.textPkmnDay = new System.Windows.Forms.TextBox();
            this.spinDayFreq = new System.Windows.Forms.NumericUpDown();
            this.groupNight = new System.Windows.Forms.GroupBox();
            this.buttonDecLevelsNight = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.buttonIncLevelsNight = new System.Windows.Forms.Button();
            this.textPkmnNight = new System.Windows.Forms.TextBox();
            this.spinNightFreq = new System.Windows.Forms.NumericUpDown();
            this.textMapBank = new System.Windows.Forms.TextBox();
            this.textMapNum = new System.Windows.Forms.TextBox();
            this.buttonAnalyze = new System.Windows.Forms.Button();
            this.comboVersion = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.spinMornFreq)).BeginInit();
            this.groupMorning.SuspendLayout();
            this.groupDay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinDayFreq)).BeginInit();
            this.groupNight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinNightFreq)).BeginInit();
            this.SuspendLayout();
            // 
            // comboRegion
            // 
            this.comboRegion.Enabled = false;
            this.comboRegion.FormattingEnabled = true;
            this.comboRegion.Items.AddRange(new object[] {
            "Johto Land",
            "Johto Water",
            "Kanto Land",
            "Kanto Water",
            "Swarms"});
            this.comboRegion.Location = new System.Drawing.Point(13, 33);
            this.comboRegion.Name = "comboRegion";
            this.comboRegion.Size = new System.Drawing.Size(144, 29);
            this.comboRegion.TabIndex = 1;
            this.comboRegion.SelectedIndexChanged += new System.EventHandler(this.comboRegion_SelectedIndexChanged);
            // 
            // comboArea
            // 
            this.comboArea.Enabled = false;
            this.comboArea.FormattingEnabled = true;
            this.comboArea.Location = new System.Drawing.Point(12, 68);
            this.comboArea.Name = "comboArea";
            this.comboArea.Size = new System.Drawing.Size(224, 29);
            this.comboArea.TabIndex = 2;
            this.comboArea.SelectedIndexChanged += new System.EventHandler(this.comboArea_SelectedIndexChanged);
            // 
            // textPkmnMorn
            // 
            this.textPkmnMorn.Enabled = false;
            this.textPkmnMorn.Location = new System.Drawing.Point(6, 61);
            this.textPkmnMorn.Multiline = true;
            this.textPkmnMorn.Name = "textPkmnMorn";
            this.textPkmnMorn.Size = new System.Drawing.Size(167, 157);
            this.textPkmnMorn.TabIndex = 4;
            this.textPkmnMorn.TextChanged += new System.EventHandler(this.textPkmnMorn_TextChanged);
            // 
            // spinMornFreq
            // 
            this.spinMornFreq.Enabled = false;
            this.spinMornFreq.Hexadecimal = true;
            this.spinMornFreq.Location = new System.Drawing.Point(126, 26);
            this.spinMornFreq.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinMornFreq.Name = "spinMornFreq";
            this.spinMornFreq.Size = new System.Drawing.Size(47, 29);
            this.spinMornFreq.TabIndex = 7;
            this.spinMornFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinMornFreq.ValueChanged += new System.EventHandler(this.spinMornFreq_ValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(6, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "Frequency";
            // 
            // groupMorning
            // 
            this.groupMorning.Controls.Add(this.buttonDecLevelsMorn);
            this.groupMorning.Controls.Add(this.buttonIncLevelsMorn);
            this.groupMorning.Controls.Add(this.textBox1);
            this.groupMorning.Controls.Add(this.textPkmnMorn);
            this.groupMorning.Controls.Add(this.spinMornFreq);
            this.groupMorning.Location = new System.Drawing.Point(13, 103);
            this.groupMorning.Name = "groupMorning";
            this.groupMorning.Size = new System.Drawing.Size(186, 303);
            this.groupMorning.TabIndex = 13;
            this.groupMorning.TabStop = false;
            this.groupMorning.Text = "Morning";
            // 
            // buttonDecLevelsMorn
            // 
            this.buttonDecLevelsMorn.Enabled = false;
            this.buttonDecLevelsMorn.Location = new System.Drawing.Point(6, 264);
            this.buttonDecLevelsMorn.Name = "buttonDecLevelsMorn";
            this.buttonDecLevelsMorn.Size = new System.Drawing.Size(167, 34);
            this.buttonDecLevelsMorn.TabIndex = 21;
            this.buttonDecLevelsMorn.Text = "- Levels";
            this.buttonDecLevelsMorn.UseVisualStyleBackColor = true;
            this.buttonDecLevelsMorn.Click += new System.EventHandler(this.buttonDecLevelsMorn_Click);
            // 
            // buttonIncLevelsMorn
            // 
            this.buttonIncLevelsMorn.Enabled = false;
            this.buttonIncLevelsMorn.Location = new System.Drawing.Point(6, 224);
            this.buttonIncLevelsMorn.Name = "buttonIncLevelsMorn";
            this.buttonIncLevelsMorn.Size = new System.Drawing.Size(167, 34);
            this.buttonIncLevelsMorn.TabIndex = 20;
            this.buttonIncLevelsMorn.Text = "+ Levels";
            this.buttonIncLevelsMorn.UseVisualStyleBackColor = true;
            this.buttonIncLevelsMorn.Click += new System.EventHandler(this.buttonIncLevelsMorn_Click);
            // 
            // groupDay
            // 
            this.groupDay.Controls.Add(this.buttonDecLevelsDay);
            this.groupDay.Controls.Add(this.textBox2);
            this.groupDay.Controls.Add(this.buttonIncLevelsDay);
            this.groupDay.Controls.Add(this.textPkmnDay);
            this.groupDay.Controls.Add(this.spinDayFreq);
            this.groupDay.Location = new System.Drawing.Point(205, 103);
            this.groupDay.Name = "groupDay";
            this.groupDay.Size = new System.Drawing.Size(186, 303);
            this.groupDay.TabIndex = 14;
            this.groupDay.TabStop = false;
            this.groupDay.Text = "Day";
            // 
            // buttonDecLevelsDay
            // 
            this.buttonDecLevelsDay.Enabled = false;
            this.buttonDecLevelsDay.Location = new System.Drawing.Point(6, 264);
            this.buttonDecLevelsDay.Name = "buttonDecLevelsDay";
            this.buttonDecLevelsDay.Size = new System.Drawing.Size(167, 34);
            this.buttonDecLevelsDay.TabIndex = 23;
            this.buttonDecLevelsDay.Text = "- Levels";
            this.buttonDecLevelsDay.UseVisualStyleBackColor = true;
            this.buttonDecLevelsDay.Click += new System.EventHandler(this.buttonDecLevelsDay_Click);
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(6, 28);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "Frequency";
            // 
            // buttonIncLevelsDay
            // 
            this.buttonIncLevelsDay.Enabled = false;
            this.buttonIncLevelsDay.Location = new System.Drawing.Point(6, 224);
            this.buttonIncLevelsDay.Name = "buttonIncLevelsDay";
            this.buttonIncLevelsDay.Size = new System.Drawing.Size(167, 34);
            this.buttonIncLevelsDay.TabIndex = 22;
            this.buttonIncLevelsDay.Text = "+ Levels";
            this.buttonIncLevelsDay.UseVisualStyleBackColor = true;
            this.buttonIncLevelsDay.Click += new System.EventHandler(this.buttonIncLevelsDay_Click);
            // 
            // textPkmnDay
            // 
            this.textPkmnDay.Enabled = false;
            this.textPkmnDay.Location = new System.Drawing.Point(6, 61);
            this.textPkmnDay.Multiline = true;
            this.textPkmnDay.Name = "textPkmnDay";
            this.textPkmnDay.Size = new System.Drawing.Size(167, 157);
            this.textPkmnDay.TabIndex = 4;
            this.textPkmnDay.TextChanged += new System.EventHandler(this.textPkmnDay_TextChanged);
            // 
            // spinDayFreq
            // 
            this.spinDayFreq.Enabled = false;
            this.spinDayFreq.Hexadecimal = true;
            this.spinDayFreq.Location = new System.Drawing.Point(126, 26);
            this.spinDayFreq.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinDayFreq.Name = "spinDayFreq";
            this.spinDayFreq.Size = new System.Drawing.Size(47, 29);
            this.spinDayFreq.TabIndex = 7;
            this.spinDayFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinDayFreq.ValueChanged += new System.EventHandler(this.spinDayFreq_ValueChanged);
            // 
            // groupNight
            // 
            this.groupNight.Controls.Add(this.buttonDecLevelsNight);
            this.groupNight.Controls.Add(this.textBox4);
            this.groupNight.Controls.Add(this.buttonIncLevelsNight);
            this.groupNight.Controls.Add(this.textPkmnNight);
            this.groupNight.Controls.Add(this.spinNightFreq);
            this.groupNight.Location = new System.Drawing.Point(397, 103);
            this.groupNight.Name = "groupNight";
            this.groupNight.Size = new System.Drawing.Size(186, 303);
            this.groupNight.TabIndex = 14;
            this.groupNight.TabStop = false;
            this.groupNight.Text = "Night";
            // 
            // buttonDecLevelsNight
            // 
            this.buttonDecLevelsNight.Enabled = false;
            this.buttonDecLevelsNight.Location = new System.Drawing.Point(6, 264);
            this.buttonDecLevelsNight.Name = "buttonDecLevelsNight";
            this.buttonDecLevelsNight.Size = new System.Drawing.Size(167, 34);
            this.buttonDecLevelsNight.TabIndex = 25;
            this.buttonDecLevelsNight.Text = "- Levels";
            this.buttonDecLevelsNight.UseVisualStyleBackColor = true;
            this.buttonDecLevelsNight.Click += new System.EventHandler(this.buttonDecLevelsNight_Click);
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(6, 28);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 22);
            this.textBox4.TabIndex = 10;
            this.textBox4.Text = "Frequency";
            // 
            // buttonIncLevelsNight
            // 
            this.buttonIncLevelsNight.Enabled = false;
            this.buttonIncLevelsNight.Location = new System.Drawing.Point(6, 224);
            this.buttonIncLevelsNight.Name = "buttonIncLevelsNight";
            this.buttonIncLevelsNight.Size = new System.Drawing.Size(167, 34);
            this.buttonIncLevelsNight.TabIndex = 24;
            this.buttonIncLevelsNight.Text = "+ Levels";
            this.buttonIncLevelsNight.UseVisualStyleBackColor = true;
            this.buttonIncLevelsNight.Click += new System.EventHandler(this.buttonIncLevelsNight_Click);
            // 
            // textPkmnNight
            // 
            this.textPkmnNight.Enabled = false;
            this.textPkmnNight.Location = new System.Drawing.Point(6, 61);
            this.textPkmnNight.Multiline = true;
            this.textPkmnNight.Name = "textPkmnNight";
            this.textPkmnNight.Size = new System.Drawing.Size(167, 157);
            this.textPkmnNight.TabIndex = 4;
            this.textPkmnNight.TextChanged += new System.EventHandler(this.textPkmnNight_TextChanged);
            // 
            // spinNightFreq
            // 
            this.spinNightFreq.Enabled = false;
            this.spinNightFreq.Hexadecimal = true;
            this.spinNightFreq.Location = new System.Drawing.Point(126, 26);
            this.spinNightFreq.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinNightFreq.Name = "spinNightFreq";
            this.spinNightFreq.Size = new System.Drawing.Size(47, 29);
            this.spinNightFreq.TabIndex = 7;
            this.spinNightFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinNightFreq.ValueChanged += new System.EventHandler(this.spinNightFreq_ValueChanged);
            // 
            // textMapBank
            // 
            this.textMapBank.Location = new System.Drawing.Point(247, 68);
            this.textMapBank.Name = "textMapBank";
            this.textMapBank.ReadOnly = true;
            this.textMapBank.Size = new System.Drawing.Size(30, 29);
            this.textMapBank.TabIndex = 15;
            // 
            // textMapNum
            // 
            this.textMapNum.Location = new System.Drawing.Point(288, 68);
            this.textMapNum.Name = "textMapNum";
            this.textMapNum.ReadOnly = true;
            this.textMapNum.Size = new System.Drawing.Size(30, 29);
            this.textMapNum.TabIndex = 16;
            // 
            // buttonAnalyze
            // 
            this.buttonAnalyze.Enabled = false;
            this.buttonAnalyze.Location = new System.Drawing.Point(329, 32);
            this.buttonAnalyze.Name = "buttonAnalyze";
            this.buttonAnalyze.Size = new System.Drawing.Size(254, 64);
            this.buttonAnalyze.TabIndex = 18;
            this.buttonAnalyze.Text = "Analyze region Pokemon family rarity";
            this.buttonAnalyze.UseVisualStyleBackColor = true;
            this.buttonAnalyze.Click += new System.EventHandler(this.buttonAnalyze_Click);
            // 
            // comboVersion
            // 
            this.comboVersion.Enabled = false;
            this.comboVersion.FormattingEnabled = true;
            this.comboVersion.Items.AddRange(new object[] {
            "Crystal",
            "Gold/Silver",
            "Other"});
            this.comboVersion.Location = new System.Drawing.Point(173, 33);
            this.comboVersion.Name = "comboVersion";
            this.comboVersion.Size = new System.Drawing.Size(145, 29);
            this.comboVersion.TabIndex = 19;
            this.comboVersion.SelectedIndexChanged += new System.EventHandler(this.comboVersion_SelectedIndexChanged);
            // 
            // Wild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 416);
            this.Controls.Add(this.comboVersion);
            this.Controls.Add(this.buttonAnalyze);
            this.Controls.Add(this.textMapNum);
            this.Controls.Add(this.textMapBank);
            this.Controls.Add(this.groupNight);
            this.Controls.Add(this.groupDay);
            this.Controls.Add(this.groupMorning);
            this.Controls.Add(this.comboArea);
            this.Controls.Add(this.comboRegion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Wild";
            this.Text = "Wargrave Gen2 Area Wild Pokemon Editor";
            this.Controls.SetChildIndex(this.comboRegion, 0);
            this.Controls.SetChildIndex(this.comboArea, 0);
            this.Controls.SetChildIndex(this.groupMorning, 0);
            this.Controls.SetChildIndex(this.groupDay, 0);
            this.Controls.SetChildIndex(this.groupNight, 0);
            this.Controls.SetChildIndex(this.textMapBank, 0);
            this.Controls.SetChildIndex(this.textMapNum, 0);
            this.Controls.SetChildIndex(this.buttonAnalyze, 0);
            this.Controls.SetChildIndex(this.comboVersion, 0);
            ((System.ComponentModel.ISupportInitialize)(this.spinMornFreq)).EndInit();
            this.groupMorning.ResumeLayout(false);
            this.groupMorning.PerformLayout();
            this.groupDay.ResumeLayout(false);
            this.groupDay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinDayFreq)).EndInit();
            this.groupNight.ResumeLayout(false);
            this.groupNight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinNightFreq)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboRegion;
        private System.Windows.Forms.ComboBox comboArea;
        private System.Windows.Forms.TextBox textPkmnMorn;
        private System.Windows.Forms.NumericUpDown spinMornFreq;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupMorning;
        private System.Windows.Forms.GroupBox groupDay;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textPkmnDay;
        private System.Windows.Forms.NumericUpDown spinDayFreq;
        private System.Windows.Forms.GroupBox groupNight;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textPkmnNight;
        private System.Windows.Forms.NumericUpDown spinNightFreq;
        private System.Windows.Forms.TextBox textMapBank;
        private System.Windows.Forms.TextBox textMapNum;
        private System.Windows.Forms.Button buttonAnalyze;
        private System.Windows.Forms.ComboBox comboVersion;
        private System.Windows.Forms.Button buttonDecLevelsMorn;
        private System.Windows.Forms.Button buttonIncLevelsMorn;
        private System.Windows.Forms.Button buttonDecLevelsDay;
        private System.Windows.Forms.Button buttonIncLevelsDay;
        private System.Windows.Forms.Button buttonDecLevelsNight;
        private System.Windows.Forms.Button buttonIncLevelsNight;
    }
}

