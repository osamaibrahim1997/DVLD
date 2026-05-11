namespace DVLD.People
{
    partial class frmAddEditPerson
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
            this.lblAddEditPerson = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPersonID = new System.Windows.Forms.Label();
            this.ucAddEditPerson = new DVLD.People.ucAddEditPerson();
            this.SuspendLayout();
            // 
            // lblAddEditPerson
            // 
            this.lblAddEditPerson.AutoSize = true;
            this.lblAddEditPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddEditPerson.ForeColor = System.Drawing.Color.Red;
            this.lblAddEditPerson.Location = new System.Drawing.Point(363, 15);
            this.lblAddEditPerson.Name = "lblAddEditPerson";
            this.lblAddEditPerson.Size = new System.Drawing.Size(88, 20);
            this.lblAddEditPerson.TabIndex = 0;
            this.lblAddEditPerson.Text = "Add / Edit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(49, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Person ID : ";
            // 
            // lblPersonID
            // 
            this.lblPersonID.AutoSize = true;
            this.lblPersonID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersonID.ForeColor = System.Drawing.Color.Black;
            this.lblPersonID.Location = new System.Drawing.Point(159, 48);
            this.lblPersonID.Name = "lblPersonID";
            this.lblPersonID.Size = new System.Drawing.Size(38, 20);
            this.lblPersonID.TabIndex = 2;
            this.lblPersonID.Text = "N\\A";
            // 
            // ucAddEditPerson
            // 
            this.ucAddEditPerson.Location = new System.Drawing.Point(12, 71);
            this.ucAddEditPerson.Name = "ucAddEditPerson";
            this.ucAddEditPerson.Size = new System.Drawing.Size(761, 326);
            this.ucAddEditPerson.TabIndex = 3;
            this.ucAddEditPerson.OnSaveButtonClicking += new DVLD.People.ucAddEditPerson.OnSaveButtonClick(this.ucAddEditPerson_OnSaveButtonClicking);
            this.ucAddEditPerson.Load += new System.EventHandler(this.ucAddEditPerson_Load);
            // 
            // frmAddEditPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ucAddEditPerson);
            this.Controls.Add(this.lblPersonID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAddEditPerson);
            this.Name = "frmAddEditPerson";
            this.Text = "frmAddEditPerson";
            this.Load += new System.EventHandler(this.frmAddEditPerson_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAddEditPerson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPersonID;
        private ucAddEditPerson ucAddEditPerson;
    }
}