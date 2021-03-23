namespace Gen2_Item_Editor {
    partial class ItemEditor {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemEditor));
            this.tboxDeltaNameChars = new System.Windows.Forms.TextBox();
            this.tboxDeltaDescChars = new System.Windows.Forms.TextBox();
            this.spinItemID = new System.Windows.Forms.NumericUpDown();
            this.tboxName = new System.Windows.Forms.TextBox();
            this.tboxDesc = new System.Windows.Forms.TextBox();
            this.gboxCost = new System.Windows.Forms.GroupBox();
            this.spinCost = new System.Windows.Forms.NumericUpDown();
            this.gboxByte2 = new System.Windows.Forms.GroupBox();
            this.spinHeldItemID = new System.Windows.Forms.NumericUpDown();
            this.gboxParam = new System.Windows.Forms.GroupBox();
            this.spinParam = new System.Windows.Forms.NumericUpDown();
            this.gboxUR = new System.Windows.Forms.GroupBox();
            this.cboxUseRestriction = new System.Windows.Forms.ComboBox();
            this.gboxPocket = new System.Windows.Forms.GroupBox();
            this.cboxPocket = new System.Windows.Forms.ComboBox();
            this.gboxFlag = new System.Windows.Forms.GroupBox();
            this.cboxFlagtext = new System.Windows.Forms.ComboBox();
            this.gboxASM = new System.Windows.Forms.GroupBox();
            this.spinASM = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.spinItemID)).BeginInit();
            this.gboxCost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinCost)).BeginInit();
            this.gboxByte2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinHeldItemID)).BeginInit();
            this.gboxParam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinParam)).BeginInit();
            this.gboxUR.SuspendLayout();
            this.gboxPocket.SuspendLayout();
            this.gboxFlag.SuspendLayout();
            this.gboxASM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinASM)).BeginInit();
            this.SuspendLayout();
            // 
            // tboxDeltaNameChars
            // 
            this.tboxDeltaNameChars.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tboxDeltaNameChars.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxDeltaNameChars.Location = new System.Drawing.Point(271, 32);
            this.tboxDeltaNameChars.Name = "tboxDeltaNameChars";
            this.tboxDeltaNameChars.ReadOnly = true;
            this.tboxDeltaNameChars.Size = new System.Drawing.Size(292, 22);
            this.tboxDeltaNameChars.TabIndex = 33;
            this.tboxDeltaNameChars.Text = "0 bytes free for name";
            // 
            // tboxDeltaDescChars
            // 
            this.tboxDeltaDescChars.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tboxDeltaDescChars.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxDeltaDescChars.Location = new System.Drawing.Point(271, 60);
            this.tboxDeltaDescChars.Name = "tboxDeltaDescChars";
            this.tboxDeltaDescChars.ReadOnly = true;
            this.tboxDeltaDescChars.Size = new System.Drawing.Size(292, 22);
            this.tboxDeltaDescChars.TabIndex = 34;
            this.tboxDeltaDescChars.Text = "0 bytes free for desc";
            // 
            // spinItemID
            // 
            this.spinItemID.Enabled = false;
            this.spinItemID.Hexadecimal = true;
            this.spinItemID.Location = new System.Drawing.Point(12, 32);
            this.spinItemID.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinItemID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinItemID.Name = "spinItemID";
            this.spinItemID.Size = new System.Drawing.Size(46, 29);
            this.spinItemID.TabIndex = 35;
            this.spinItemID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinItemID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinItemID.ValueChanged += new System.EventHandler(this.SpinItemID_ValueChanged);
            // 
            // tboxName
            // 
            this.tboxName.Enabled = false;
            this.tboxName.Location = new System.Drawing.Point(64, 32);
            this.tboxName.Name = "tboxName";
            this.tboxName.Size = new System.Drawing.Size(139, 29);
            this.tboxName.TabIndex = 36;
            this.tboxName.TextChanged += new System.EventHandler(this.TboxName_TextChanged);
            // 
            // tboxDesc
            // 
            this.tboxDesc.Enabled = false;
            this.tboxDesc.Location = new System.Drawing.Point(12, 67);
            this.tboxDesc.Multiline = true;
            this.tboxDesc.Name = "tboxDesc";
            this.tboxDesc.Size = new System.Drawing.Size(253, 55);
            this.tboxDesc.TabIndex = 37;
            this.tboxDesc.TextChanged += new System.EventHandler(this.TboxDesc_TextChanged);
            // 
            // grBoxCost
            // 
            this.gboxCost.Controls.Add(this.spinCost);
            this.gboxCost.Location = new System.Drawing.Point(12, 128);
            this.gboxCost.Name = "grBoxCost";
            this.gboxCost.Size = new System.Drawing.Size(92, 65);
            this.gboxCost.TabIndex = 39;
            this.gboxCost.TabStop = false;
            this.gboxCost.Text = "Cost";
            // 
            // spinCost
            // 
            this.spinCost.Enabled = false;
            this.spinCost.Location = new System.Drawing.Point(6, 28);
            this.spinCost.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.spinCost.Name = "spinCost";
            this.spinCost.Size = new System.Drawing.Size(76, 29);
            this.spinCost.TabIndex = 0;
            this.spinCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinCost.ValueChanged += new System.EventHandler(this.SpinCost_ValueChanged);
            // 
            // grBoxByte2
            // 
            this.gboxByte2.Controls.Add(this.spinHeldItemID);
            this.gboxByte2.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gboxByte2.Location = new System.Drawing.Point(328, 129);
            this.gboxByte2.Name = "grBoxByte2";
            this.gboxByte2.Size = new System.Drawing.Size(134, 64);
            this.gboxByte2.TabIndex = 40;
            this.gboxByte2.TabStop = false;
            this.gboxByte2.Text = "Held Item";
            // 
            // spinHeldItemID
            // 
            this.spinHeldItemID.Enabled = false;
            this.spinHeldItemID.Hexadecimal = true;
            this.spinHeldItemID.Location = new System.Drawing.Point(45, 27);
            this.spinHeldItemID.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinHeldItemID.Name = "spinHeldItemID";
            this.spinHeldItemID.Size = new System.Drawing.Size(43, 29);
            this.spinHeldItemID.TabIndex = 0;
            this.spinHeldItemID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinHeldItemID.ValueChanged += new System.EventHandler(this.SpinHeldItemID_ValueChanged);
            // 
            // grBoxParam
            // 
            this.gboxParam.Controls.Add(this.spinParam);
            this.gboxParam.Location = new System.Drawing.Point(224, 129);
            this.gboxParam.Name = "grBoxParam";
            this.gboxParam.Size = new System.Drawing.Size(96, 64);
            this.gboxParam.TabIndex = 41;
            this.gboxParam.TabStop = false;
            this.gboxParam.Text = "Param";
            // 
            // spinParam
            // 
            this.spinParam.Enabled = false;
            this.spinParam.Hexadecimal = true;
            this.spinParam.Location = new System.Drawing.Point(29, 27);
            this.spinParam.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinParam.Name = "spinParam";
            this.spinParam.Size = new System.Drawing.Size(43, 29);
            this.spinParam.TabIndex = 0;
            this.spinParam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinParam.ValueChanged += new System.EventHandler(this.SpinParam_ValueChanged);
            // 
            // grBoxUR
            // 
            this.gboxUR.Controls.Add(this.cboxUseRestriction);
            this.gboxUR.Location = new System.Drawing.Point(12, 199);
            this.gboxUR.Name = "grBoxUR";
            this.gboxUR.Size = new System.Drawing.Size(286, 68);
            this.gboxUR.TabIndex = 43;
            this.gboxUR.TabStop = false;
            this.gboxUR.Text = "Use restriction";
            // 
            // cboxUseRestriction
            // 
            this.cboxUseRestriction.Enabled = false;
            this.cboxUseRestriction.FormattingEnabled = true;
            this.cboxUseRestriction.Items.AddRange(new object[] {
            "Can\'t use",
            "In battle",
            "Out of battle",
            "Out of battle on Pkmn",
            "In or out of battle",
            "Certain areas",
            "Unknown"});
            this.cboxUseRestriction.Location = new System.Drawing.Point(7, 28);
            this.cboxUseRestriction.Name = "cboxUseRestriction";
            this.cboxUseRestriction.Size = new System.Drawing.Size(266, 29);
            this.cboxUseRestriction.TabIndex = 1;
            this.cboxUseRestriction.Text = "Out of battle on Pkmn";
            this.cboxUseRestriction.SelectedIndexChanged += new System.EventHandler(this.CboxUseRestriction_SelectedIndexChanged);
            // 
            // grBoxPocket
            // 
            this.gboxPocket.Controls.Add(this.cboxPocket);
            this.gboxPocket.Location = new System.Drawing.Point(112, 129);
            this.gboxPocket.Name = "grBoxPocket";
            this.gboxPocket.Size = new System.Drawing.Size(104, 64);
            this.gboxPocket.TabIndex = 42;
            this.gboxPocket.TabStop = false;
            this.gboxPocket.Text = "Pocket";
            // 
            // cboxPocket
            // 
            this.cboxPocket.Enabled = false;
            this.cboxPocket.FormattingEnabled = true;
            this.cboxPocket.Items.AddRange(new object[] {
            "Item",
            "Key",
            "Ball",
            "TM"});
            this.cboxPocket.Location = new System.Drawing.Point(13, 24);
            this.cboxPocket.Name = "cboxPocket";
            this.cboxPocket.Size = new System.Drawing.Size(76, 29);
            this.cboxPocket.TabIndex = 0;
            this.cboxPocket.SelectedIndexChanged += new System.EventHandler(this.CboxPocket_SelectedIndexChanged);
            // 
            // grBoxFlag
            // 
            this.gboxFlag.Controls.Add(this.cboxFlagtext);
            this.gboxFlag.Location = new System.Drawing.Point(305, 199);
            this.gboxFlag.Name = "grBoxFlag";
            this.gboxFlag.Size = new System.Drawing.Size(258, 68);
            this.gboxFlag.TabIndex = 43;
            this.gboxFlag.TabStop = false;
            this.gboxFlag.Text = "Flag";
            // 
            // cboxFlagtext
            // 
            this.cboxFlagtext.Enabled = false;
            this.cboxFlagtext.FormattingEnabled = true;
            this.cboxFlagtext.Items.AddRange(new object[] {
            "00 Use Give Sel Toss",
            "40 Use Give     Toss",
            "80 Use      Sel",
            "C0 Use"});
            this.cboxFlagtext.Location = new System.Drawing.Point(6, 27);
            this.cboxFlagtext.Name = "cboxFlagtext";
            this.cboxFlagtext.Size = new System.Drawing.Size(243, 29);
            this.cboxFlagtext.TabIndex = 1;
            this.cboxFlagtext.Text = "00 Use Give Sel Toss";
            this.cboxFlagtext.SelectedIndexChanged += new System.EventHandler(this.CboxFlagtext_SelectedIndexChanged);
            // 
            // gboxASM
            // 
            this.gboxASM.Controls.Add(this.spinASM);
            this.gboxASM.Location = new System.Drawing.Point(471, 128);
            this.gboxASM.Name = "gboxASM";
            this.gboxASM.Size = new System.Drawing.Size(92, 64);
            this.gboxASM.TabIndex = 44;
            this.gboxASM.TabStop = false;
            this.gboxASM.Text = "ASM";
            // 
            // spinASM
            // 
            this.spinASM.Enabled = false;
            this.spinASM.Hexadecimal = true;
            this.spinASM.Location = new System.Drawing.Point(6, 25);
            this.spinASM.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.spinASM.Name = "spinASM";
            this.spinASM.Size = new System.Drawing.Size(74, 29);
            this.spinASM.TabIndex = 0;
            this.spinASM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinASM.ValueChanged += new System.EventHandler(this.SpinASM_ValueChanged);
            // 
            // ItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 276);
            this.Controls.Add(this.gboxASM);
            this.Controls.Add(this.gboxFlag);
            this.Controls.Add(this.gboxUR);
            this.Controls.Add(this.gboxParam);
            this.Controls.Add(this.gboxPocket);
            this.Controls.Add(this.gboxByte2);
            this.Controls.Add(this.gboxCost);
            this.Controls.Add(this.tboxDesc);
            this.Controls.Add(this.tboxName);
            this.Controls.Add(this.spinItemID);
            this.Controls.Add(this.tboxDeltaDescChars);
            this.Controls.Add(this.tboxDeltaNameChars);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "ItemEditor";
            this.Text = "Wargrave Gen2 Item Editor";
            this.Controls.SetChildIndex(this.tboxDeltaNameChars, 0);
            this.Controls.SetChildIndex(this.tboxDeltaDescChars, 0);
            this.Controls.SetChildIndex(this.spinItemID, 0);
            this.Controls.SetChildIndex(this.tboxName, 0);
            this.Controls.SetChildIndex(this.tboxDesc, 0);
            this.Controls.SetChildIndex(this.gboxCost, 0);
            this.Controls.SetChildIndex(this.gboxByte2, 0);
            this.Controls.SetChildIndex(this.gboxPocket, 0);
            this.Controls.SetChildIndex(this.gboxParam, 0);
            this.Controls.SetChildIndex(this.gboxUR, 0);
            this.Controls.SetChildIndex(this.gboxFlag, 0);
            this.Controls.SetChildIndex(this.gboxASM, 0);
            ((System.ComponentModel.ISupportInitialize)(this.spinItemID)).EndInit();
            this.gboxCost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinCost)).EndInit();
            this.gboxByte2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinHeldItemID)).EndInit();
            this.gboxParam.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinParam)).EndInit();
            this.gboxUR.ResumeLayout(false);
            this.gboxPocket.ResumeLayout(false);
            this.gboxFlag.ResumeLayout(false);
            this.gboxASM.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinASM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tboxDeltaNameChars;
        private System.Windows.Forms.TextBox tboxDeltaDescChars;
        private System.Windows.Forms.NumericUpDown spinItemID;
        private System.Windows.Forms.TextBox tboxName;
        private System.Windows.Forms.TextBox tboxDesc;
        private System.Windows.Forms.GroupBox gboxCost;
        private System.Windows.Forms.NumericUpDown spinCost;
        private System.Windows.Forms.GroupBox gboxByte2;
        private System.Windows.Forms.NumericUpDown spinHeldItemID;
        private System.Windows.Forms.GroupBox gboxParam;
        private System.Windows.Forms.NumericUpDown spinParam;
        private System.Windows.Forms.GroupBox gboxUR;
        private System.Windows.Forms.GroupBox gboxPocket;
        private System.Windows.Forms.GroupBox gboxFlag;
        private System.Windows.Forms.ComboBox cboxPocket;
        private System.Windows.Forms.GroupBox gboxASM;
        private System.Windows.Forms.NumericUpDown spinASM;
        private System.Windows.Forms.ComboBox cboxUseRestriction;
        private System.Windows.Forms.ComboBox cboxFlagtext;

    }
}

