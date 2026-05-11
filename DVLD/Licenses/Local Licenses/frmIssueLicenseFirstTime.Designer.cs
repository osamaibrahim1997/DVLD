namespace DVLD.Licenses.Local_Licenses
{
    partial class frmIssueLicenseFirstTime
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNotes = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnIssueLicense = new System.Windows.Forms.Button();
            this.ctrlLocalApplicationInfo1 = new DVLD.Tests.Controls.ctrlLocalApplicationInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(162, 396);
            this.lblNotes.Multiline = true;
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(488, 103);
            this.lblNotes.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Notes_32;
            this.pictureBox1.Location = new System.Drawing.Point(120, 399);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 177;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 396);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 176;
            this.label1.Text = "Notes:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(677, 445);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 179;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnIssueLicense
            // 
            this.btnIssueLicense.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIssueLicense.Image = global::DVLD.Properties.Resources.License_Type_32;
            this.btnIssueLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueLicense.Location = new System.Drawing.Point(677, 398);
            this.btnIssueLicense.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIssueLicense.Name = "btnIssueLicense";
            this.btnIssueLicense.Size = new System.Drawing.Size(126, 37);
            this.btnIssueLicense.TabIndex = 178;
            this.btnIssueLicense.Text = "Issue";
            this.btnIssueLicense.UseVisualStyleBackColor = true;
            this.btnIssueLicense.Click += new System.EventHandler(this.btnIssueLicense_Click);
            // 
            // ctrlLocalApplicationInfo1
            // 
            this.ctrlLocalApplicationInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlLocalApplicationInfo1.Location = new System.Drawing.Point(12, 12);
            this.ctrlLocalApplicationInfo1.Name = "ctrlLocalApplicationInfo1";
            this.ctrlLocalApplicationInfo1.Size = new System.Drawing.Size(872, 378);
            this.ctrlLocalApplicationInfo1.TabIndex = 0;
            // 
            // frmIssueLicenseFirstTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(892, 522);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnIssueLicense);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.ctrlLocalApplicationInfo1);
            this.Name = "frmIssueLicenseFirstTime";
            this.Text = "Issue License First Time";
            this.Load += new System.EventHandler(this.frmIssueLicenseFirstTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tests.Controls.ctrlLocalApplicationInfo ctrlLocalApplicationInfo1;
        private System.Windows.Forms.TextBox lblNotes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnIssueLicense;
    }
}