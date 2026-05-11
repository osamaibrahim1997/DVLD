namespace DVLD.Licenses
{
    partial class frmShowLicensesHestory
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ctrlDriverLicenses1 = new DVLD.Licenses.controls.ctrlDriverLicenses();
            this.ucPersonDetails1 = new DVLD.People.ucPersonDetails();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(179, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(680, 39);
            this.lblTitle.TabIndex = 130;
            this.lblTitle.Text = "License History";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_512;
            this.pictureBox1.Location = new System.Drawing.Point(12, 77);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(161, 192);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ctrlDriverLicenses1
            // 
            this.ctrlDriverLicenses1.AutoScroll = true;
            this.ctrlDriverLicenses1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ctrlDriverLicenses1.Location = new System.Drawing.Point(12, 351);
            this.ctrlDriverLicenses1.Name = "ctrlDriverLicenses1";
            this.ctrlDriverLicenses1.Size = new System.Drawing.Size(864, 351);
            this.ctrlDriverLicenses1.TabIndex = 133;
            // 
            // ucPersonDetails1
            // 
            this.ucPersonDetails1.Location = new System.Drawing.Point(186, 77);
            this.ucPersonDetails1.Name = "ucPersonDetails1";
            this.ucPersonDetails1.Size = new System.Drawing.Size(663, 253);
            this.ucPersonDetails1.TabIndex = 132;
            this.ucPersonDetails1.Tag = "Person Informaition";
            // 
            // frmShowLicensesHestory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(889, 749);
            this.Controls.Add(this.ctrlDriverLicenses1);
            this.Controls.Add(this.ucPersonDetails1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmShowLicensesHestory";
            this.Text = "Licenses Hestory";
            this.Load += new System.EventHandler(this.frmShowLicensesHestory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private People.ucPersonDetails ucPersonDetails1;
        private controls.ctrlDriverLicenses ctrlDriverLicenses1;
    }
}