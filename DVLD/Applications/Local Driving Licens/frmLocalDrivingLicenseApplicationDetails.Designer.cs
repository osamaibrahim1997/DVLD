namespace DVLD.Applications.Local_Driving_Licens
{
    partial class frmLocalDrivingLicenseApplicationDetails
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
            this.ctrlLocalApplicationInfo1 = new DVLD.Tests.Controls.ctrlLocalApplicationInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlLocalApplicationInfo1
            // 
            this.ctrlLocalApplicationInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlLocalApplicationInfo1.Location = new System.Drawing.Point(12, 45);
            this.ctrlLocalApplicationInfo1.Name = "ctrlLocalApplicationInfo1";
            this.ctrlLocalApplicationInfo1.Size = new System.Drawing.Size(835, 393);
            this.ctrlLocalApplicationInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(720, 428);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmLocalDrivingLicenseApplicationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(859, 503);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlLocalApplicationInfo1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "frmLocalDrivingLicenseApplicationDetails";
            this.Text = "frmLocalDrivingLicenseApplicationDetails";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplicationDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Tests.Controls.ctrlLocalApplicationInfo ctrlLocalApplicationInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}