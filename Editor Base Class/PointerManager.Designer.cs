namespace Editor_Base_Class {
    partial class PointerManager<T> {
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
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.btnUncheckAll = new System.Windows.Forms.Button();
            this.btnUpdatePtrs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Location = new System.Drawing.Point(13, 13);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(140, 42);
            this.btnCheckAll.TabIndex = 0;
            this.btnCheckAll.Text = "Check all";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.BtnCheckAll_Click);
            // 
            // btnUncheckAll
            // 
            this.btnUncheckAll.Location = new System.Drawing.Point(159, 13);
            this.btnUncheckAll.Name = "btnUncheckAll";
            this.btnUncheckAll.Size = new System.Drawing.Size(141, 42);
            this.btnUncheckAll.TabIndex = 1;
            this.btnUncheckAll.Text = "Uncheck all";
            this.btnUncheckAll.UseVisualStyleBackColor = true;
            this.btnUncheckAll.Click += new System.EventHandler(this.BtnUncheckAll_Click);
            // 
            // btnUpdatePtrs
            // 
            this.btnUpdatePtrs.Location = new System.Drawing.Point(306, 12);
            this.btnUpdatePtrs.Name = "btnUpdatePtrs";
            this.btnUpdatePtrs.Size = new System.Drawing.Size(245, 42);
            this.btnUpdatePtrs.TabIndex = 2;
            this.btnUpdatePtrs.Text = "Update pointers now";
            this.btnUpdatePtrs.UseVisualStyleBackColor = true;
            this.btnUpdatePtrs.Click += new System.EventHandler(this.BtnUpdatePtrs_Click);
            // 
            // PointerManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 61);
            this.Controls.Add(this.btnUpdatePtrs);
            this.Controls.Add(this.btnUncheckAll);
            this.Controls.Add(this.btnCheckAll);
            this.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "PointerManager";
            this.Text = "PointerManager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Button btnUncheckAll;
        private System.Windows.Forms.Button btnUpdatePtrs;

        private System.Windows.Forms.CheckBox[] checkDiscontig;
    }
}