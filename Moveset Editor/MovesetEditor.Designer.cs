namespace Gen2_Moveset_Editor {
    using System.Windows.Forms;
    
    partial class MovesetEditor {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MovesetEditor));
            this.checkConsecMode = new System.Windows.Forms.CheckBox();
            this.buttonEditTMs = new System.Windows.Forms.Button();
            this.buttonCopyTMsA = new System.Windows.Forms.Button();
            this.buttonCopyTMsB = new System.Windows.Forms.Button();
            this.tboxFreeBytes = new System.Windows.Forms.TextBox();
            this.tBoxMoveset1 = new System.Windows.Forms.TextBox();
            this.tBoxMoveset0 = new System.Windows.Forms.TextBox();
            this.tBoxMoveset2 = new System.Windows.Forms.TextBox();
            this.spinPkmnID_0 = new System.Windows.Forms.NumericUpDown();
            this.tBoxPkmn_0 = new System.Windows.Forms.TextBox();
            this.tBoxPkmn_1 = new System.Windows.Forms.TextBox();
            this.spinPkmnID_1 = new System.Windows.Forms.NumericUpDown();
            this.tBoxPkmn_2 = new System.Windows.Forms.TextBox();
            this.spinPkmnID_2 = new System.Windows.Forms.NumericUpDown();
            this.buttonAnalyze = new System.Windows.Forms.Button();
            this.textEvoCond_0 = new System.Windows.Forms.TextBox();
            this.textEvoCond_1 = new System.Windows.Forms.TextBox();
            this.textEvoCond_2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.spinPkmnID_0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPkmnID_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPkmnID_2)).BeginInit();
            this.SuspendLayout();
            // 
            // checkConsecMode
            // 
            this.checkConsecMode.AutoSize = true;
            this.checkConsecMode.Checked = true;
            this.checkConsecMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkConsecMode.Enabled = false;
            this.checkConsecMode.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkConsecMode.Location = new System.Drawing.Point(223, 36);
            this.checkConsecMode.Name = "checkConsecMode";
            this.checkConsecMode.Size = new System.Drawing.Size(205, 25);
            this.checkConsecMode.TabIndex = 3;
            this.checkConsecMode.Text = "Consecutive mode";
            this.checkConsecMode.UseVisualStyleBackColor = true;
            this.checkConsecMode.CheckedChanged += new System.EventHandler(this.checkConsecMode_CheckedChanged);
            // 
            // buttonEditTMs
            // 
            this.buttonEditTMs.Enabled = false;
            this.buttonEditTMs.Location = new System.Drawing.Point(223, 438);
            this.buttonEditTMs.Name = "buttonEditTMs";
            this.buttonEditTMs.Size = new System.Drawing.Size(121, 40);
            this.buttonEditTMs.TabIndex = 4;
            this.buttonEditTMs.Text = "Edit TMs\r\n";
            this.buttonEditTMs.UseVisualStyleBackColor = true;
            this.buttonEditTMs.Click += new System.EventHandler(this.buttonOpenTMs_Click);
            // 
            // buttonCopyTMsA
            // 
            this.buttonCopyTMsA.Enabled = false;
            this.buttonCopyTMsA.Location = new System.Drawing.Point(12, 438);
            this.buttonCopyTMsA.Name = "buttonCopyTMsA";
            this.buttonCopyTMsA.Size = new System.Drawing.Size(121, 40);
            this.buttonCopyTMsA.TabIndex = 5;
            this.buttonCopyTMsA.Text = "Copy TMs\r\n";
            this.buttonCopyTMsA.UseVisualStyleBackColor = true;
            this.buttonCopyTMsA.Click += new System.EventHandler(this.buttonCopyTMsA_Click);
            // 
            // buttonCopyTMsB
            // 
            this.buttonCopyTMsB.Enabled = false;
            this.buttonCopyTMsB.Location = new System.Drawing.Point(434, 438);
            this.buttonCopyTMsB.Name = "buttonCopyTMsB";
            this.buttonCopyTMsB.Size = new System.Drawing.Size(121, 40);
            this.buttonCopyTMsB.TabIndex = 6;
            this.buttonCopyTMsB.Text = "Copy TMs\r\n";
            this.buttonCopyTMsB.UseVisualStyleBackColor = true;
            this.buttonCopyTMsB.Click += new System.EventHandler(this.buttonCopyTMsB_Click);
            // 
            // tboxFreeBytes
            // 
            this.tboxFreeBytes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tboxFreeBytes.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxFreeBytes.Location = new System.Drawing.Point(12, 35);
            this.tboxFreeBytes.Name = "tboxFreeBytes";
            this.tboxFreeBytes.ReadOnly = true;
            this.tboxFreeBytes.Size = new System.Drawing.Size(189, 29);
            this.tboxFreeBytes.TabIndex = 7;
            this.tboxFreeBytes.Text = "0 bytes free";
            // 
            // tBoxMoveset1
            // 
            this.tBoxMoveset1.AcceptsTab = true;
            this.tBoxMoveset1.Enabled = false;
            this.tBoxMoveset1.Location = new System.Drawing.Point(223, 108);
            this.tBoxMoveset1.Multiline = true;
            this.tBoxMoveset1.Name = "tBoxMoveset1";
            this.tBoxMoveset1.Size = new System.Drawing.Size(189, 324);
            this.tBoxMoveset1.TabIndex = 9;
            this.tBoxMoveset1.WordWrap = false;
            this.tBoxMoveset1.TextChanged += new System.EventHandler(this.tBox1_TextChanged);
            // 
            // tBoxMoveset0
            // 
            this.tBoxMoveset0.AcceptsTab = true;
            this.tBoxMoveset0.Enabled = false;
            this.tBoxMoveset0.Location = new System.Drawing.Point(12, 108);
            this.tBoxMoveset0.Multiline = true;
            this.tBoxMoveset0.Name = "tBoxMoveset0";
            this.tBoxMoveset0.Size = new System.Drawing.Size(189, 324);
            this.tBoxMoveset0.TabIndex = 10;
            this.tBoxMoveset0.TextChanged += new System.EventHandler(this.tBox0_TextChanged);
            // 
            // tBoxMoveset2
            // 
            this.tBoxMoveset2.AcceptsTab = true;
            this.tBoxMoveset2.Enabled = false;
            this.tBoxMoveset2.Location = new System.Drawing.Point(434, 108);
            this.tBoxMoveset2.Multiline = true;
            this.tBoxMoveset2.Name = "tBoxMoveset2";
            this.tBoxMoveset2.Size = new System.Drawing.Size(189, 324);
            this.tBoxMoveset2.TabIndex = 11;
            this.tBoxMoveset2.TextChanged += new System.EventHandler(this.tBox2_TextChanged);
            // 
            // spinPkmnID_0
            // 
            this.spinPkmnID_0.Enabled = false;
            this.spinPkmnID_0.Location = new System.Drawing.Point(12, 70);
            this.spinPkmnID_0.Maximum = new decimal(new int[] {
            251,
            0,
            0,
            0});
            this.spinPkmnID_0.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinPkmnID_0.Name = "spinPkmnID_0";
            this.spinPkmnID_0.Size = new System.Drawing.Size(61, 29);
            this.spinPkmnID_0.TabIndex = 12;
            this.spinPkmnID_0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinPkmnID_0.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinPkmnID_0.ValueChanged += new System.EventHandler(this.spinPkmnID_0_ValueChanged);
            // 
            // tBoxPkmn_0
            // 
            this.tBoxPkmn_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tBoxPkmn_0.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxPkmn_0.Location = new System.Drawing.Point(79, 70);
            this.tBoxPkmn_0.Name = "tBoxPkmn_0";
            this.tBoxPkmn_0.ReadOnly = true;
            this.tBoxPkmn_0.Size = new System.Drawing.Size(122, 29);
            this.tBoxPkmn_0.TabIndex = 13;
            // 
            // tBoxPkmn_1
            // 
            this.tBoxPkmn_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tBoxPkmn_1.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxPkmn_1.Location = new System.Drawing.Point(290, 70);
            this.tBoxPkmn_1.Name = "tBoxPkmn_1";
            this.tBoxPkmn_1.ReadOnly = true;
            this.tBoxPkmn_1.Size = new System.Drawing.Size(122, 29);
            this.tBoxPkmn_1.TabIndex = 15;
            // 
            // spinPkmnID_1
            // 
            this.spinPkmnID_1.Enabled = false;
            this.spinPkmnID_1.Location = new System.Drawing.Point(223, 70);
            this.spinPkmnID_1.Maximum = new decimal(new int[] {
            251,
            0,
            0,
            0});
            this.spinPkmnID_1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinPkmnID_1.Name = "spinPkmnID_1";
            this.spinPkmnID_1.Size = new System.Drawing.Size(61, 29);
            this.spinPkmnID_1.TabIndex = 14;
            this.spinPkmnID_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinPkmnID_1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.spinPkmnID_1.ValueChanged += new System.EventHandler(this.spinPkmnID_1_ValueChanged);
            // 
            // tBoxPkmn_2
            // 
            this.tBoxPkmn_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tBoxPkmn_2.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxPkmn_2.Location = new System.Drawing.Point(501, 70);
            this.tBoxPkmn_2.Name = "tBoxPkmn_2";
            this.tBoxPkmn_2.ReadOnly = true;
            this.tBoxPkmn_2.Size = new System.Drawing.Size(122, 29);
            this.tBoxPkmn_2.TabIndex = 17;
            // 
            // spinPkmnID_2
            // 
            this.spinPkmnID_2.Enabled = false;
            this.spinPkmnID_2.Location = new System.Drawing.Point(434, 70);
            this.spinPkmnID_2.Maximum = new decimal(new int[] {
            251,
            0,
            0,
            0});
            this.spinPkmnID_2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinPkmnID_2.Name = "spinPkmnID_2";
            this.spinPkmnID_2.Size = new System.Drawing.Size(61, 29);
            this.spinPkmnID_2.TabIndex = 16;
            this.spinPkmnID_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinPkmnID_2.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.spinPkmnID_2.ValueChanged += new System.EventHandler(this.spinPkmnID_2_ValueChanged);
            // 
            // buttonAnalyze
            // 
            this.buttonAnalyze.Enabled = false;
            this.buttonAnalyze.Location = new System.Drawing.Point(435, 32);
            this.buttonAnalyze.Name = "buttonAnalyze";
            this.buttonAnalyze.Size = new System.Drawing.Size(188, 32);
            this.buttonAnalyze.TabIndex = 18;
            this.buttonAnalyze.Text = "Analyze usage";
            this.buttonAnalyze.UseVisualStyleBackColor = true;
            this.buttonAnalyze.Click += new System.EventHandler(this.buttonAnalyze_Click);
            // 
            // textEvoCond_0
            // 
            this.textEvoCond_0.Location = new System.Drawing.Point(140, 439);
            this.textEvoCond_0.Multiline = true;
            this.textEvoCond_0.Name = "textEvoCond_0";
            this.textEvoCond_0.ReadOnly = true;
            this.textEvoCond_0.Size = new System.Drawing.Size(61, 39);
            this.textEvoCond_0.TabIndex = 19;
            // 
            // textEvoCond_1
            // 
            this.textEvoCond_1.Location = new System.Drawing.Point(350, 439);
            this.textEvoCond_1.Multiline = true;
            this.textEvoCond_1.Name = "textEvoCond_1";
            this.textEvoCond_1.ReadOnly = true;
            this.textEvoCond_1.Size = new System.Drawing.Size(61, 39);
            this.textEvoCond_1.TabIndex = 20;
            // 
            // textEvoCond_2
            // 
            this.textEvoCond_2.Location = new System.Drawing.Point(561, 438);
            this.textEvoCond_2.Multiline = true;
            this.textEvoCond_2.Name = "textEvoCond_2";
            this.textEvoCond_2.ReadOnly = true;
            this.textEvoCond_2.Size = new System.Drawing.Size(61, 40);
            this.textEvoCond_2.TabIndex = 21;
            // 
            // MovesetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 490);
            this.Controls.Add(this.textEvoCond_2);
            this.Controls.Add(this.textEvoCond_1);
            this.Controls.Add(this.textEvoCond_0);
            this.Controls.Add(this.buttonAnalyze);
            this.Controls.Add(this.tBoxPkmn_2);
            this.Controls.Add(this.spinPkmnID_2);
            this.Controls.Add(this.tBoxPkmn_1);
            this.Controls.Add(this.spinPkmnID_1);
            this.Controls.Add(this.tBoxPkmn_0);
            this.Controls.Add(this.spinPkmnID_0);
            this.Controls.Add(this.tBoxMoveset2);
            this.Controls.Add(this.tBoxMoveset0);
            this.Controls.Add(this.tBoxMoveset1);
            this.Controls.Add(this.tboxFreeBytes);
            this.Controls.Add(this.buttonCopyTMsB);
            this.Controls.Add(this.buttonCopyTMsA);
            this.Controls.Add(this.buttonEditTMs);
            this.Controls.Add(this.checkConsecMode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "MovesetEditor";
            this.Text = "Wargrave GSC Moveset Editor";
            this.Controls.SetChildIndex(this.checkConsecMode, 0);
            this.Controls.SetChildIndex(this.buttonEditTMs, 0);
            this.Controls.SetChildIndex(this.buttonCopyTMsA, 0);
            this.Controls.SetChildIndex(this.buttonCopyTMsB, 0);
            this.Controls.SetChildIndex(this.tboxFreeBytes, 0);
            this.Controls.SetChildIndex(this.tBoxMoveset1, 0);
            this.Controls.SetChildIndex(this.tBoxMoveset0, 0);
            this.Controls.SetChildIndex(this.tBoxMoveset2, 0);
            this.Controls.SetChildIndex(this.spinPkmnID_0, 0);
            this.Controls.SetChildIndex(this.tBoxPkmn_0, 0);
            this.Controls.SetChildIndex(this.spinPkmnID_1, 0);
            this.Controls.SetChildIndex(this.tBoxPkmn_1, 0);
            this.Controls.SetChildIndex(this.spinPkmnID_2, 0);
            this.Controls.SetChildIndex(this.tBoxPkmn_2, 0);
            this.Controls.SetChildIndex(this.buttonAnalyze, 0);
            this.Controls.SetChildIndex(this.textEvoCond_0, 0);
            this.Controls.SetChildIndex(this.textEvoCond_1, 0);
            this.Controls.SetChildIndex(this.textEvoCond_2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.spinPkmnID_0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPkmnID_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPkmnID_2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox checkConsecMode;
        private Button buttonEditTMs;
        private Button buttonCopyTMsA;
        private Button buttonCopyTMsB;
        private TextBox tboxFreeBytes;
        private TextBox tBoxMoveset0;
        private TextBox tBoxMoveset1;
        private TextBox tBoxMoveset2;
        private NumericUpDown spinPkmnID_0;
        private TextBox tBoxPkmn_0;
        private TextBox tBoxPkmn_1;
        private NumericUpDown spinPkmnID_1;
        private TextBox tBoxPkmn_2;
        private NumericUpDown spinPkmnID_2;
        private Button buttonAnalyze;
        private TextBox textEvoCond_0;
        private TextBox textEvoCond_1;
        private TextBox textEvoCond_2;
    }
}

