namespace DVLD.Tests.Tests_Applointments
{
    partial class frmSchedualTest
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
            this.ctrlSchedualTest1 = new DVLD.Tests.Controls.ctrlSchedualTest();
            this.SuspendLayout();
            // 
            // ctrlSchedualTest1
            // 
            this.ctrlSchedualTest1.BackColor = System.Drawing.Color.White;
            this.ctrlSchedualTest1.Location = new System.Drawing.Point(22, 12);
            this.ctrlSchedualTest1.Name = "ctrlSchedualTest1";
            this.ctrlSchedualTest1.Size = new System.Drawing.Size(545, 611);
            this.ctrlSchedualTest1.TabIndex = 0;
            this.ctrlSchedualTest1.TestAppointmentID = -1;
            this.ctrlSchedualTest1.TestTypeID = DVLD_Business.clsTestType.enTestType.WrittenTest;
            // 
            // frmSchedualTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(590, 683);
            this.Controls.Add(this.ctrlSchedualTest1);
            this.Name = "frmSchedualTest";
            this.Text = "frmSchedualTest";
            this.Load += new System.EventHandler(this.frmSchedualTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlSchedualTest ctrlSchedualTest1;
    }
}