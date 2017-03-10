namespace Gen2_Move_Animation_Editor {
    partial class MoveAnimationEditor {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveAnimationEditor));
            this.spinAnimID = new System.Windows.Forms.NumericUpDown();
            this.txtMoveName = new System.Windows.Forms.TextBox();
            this.rTxtBytes = new System.Windows.Forms.RichTextBox();
            this.rTxtCode = new System.Windows.Forms.RichTextBox();
            this.txtBytesFree = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.spinAnimID)).BeginInit();
            this.SuspendLayout();
            // 
            // spinAnimID
            // 
            this.spinAnimID.Enabled = false;
            this.spinAnimID.Hexadecimal = true;
            this.spinAnimID.Location = new System.Drawing.Point(13, 33);
            this.spinAnimID.Name = "spinAnimID";
            this.spinAnimID.Size = new System.Drawing.Size(57, 29);
            this.spinAnimID.TabIndex = 1;
            this.spinAnimID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinAnimID.ValueChanged += new System.EventHandler(this.spinAnimID_ValueChanged);
            // 
            // txtMoveName
            // 
            this.txtMoveName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMoveName.Location = new System.Drawing.Point(76, 33);
            this.txtMoveName.Name = "txtMoveName";
            this.txtMoveName.ReadOnly = true;
            this.txtMoveName.Size = new System.Drawing.Size(140, 29);
            this.txtMoveName.TabIndex = 2;
            // 
            // rTxtBytes
            // 
            this.rTxtBytes.Location = new System.Drawing.Point(12, 68);
            this.rTxtBytes.Name = "rTxtBytes";
            this.rTxtBytes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rTxtBytes.Size = new System.Drawing.Size(182, 344);
            this.rTxtBytes.TabIndex = 3;
            this.rTxtBytes.Text = "";
            this.rTxtBytes.TextChanged += new System.EventHandler(this.rTxtBytes_TextChanged);
            // 
            // rTxtCode
            // 
            this.rTxtCode.Location = new System.Drawing.Point(200, 68);
            this.rTxtCode.Name = "rTxtCode";
            this.rTxtCode.ReadOnly = true;
            this.rTxtCode.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rTxtCode.Size = new System.Drawing.Size(284, 344);
            this.rTxtCode.TabIndex = 4;
            this.rTxtCode.Text = "";
            // 
            // txtBytesFree
            // 
            this.txtBytesFree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBytesFree.Location = new System.Drawing.Point(222, 32);
            this.txtBytesFree.Name = "txtBytesFree";
            this.txtBytesFree.ReadOnly = true;
            this.txtBytesFree.Size = new System.Drawing.Size(262, 29);
            this.txtBytesFree.TabIndex = 6;
            this.txtBytesFree.Text = "0 bytes free";
            // 
            // Animation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 422);
            this.Controls.Add(this.txtBytesFree);
            this.Controls.Add(this.rTxtCode);
            this.Controls.Add(this.rTxtBytes);
            this.Controls.Add(this.txtMoveName);
            this.Controls.Add(this.spinAnimID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Animation";
            this.Text = "Wargrave Gen2 Animation Editor";
            this.Controls.SetChildIndex(this.spinAnimID, 0);
            this.Controls.SetChildIndex(this.txtMoveName, 0);
            this.Controls.SetChildIndex(this.rTxtBytes, 0);
            this.Controls.SetChildIndex(this.rTxtCode, 0);
            this.Controls.SetChildIndex(this.txtBytesFree, 0);
            ((System.ComponentModel.ISupportInitialize)(this.spinAnimID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown spinAnimID;
        private System.Windows.Forms.TextBox txtMoveName;
        private System.Windows.Forms.RichTextBox rTxtBytes;
        private System.Windows.Forms.RichTextBox rTxtCode;
        private System.Windows.Forms.TextBox txtBytesFree;
    }
}

