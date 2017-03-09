namespace Gen2_Evolution_Editor {
    partial class EvolutionEditor {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EvolutionEditor));
            this.comboEvolveFrom = new System.Windows.Forms.ComboBox();
            this.comboEvolveTo = new System.Windows.Forms.ComboBox();
            this.spinEvoIndex = new System.Windows.Forms.NumericUpDown();
            this.spinEvoParam = new System.Windows.Forms.NumericUpDown();
            this.comboEvoMethod = new System.Windows.Forms.ComboBox();
            this.comboItems = new System.Windows.Forms.ComboBox();
            this.txtNumOfEvos = new System.Windows.Forms.TextBox();
            this.btnAddEvo = new System.Windows.Forms.Button();
            this.btnRemoveEvo = new System.Windows.Forms.Button();
            this.tboxFreeBytes = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.spinDVbyte = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.spinEvoIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEvoParam)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinDVbyte)).BeginInit();
            this.SuspendLayout();
            // 
            // comboEvolveFrom
            // 
            this.comboEvolveFrom.Enabled = false;
            this.comboEvolveFrom.FormattingEnabled = true;
            this.comboEvolveFrom.Location = new System.Drawing.Point(12, 61);
            this.comboEvolveFrom.Name = "comboEvolveFrom";
            this.comboEvolveFrom.Size = new System.Drawing.Size(139, 29);
            this.comboEvolveFrom.TabIndex = 1;
            this.comboEvolveFrom.SelectedIndexChanged += new System.EventHandler(this.comboEvolveFrom_SelectedIndexChanged);
            // 
            // comboEvolveTo
            // 
            this.comboEvolveTo.Enabled = false;
            this.comboEvolveTo.FormattingEnabled = true;
            this.comboEvolveTo.Location = new System.Drawing.Point(12, 149);
            this.comboEvolveTo.Name = "comboEvolveTo";
            this.comboEvolveTo.Size = new System.Drawing.Size(139, 29);
            this.comboEvolveTo.TabIndex = 2;
            this.comboEvolveTo.SelectedIndexChanged += new System.EventHandler(this.comboEvolveTo_SelectedIndexChanged);
            // 
            // spinEvoIndex
            // 
            this.spinEvoIndex.Enabled = false;
            this.spinEvoIndex.Location = new System.Drawing.Point(12, 114);
            this.spinEvoIndex.Maximum = new decimal(new int[] {
            251,
            0,
            0,
            0});
            this.spinEvoIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEvoIndex.Name = "spinEvoIndex";
            this.spinEvoIndex.Size = new System.Drawing.Size(54, 29);
            this.spinEvoIndex.TabIndex = 3;
            this.spinEvoIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinEvoIndex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEvoIndex.ValueChanged += new System.EventHandler(this.spinEvoIndex_ValueChanged);
            // 
            // spinEvoParam
            // 
            this.spinEvoParam.Enabled = false;
            this.spinEvoParam.Location = new System.Drawing.Point(13, 221);
            this.spinEvoParam.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinEvoParam.Name = "spinEvoParam";
            this.spinEvoParam.Size = new System.Drawing.Size(53, 29);
            this.spinEvoParam.TabIndex = 5;
            this.spinEvoParam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinEvoParam.ValueChanged += new System.EventHandler(this.spinEvoParam_ValueChanged);
            // 
            // comboEvoMethod
            // 
            this.comboEvoMethod.Enabled = false;
            this.comboEvoMethod.FormattingEnabled = true;
            this.comboEvoMethod.Items.AddRange(new object[] {
            "Level Up",
            "Item",
            "Trade",
            "Happiness",
            "Atk ? Def"});
            this.comboEvoMethod.Location = new System.Drawing.Point(13, 185);
            this.comboEvoMethod.Name = "comboEvoMethod";
            this.comboEvoMethod.Size = new System.Drawing.Size(138, 29);
            this.comboEvoMethod.TabIndex = 6;
            this.comboEvoMethod.SelectedIndexChanged += new System.EventHandler(this.comboEvoMethod_SelectedIndexChanged);
            // 
            // comboItems
            // 
            this.comboItems.Enabled = false;
            this.comboItems.FormattingEnabled = true;
            this.comboItems.Location = new System.Drawing.Point(73, 221);
            this.comboItems.Name = "comboItems";
            this.comboItems.Size = new System.Drawing.Size(157, 29);
            this.comboItems.TabIndex = 7;
            this.comboItems.SelectedIndexChanged += new System.EventHandler(this.comboItems_SelectedIndexChanged);
            // 
            // txtNumOfEvos
            // 
            this.txtNumOfEvos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNumOfEvos.Enabled = false;
            this.txtNumOfEvos.Location = new System.Drawing.Point(73, 114);
            this.txtNumOfEvos.Name = "txtNumOfEvos";
            this.txtNumOfEvos.Size = new System.Drawing.Size(37, 22);
            this.txtNumOfEvos.TabIndex = 8;
            this.txtNumOfEvos.Text = "/0";
            // 
            // btnAddEvo
            // 
            this.btnAddEvo.Enabled = false;
            this.btnAddEvo.Location = new System.Drawing.Point(117, 114);
            this.btnAddEvo.Name = "btnAddEvo";
            this.btnAddEvo.Size = new System.Drawing.Size(34, 29);
            this.btnAddEvo.TabIndex = 9;
            this.btnAddEvo.Text = "+";
            this.btnAddEvo.UseVisualStyleBackColor = true;
            this.btnAddEvo.Click += new System.EventHandler(this.btnAddEvo_Click);
            // 
            // btnRemoveEvo
            // 
            this.btnRemoveEvo.Enabled = false;
            this.btnRemoveEvo.Location = new System.Drawing.Point(157, 114);
            this.btnRemoveEvo.Name = "btnRemoveEvo";
            this.btnRemoveEvo.Size = new System.Drawing.Size(34, 29);
            this.btnRemoveEvo.TabIndex = 10;
            this.btnRemoveEvo.Text = "-";
            this.btnRemoveEvo.UseVisualStyleBackColor = true;
            this.btnRemoveEvo.Click += new System.EventHandler(this.btnRemoveEvo_Click);
            // 
            // tboxFreeBytes
            // 
            this.tboxFreeBytes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tboxFreeBytes.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxFreeBytes.Location = new System.Drawing.Point(12, 32);
            this.tboxFreeBytes.Multiline = true;
            this.tboxFreeBytes.Name = "tboxFreeBytes";
            this.tboxFreeBytes.ReadOnly = true;
            this.tboxFreeBytes.Size = new System.Drawing.Size(217, 23);
            this.tboxFreeBytes.TabIndex = 38;
            this.tboxFreeBytes.Text = "0 bytes free";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.spinDVbyte);
            this.groupBox1.Location = new System.Drawing.Point(158, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(71, 64);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DV";
            // 
            // spinDVbyte
            // 
            this.spinDVbyte.Enabled = false;
            this.spinDVbyte.Hexadecimal = true;
            this.spinDVbyte.Location = new System.Drawing.Point(6, 28);
            this.spinDVbyte.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinDVbyte.Name = "spinDVbyte";
            this.spinDVbyte.Size = new System.Drawing.Size(59, 29);
            this.spinDVbyte.TabIndex = 0;
            this.spinDVbyte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinDVbyte.ValueChanged += new System.EventHandler(this.spinDVbyte_ValueChanged);
            // 
            // Evolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 262);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tboxFreeBytes);
            this.Controls.Add(this.btnRemoveEvo);
            this.Controls.Add(this.btnAddEvo);
            this.Controls.Add(this.txtNumOfEvos);
            this.Controls.Add(this.comboItems);
            this.Controls.Add(this.comboEvoMethod);
            this.Controls.Add(this.spinEvoParam);
            this.Controls.Add(this.spinEvoIndex);
            this.Controls.Add(this.comboEvolveTo);
            this.Controls.Add(this.comboEvolveFrom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Evolution";
            this.Text = "WGSC Evo Editor";
            this.Controls.SetChildIndex(this.comboEvolveFrom, 0);
            this.Controls.SetChildIndex(this.comboEvolveTo, 0);
            this.Controls.SetChildIndex(this.spinEvoIndex, 0);
            this.Controls.SetChildIndex(this.spinEvoParam, 0);
            this.Controls.SetChildIndex(this.comboEvoMethod, 0);
            this.Controls.SetChildIndex(this.comboItems, 0);
            this.Controls.SetChildIndex(this.txtNumOfEvos, 0);
            this.Controls.SetChildIndex(this.btnAddEvo, 0);
            this.Controls.SetChildIndex(this.btnRemoveEvo, 0);
            this.Controls.SetChildIndex(this.tboxFreeBytes, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.spinEvoIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEvoParam)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinDVbyte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboEvolveFrom;
        private System.Windows.Forms.ComboBox comboEvolveTo;
        private System.Windows.Forms.NumericUpDown spinEvoIndex;
        private System.Windows.Forms.NumericUpDown spinEvoParam;
        private System.Windows.Forms.ComboBox comboEvoMethod;
        private System.Windows.Forms.ComboBox comboItems;
        private System.Windows.Forms.TextBox txtNumOfEvos;
        private System.Windows.Forms.Button btnAddEvo;
        private System.Windows.Forms.Button btnRemoveEvo;
        private System.Windows.Forms.TextBox tboxFreeBytes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown spinDVbyte;
    }
}

