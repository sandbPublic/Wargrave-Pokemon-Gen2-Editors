namespace Gen2_Move_Editor {
    partial class MoveEditor {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveEditor));
            this.spinMoveID = new System.Windows.Forms.NumericUpDown();
            this.tboxDeltaNameChars = new System.Windows.Forms.TextBox();
            this.tboxDeltaDescChars = new System.Windows.Forms.TextBox();
            this.spinPower = new System.Windows.Forms.NumericUpDown();
            this.spinAccuracy = new System.Windows.Forms.NumericUpDown();
            this.spinPP = new System.Windows.Forms.NumericUpDown();
            this.spinEffect = new System.Windows.Forms.NumericUpDown();
            this.spinEffectChance = new System.Windows.Forms.NumericUpDown();
            this.spinAnimation = new System.Windows.Forms.NumericUpDown();
            this.tboxEffect = new System.Windows.Forms.TextBox();
            this.tboxEffectChance = new System.Windows.Forms.TextBox();
            this.tboxAnimation = new System.Windows.Forms.TextBox();
            this.tboxAccuracy = new System.Windows.Forms.TextBox();
            this.tboxDesc = new System.Windows.Forms.TextBox();
            this.tboxName = new System.Windows.Forms.TextBox();
            this.cboxType = new System.Windows.Forms.ComboBox();
            this.cboxCrit = new System.Windows.Forms.CheckBox();
            this.tboxDeltaCritBytes = new System.Windows.Forms.TextBox();
            this.gboxEffect = new System.Windows.Forms.GroupBox();
            this.gboxPower = new System.Windows.Forms.GroupBox();
            this.gboxPP = new System.Windows.Forms.GroupBox();
            this.gboxAccuracy = new System.Windows.Forms.GroupBox();
            this.gboxAnimID = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.spinMoveID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEffect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEffectChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinAnimation)).BeginInit();
            this.gboxEffect.SuspendLayout();
            this.gboxPower.SuspendLayout();
            this.gboxPP.SuspendLayout();
            this.gboxAccuracy.SuspendLayout();
            this.gboxAnimID.SuspendLayout();
            this.SuspendLayout();
            // 
            // spinMoveID
            // 
            this.spinMoveID.Enabled = false;
            this.spinMoveID.Hexadecimal = true;
            this.spinMoveID.Location = new System.Drawing.Point(12, 32);
            this.spinMoveID.Maximum = new decimal(new int[] {
            251,
            0,
            0,
            0});
            this.spinMoveID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinMoveID.Name = "spinMoveID";
            this.spinMoveID.Size = new System.Drawing.Size(46, 29);
            this.spinMoveID.TabIndex = 9;
            this.spinMoveID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinMoveID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinMoveID.ValueChanged += new System.EventHandler(this.SpinMoveID_ValueChanged);
            // 
            // tboxDeltaNameChars
            // 
            this.tboxDeltaNameChars.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tboxDeltaNameChars.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxDeltaNameChars.Location = new System.Drawing.Point(272, 31);
            this.tboxDeltaNameChars.Name = "tboxDeltaNameChars";
            this.tboxDeltaNameChars.ReadOnly = true;
            this.tboxDeltaNameChars.Size = new System.Drawing.Size(48, 22);
            this.tboxDeltaNameChars.TabIndex = 10;
            this.tboxDeltaNameChars.Text = "0";
            this.tboxDeltaNameChars.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tboxDeltaDescChars
            // 
            this.tboxDeltaDescChars.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tboxDeltaDescChars.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxDeltaDescChars.Location = new System.Drawing.Point(271, 59);
            this.tboxDeltaDescChars.Name = "tboxDeltaDescChars";
            this.tboxDeltaDescChars.ReadOnly = true;
            this.tboxDeltaDescChars.Size = new System.Drawing.Size(49, 22);
            this.tboxDeltaDescChars.TabIndex = 11;
            this.tboxDeltaDescChars.Text = "0";
            this.tboxDeltaDescChars.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // spinPower
            // 
            this.spinPower.Enabled = false;
            this.spinPower.Location = new System.Drawing.Point(6, 28);
            this.spinPower.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinPower.Name = "spinPower";
            this.spinPower.Size = new System.Drawing.Size(55, 29);
            this.spinPower.TabIndex = 13;
            this.spinPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinPower.ValueChanged += new System.EventHandler(this.SpinPower_ValueChanged);
            // 
            // spinAccuracy
            // 
            this.spinAccuracy.Enabled = false;
            this.spinAccuracy.Hexadecimal = true;
            this.spinAccuracy.Location = new System.Drawing.Point(6, 28);
            this.spinAccuracy.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinAccuracy.Name = "spinAccuracy";
            this.spinAccuracy.Size = new System.Drawing.Size(55, 29);
            this.spinAccuracy.TabIndex = 14;
            this.spinAccuracy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinAccuracy.ValueChanged += new System.EventHandler(this.SpinAccuracy_ValueChanged);
            // 
            // spinPP
            // 
            this.spinPP.Enabled = false;
            this.spinPP.Location = new System.Drawing.Point(6, 28);
            this.spinPP.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinPP.Name = "spinPP";
            this.spinPP.Size = new System.Drawing.Size(55, 29);
            this.spinPP.TabIndex = 15;
            this.spinPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinPP.ValueChanged += new System.EventHandler(this.SpinPP_ValueChanged);
            // 
            // spinEffect
            // 
            this.spinEffect.Enabled = false;
            this.spinEffect.Hexadecimal = true;
            this.spinEffect.Location = new System.Drawing.Point(6, 28);
            this.spinEffect.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinEffect.Name = "spinEffect";
            this.spinEffect.Size = new System.Drawing.Size(55, 29);
            this.spinEffect.TabIndex = 16;
            this.spinEffect.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinEffect.ValueChanged += new System.EventHandler(this.SpinEffect_ValueChanged);
            // 
            // spinEffectChance
            // 
            this.spinEffectChance.Enabled = false;
            this.spinEffectChance.Hexadecimal = true;
            this.spinEffectChance.Location = new System.Drawing.Point(67, 28);
            this.spinEffectChance.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinEffectChance.Name = "spinEffectChance";
            this.spinEffectChance.Size = new System.Drawing.Size(55, 29);
            this.spinEffectChance.TabIndex = 17;
            this.spinEffectChance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinEffectChance.ValueChanged += new System.EventHandler(this.SpinEffectChance_ValueChanged);
            // 
            // spinAnimation
            // 
            this.spinAnimation.Enabled = false;
            this.spinAnimation.Hexadecimal = true;
            this.spinAnimation.Location = new System.Drawing.Point(6, 28);
            this.spinAnimation.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinAnimation.Name = "spinAnimation";
            this.spinAnimation.Size = new System.Drawing.Size(55, 29);
            this.spinAnimation.TabIndex = 18;
            this.spinAnimation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinAnimation.ValueChanged += new System.EventHandler(this.SpinAnimation_ValueChanged);
            // 
            // tboxEffect
            // 
            this.tboxEffect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tboxEffect.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxEffect.Location = new System.Drawing.Point(6, 63);
            this.tboxEffect.Multiline = true;
            this.tboxEffect.Name = "tboxEffect";
            this.tboxEffect.ReadOnly = true;
            this.tboxEffect.Size = new System.Drawing.Size(205, 68);
            this.tboxEffect.TabIndex = 23;
            // 
            // tboxEffectChance
            // 
            this.tboxEffectChance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tboxEffectChance.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxEffectChance.Location = new System.Drawing.Point(128, 28);
            this.tboxEffectChance.Name = "tboxEffectChance";
            this.tboxEffectChance.ReadOnly = true;
            this.tboxEffectChance.Size = new System.Drawing.Size(83, 29);
            this.tboxEffectChance.TabIndex = 24;
            this.tboxEffectChance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tboxAnimation
            // 
            this.tboxAnimation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tboxAnimation.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxAnimation.Location = new System.Drawing.Point(67, 28);
            this.tboxAnimation.Name = "tboxAnimation";
            this.tboxAnimation.ReadOnly = true;
            this.tboxAnimation.Size = new System.Drawing.Size(139, 29);
            this.tboxAnimation.TabIndex = 25;
            // 
            // tboxAccuracy
            // 
            this.tboxAccuracy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tboxAccuracy.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxAccuracy.Location = new System.Drawing.Point(67, 30);
            this.tboxAccuracy.Name = "tboxAccuracy";
            this.tboxAccuracy.ReadOnly = true;
            this.tboxAccuracy.Size = new System.Drawing.Size(83, 29);
            this.tboxAccuracy.TabIndex = 21;
            this.tboxAccuracy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tboxDesc
            // 
            this.tboxDesc.Enabled = false;
            this.tboxDesc.Location = new System.Drawing.Point(11, 67);
            this.tboxDesc.Multiline = true;
            this.tboxDesc.Name = "tboxDesc";
            this.tboxDesc.Size = new System.Drawing.Size(252, 55);
            this.tboxDesc.TabIndex = 26;
            this.tboxDesc.TextChanged += new System.EventHandler(this.TboxMoveDesc_TextChanged);
            // 
            // tboxName
            // 
            this.tboxName.Enabled = false;
            this.tboxName.Location = new System.Drawing.Point(63, 31);
            this.tboxName.Name = "tboxName";
            this.tboxName.Size = new System.Drawing.Size(141, 29);
            this.tboxName.TabIndex = 27;
            this.tboxName.TextChanged += new System.EventHandler(this.TboxMoveName_TextChanged);
            // 
            // cboxType
            // 
            this.cboxType.Enabled = false;
            this.cboxType.FormattingEnabled = true;
            this.cboxType.Location = new System.Drawing.Point(11, 128);
            this.cboxType.Name = "cboxType";
            this.cboxType.Size = new System.Drawing.Size(173, 29);
            this.cboxType.TabIndex = 28;
            this.cboxType.SelectedIndexChanged += new System.EventHandler(this.CboxType_SelectedIndexChanged);
            // 
            // cboxCrit
            // 
            this.cboxCrit.AutoSize = true;
            this.cboxCrit.Enabled = false;
            this.cboxCrit.Location = new System.Drawing.Point(190, 132);
            this.cboxCrit.Name = "cboxCrit";
            this.cboxCrit.Size = new System.Drawing.Size(73, 25);
            this.cboxCrit.TabIndex = 30;
            this.cboxCrit.Text = "crit";
            this.cboxCrit.UseVisualStyleBackColor = true;
            this.cboxCrit.CheckedChanged += new System.EventHandler(this.CboxCrit_CheckedChanged);
            // 
            // tboxDeltaCritBytes
            // 
            this.tboxDeltaCritBytes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tboxDeltaCritBytes.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxDeltaCritBytes.Location = new System.Drawing.Point(272, 87);
            this.tboxDeltaCritBytes.Name = "tboxDeltaCritBytes";
            this.tboxDeltaCritBytes.ReadOnly = true;
            this.tboxDeltaCritBytes.Size = new System.Drawing.Size(49, 22);
            this.tboxDeltaCritBytes.TabIndex = 34;
            this.tboxDeltaCritBytes.Text = "0";
            this.tboxDeltaCritBytes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gboxEffect
            // 
            this.gboxEffect.Controls.Add(this.spinEffect);
            this.gboxEffect.Controls.Add(this.spinEffectChance);
            this.gboxEffect.Controls.Add(this.tboxEffectChance);
            this.gboxEffect.Controls.Add(this.tboxEffect);
            this.gboxEffect.Location = new System.Drawing.Point(326, 163);
            this.gboxEffect.Name = "gboxEffect";
            this.gboxEffect.Size = new System.Drawing.Size(227, 142);
            this.gboxEffect.TabIndex = 35;
            this.gboxEffect.TabStop = false;
            this.gboxEffect.Text = "Effect";
            // 
            // gboxPower
            // 
            this.gboxPower.Controls.Add(this.spinPower);
            this.gboxPower.Location = new System.Drawing.Point(11, 163);
            this.gboxPower.Name = "gboxPower";
            this.gboxPower.Size = new System.Drawing.Size(87, 68);
            this.gboxPower.TabIndex = 36;
            this.gboxPower.TabStop = false;
            this.gboxPower.Text = "Power";
            // 
            // gboxPP
            // 
            this.gboxPP.Controls.Add(this.spinPP);
            this.gboxPP.Location = new System.Drawing.Point(12, 237);
            this.gboxPP.Name = "gboxPP";
            this.gboxPP.Size = new System.Drawing.Size(87, 68);
            this.gboxPP.TabIndex = 37;
            this.gboxPP.TabStop = false;
            this.gboxPP.Text = "PP";
            // 
            // gboxAccuracy
            // 
            this.gboxAccuracy.Controls.Add(this.spinAccuracy);
            this.gboxAccuracy.Controls.Add(this.tboxAccuracy);
            this.gboxAccuracy.Location = new System.Drawing.Point(104, 163);
            this.gboxAccuracy.Name = "gboxAccuracy";
            this.gboxAccuracy.Size = new System.Drawing.Size(169, 68);
            this.gboxAccuracy.TabIndex = 38;
            this.gboxAccuracy.TabStop = false;
            this.gboxAccuracy.Text = "Accuracy";
            // 
            // gboxAnimID
            // 
            this.gboxAnimID.Controls.Add(this.spinAnimation);
            this.gboxAnimID.Controls.Add(this.tboxAnimation);
            this.gboxAnimID.Location = new System.Drawing.Point(104, 237);
            this.gboxAnimID.Name = "gboxAnimID";
            this.gboxAnimID.Size = new System.Drawing.Size(216, 68);
            this.gboxAnimID.TabIndex = 39;
            this.gboxAnimID.TabStop = false;
            this.gboxAnimID.Text = "Animation ID";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(327, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(226, 22);
            this.textBox1.TabIndex = 40;
            this.textBox1.Text = "bytes free for names";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(326, 60);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(226, 22);
            this.textBox2.TabIndex = 41;
            this.textBox2.Text = "bytes free for descs";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(327, 88);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(226, 22);
            this.textBox3.TabIndex = 42;
            this.textBox3.Text = "bytes free for crits";
            // 
            // MoveEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 320);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gboxAnimID);
            this.Controls.Add(this.gboxAccuracy);
            this.Controls.Add(this.gboxPP);
            this.Controls.Add(this.gboxPower);
            this.Controls.Add(this.gboxEffect);
            this.Controls.Add(this.tboxDeltaCritBytes);
            this.Controls.Add(this.cboxCrit);
            this.Controls.Add(this.cboxType);
            this.Controls.Add(this.tboxName);
            this.Controls.Add(this.tboxDesc);
            this.Controls.Add(this.tboxDeltaDescChars);
            this.Controls.Add(this.tboxDeltaNameChars);
            this.Controls.Add(this.spinMoveID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "MoveEditor";
            this.Text = "Wargrave Gen2 Move Editor";
            this.Controls.SetChildIndex(this.spinMoveID, 0);
            this.Controls.SetChildIndex(this.tboxDeltaNameChars, 0);
            this.Controls.SetChildIndex(this.tboxDeltaDescChars, 0);
            this.Controls.SetChildIndex(this.tboxDesc, 0);
            this.Controls.SetChildIndex(this.tboxName, 0);
            this.Controls.SetChildIndex(this.cboxType, 0);
            this.Controls.SetChildIndex(this.cboxCrit, 0);
            this.Controls.SetChildIndex(this.tboxDeltaCritBytes, 0);
            this.Controls.SetChildIndex(this.gboxEffect, 0);
            this.Controls.SetChildIndex(this.gboxPower, 0);
            this.Controls.SetChildIndex(this.gboxPP, 0);
            this.Controls.SetChildIndex(this.gboxAccuracy, 0);
            this.Controls.SetChildIndex(this.gboxAnimID, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.textBox2, 0);
            this.Controls.SetChildIndex(this.textBox3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.spinMoveID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEffect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEffectChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinAnimation)).EndInit();
            this.gboxEffect.ResumeLayout(false);
            this.gboxEffect.PerformLayout();
            this.gboxPower.ResumeLayout(false);
            this.gboxPP.ResumeLayout(false);
            this.gboxAccuracy.ResumeLayout(false);
            this.gboxAccuracy.PerformLayout();
            this.gboxAnimID.ResumeLayout(false);
            this.gboxAnimID.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown spinMoveID;
        private System.Windows.Forms.TextBox tboxDeltaNameChars;
        private System.Windows.Forms.TextBox tboxDeltaDescChars;
        private System.Windows.Forms.NumericUpDown spinPower;
        private System.Windows.Forms.NumericUpDown spinAccuracy;
        private System.Windows.Forms.NumericUpDown spinPP;
        private System.Windows.Forms.NumericUpDown spinEffect;
        private System.Windows.Forms.NumericUpDown spinEffectChance;
        private System.Windows.Forms.NumericUpDown spinAnimation;
        private System.Windows.Forms.TextBox tboxEffect;
        private System.Windows.Forms.TextBox tboxEffectChance;
        private System.Windows.Forms.TextBox tboxAnimation;
        private System.Windows.Forms.TextBox tboxAccuracy;
        private System.Windows.Forms.TextBox tboxDesc;
        private System.Windows.Forms.TextBox tboxName;
        private System.Windows.Forms.ComboBox cboxType;
        private System.Windows.Forms.CheckBox cboxCrit;
        private System.Windows.Forms.TextBox tboxDeltaCritBytes;
        private System.Windows.Forms.GroupBox gboxEffect;
        private System.Windows.Forms.GroupBox gboxPower;
        private System.Windows.Forms.GroupBox gboxPP;
        private System.Windows.Forms.GroupBox gboxAccuracy;
        private System.Windows.Forms.GroupBox gboxAnimID;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
    }
}

