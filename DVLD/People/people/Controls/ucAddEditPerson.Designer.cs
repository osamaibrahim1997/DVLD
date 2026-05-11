namespace DVLD.People
{
    partial class ucAddEditPerson
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAddEditPerson));
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirstNamePerson = new System.Windows.Forms.TextBox();
            this.txtNationalNoPerson = new System.Windows.Forms.TextBox();
            this.txtEmailPerson = new System.Windows.Forms.TextBox();
            this.txtSecondNamePerson = new System.Windows.Forms.TextBox();
            this.txtThirdNamePerson = new System.Windows.Forms.TextBox();
            this.txtLastNamePerson = new System.Windows.Forms.TextBox();
            this.txtAdressPerson = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPhonePerson = new System.Windows.Forms.TextBox();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.pictureBoxPerson = new System.Windows.Forms.PictureBox();
            this.btnSavePerson = new System.Windows.Forms.Button();
            this.btnclosePerson = new System.Windows.Forms.Button();
            this.dateTimePersonPicker = new System.Windows.Forms.DateTimePicker();
            this.cbCountries = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbGender = new System.Windows.Forms.GroupBox();
            this.linkLabelSetImage = new System.Windows.Forms.LinkLabel();
            this.linkLabelRemoveImage = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.gbGender.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(18, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Adress : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(18, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Email : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(18, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Gender : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(18, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "National No : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(18, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Name : ";
            // 
            // txtFirstNamePerson
            // 
            this.txtFirstNamePerson.Location = new System.Drawing.Point(163, 38);
            this.txtFirstNamePerson.Name = "txtFirstNamePerson";
            this.txtFirstNamePerson.Size = new System.Drawing.Size(100, 20);
            this.txtFirstNamePerson.TabIndex = 12;
            this.txtFirstNamePerson.Validating += new System.ComponentModel.CancelEventHandler(this.txtFirstNamePerson_Validating);
            // 
            // txtNationalNoPerson
            // 
            this.txtNationalNoPerson.Location = new System.Drawing.Point(163, 77);
            this.txtNationalNoPerson.Name = "txtNationalNoPerson";
            this.txtNationalNoPerson.Size = new System.Drawing.Size(100, 20);
            this.txtNationalNoPerson.TabIndex = 13;
            this.txtNationalNoPerson.Validating += new System.ComponentModel.CancelEventHandler(this.txtNationalNoPerson_Validating_1);
            // 
            // txtEmailPerson
            // 
            this.txtEmailPerson.Location = new System.Drawing.Point(163, 155);
            this.txtEmailPerson.Multiline = true;
            this.txtEmailPerson.Name = "txtEmailPerson";
            this.txtEmailPerson.Size = new System.Drawing.Size(100, 20);
            this.txtEmailPerson.TabIndex = 14;
            this.txtEmailPerson.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmailPerson_Validating);
            // 
            // txtSecondNamePerson
            // 
            this.txtSecondNamePerson.Location = new System.Drawing.Point(296, 37);
            this.txtSecondNamePerson.Name = "txtSecondNamePerson";
            this.txtSecondNamePerson.Size = new System.Drawing.Size(100, 20);
            this.txtSecondNamePerson.TabIndex = 15;
            this.txtSecondNamePerson.Validating += new System.ComponentModel.CancelEventHandler(this.txtSecondNamePerson_Validating);
            // 
            // txtThirdNamePerson
            // 
            this.txtThirdNamePerson.Location = new System.Drawing.Point(429, 37);
            this.txtThirdNamePerson.Name = "txtThirdNamePerson";
            this.txtThirdNamePerson.Size = new System.Drawing.Size(100, 20);
            this.txtThirdNamePerson.TabIndex = 16;
            this.txtThirdNamePerson.Validated += new System.EventHandler(this.txtThirdNamePerson_Validated);
            // 
            // txtLastNamePerson
            // 
            this.txtLastNamePerson.Location = new System.Drawing.Point(562, 37);
            this.txtLastNamePerson.Name = "txtLastNamePerson";
            this.txtLastNamePerson.Size = new System.Drawing.Size(100, 20);
            this.txtLastNamePerson.TabIndex = 17;
            this.txtLastNamePerson.Validating += new System.ComponentModel.CancelEventHandler(this.txtLastNamePerson_Validating);
            // 
            // txtAdressPerson
            // 
            this.txtAdressPerson.Location = new System.Drawing.Point(163, 194);
            this.txtAdressPerson.Multiline = true;
            this.txtAdressPerson.Name = "txtAdressPerson";
            this.txtAdressPerson.Size = new System.Drawing.Size(410, 62);
            this.txtAdressPerson.TabIndex = 18;
            this.txtAdressPerson.Validating += new System.ComponentModel.CancelEventHandler(this.txtAdressPerson_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(188, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "First";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(307, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "Second";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(597, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Last";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(453, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 16);
            this.label9.TabIndex = 22;
            this.label9.Text = "Third";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(294, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 16);
            this.label10.TabIndex = 25;
            this.label10.Text = "Country : ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(294, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 16);
            this.label11.TabIndex = 24;
            this.label11.Text = "Phone : ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(294, 81);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 16);
            this.label12.TabIndex = 23;
            this.label12.Text = "Date Of Birth :";
            // 
            // txtPhonePerson
            // 
            this.txtPhonePerson.Location = new System.Drawing.Point(394, 116);
            this.txtPhonePerson.Name = "txtPhonePerson";
            this.txtPhonePerson.Size = new System.Drawing.Size(200, 20);
            this.txtPhonePerson.TabIndex = 27;
            this.txtPhonePerson.Validating += new System.ComponentModel.CancelEventHandler(this.txtPhonePerson_Validating);
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Location = new System.Drawing.Point(6, 10);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(48, 17);
            this.rbMale.TabIndex = 29;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            this.rbMale.CheckedChanged += new System.EventHandler(this.rbMale_CheckedChanged);
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(60, 10);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(59, 17);
            this.rbFemale.TabIndex = 30;
            this.rbFemale.TabStop = true;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;
            this.rbFemale.CheckedChanged += new System.EventHandler(this.rbFemale_CheckedChanged);
            // 
            // pictureBoxPerson
            // 
            this.pictureBoxPerson.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxPerson.ImageLocation = "";
            this.pictureBoxPerson.InitialImage = null;
            this.pictureBoxPerson.Location = new System.Drawing.Point(600, 71);
            this.pictureBoxPerson.Name = "pictureBoxPerson";
            this.pictureBoxPerson.Size = new System.Drawing.Size(143, 155);
            this.pictureBoxPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPerson.TabIndex = 31;
            this.pictureBoxPerson.TabStop = false;
            this.pictureBoxPerson.Validating += new System.ComponentModel.CancelEventHandler(this.pictureBoxPerson_Validating);
            // 
            // btnSavePerson
            // 
            this.btnSavePerson.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSavePerson.BackgroundImage")));
            this.btnSavePerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSavePerson.Image = ((System.Drawing.Image)(resources.GetObject("btnSavePerson.Image")));
            this.btnSavePerson.Location = new System.Drawing.Point(498, 262);
            this.btnSavePerson.Name = "btnSavePerson";
            this.btnSavePerson.Size = new System.Drawing.Size(105, 46);
            this.btnSavePerson.TabIndex = 32;
            this.btnSavePerson.Text = "Save";
            this.btnSavePerson.UseVisualStyleBackColor = true;
            this.btnSavePerson.Click += new System.EventHandler(this.btnSavePerson_Click);
            // 
            // btnclosePerson
            // 
            this.btnclosePerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnclosePerson.Location = new System.Drawing.Point(387, 262);
            this.btnclosePerson.Name = "btnclosePerson";
            this.btnclosePerson.Size = new System.Drawing.Size(105, 46);
            this.btnclosePerson.TabIndex = 33;
            this.btnclosePerson.Text = "Close";
            this.btnclosePerson.UseVisualStyleBackColor = true;
            this.btnclosePerson.Click += new System.EventHandler(this.btnclosePerson_Click);
            // 
            // dateTimePersonPicker
            // 
            this.dateTimePersonPicker.Location = new System.Drawing.Point(394, 81);
            this.dateTimePersonPicker.Name = "dateTimePersonPicker";
            this.dateTimePersonPicker.Size = new System.Drawing.Size(200, 20);
            this.dateTimePersonPicker.TabIndex = 34;
            // 
            // cbCountries
            // 
            this.cbCountries.FormattingEnabled = true;
            this.cbCountries.Location = new System.Drawing.Point(394, 155);
            this.cbCountries.Name = "cbCountries";
            this.cbCountries.Size = new System.Drawing.Size(200, 21);
            this.cbCountries.TabIndex = 35;
            this.cbCountries.Validating += new System.ComponentModel.CancelEventHandler(this.cbCountries_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // gbGender
            // 
            this.gbGender.Controls.Add(this.rbMale);
            this.gbGender.Controls.Add(this.rbFemale);
            this.gbGender.Location = new System.Drawing.Point(163, 107);
            this.gbGender.Name = "gbGender";
            this.gbGender.Size = new System.Drawing.Size(115, 29);
            this.gbGender.TabIndex = 36;
            this.gbGender.TabStop = false;
            // 
            // linkLabelSetImage
            // 
            this.linkLabelSetImage.CausesValidation = false;
            this.linkLabelSetImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelSetImage.LinkArea = new System.Windows.Forms.LinkArea(0, 9);
            this.linkLabelSetImage.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLabelSetImage.Location = new System.Drawing.Point(600, 229);
            this.linkLabelSetImage.Name = "linkLabelSetImage";
            this.linkLabelSetImage.Size = new System.Drawing.Size(143, 27);
            this.linkLabelSetImage.TabIndex = 37;
            this.linkLabelSetImage.TabStop = true;
            this.linkLabelSetImage.Text = "Set Image";
            this.linkLabelSetImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelSetImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSetImage_LinkClicked);
            // 
            // linkLabelRemoveImage
            // 
            this.linkLabelRemoveImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelRemoveImage.Location = new System.Drawing.Point(604, 274);
            this.linkLabelRemoveImage.Name = "linkLabelRemoveImage";
            this.linkLabelRemoveImage.Size = new System.Drawing.Size(139, 27);
            this.linkLabelRemoveImage.TabIndex = 38;
            this.linkLabelRemoveImage.TabStop = true;
            this.linkLabelRemoveImage.Text = "Remove Image";
            this.linkLabelRemoveImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelRemoveImage.Visible = false;
            this.linkLabelRemoveImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRemoveImage_LinkClicked);
            // 
            // ucAddEditPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabelRemoveImage);
            this.Controls.Add(this.linkLabelSetImage);
            this.Controls.Add(this.gbGender);
            this.Controls.Add(this.cbCountries);
            this.Controls.Add(this.dateTimePersonPicker);
            this.Controls.Add(this.btnclosePerson);
            this.Controls.Add(this.btnSavePerson);
            this.Controls.Add(this.pictureBoxPerson);
            this.Controls.Add(this.txtPhonePerson);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAdressPerson);
            this.Controls.Add(this.txtLastNamePerson);
            this.Controls.Add(this.txtThirdNamePerson);
            this.Controls.Add(this.txtSecondNamePerson);
            this.Controls.Add(this.txtEmailPerson);
            this.Controls.Add(this.txtNationalNoPerson);
            this.Controls.Add(this.txtFirstNamePerson);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "ucAddEditPerson";
            this.Size = new System.Drawing.Size(772, 326);
            this.Load += new System.EventHandler(this.ucAddEditPerson_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.gbGender.ResumeLayout(false);
            this.gbGender.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFirstNamePerson;
        private System.Windows.Forms.TextBox txtNationalNoPerson;
        private System.Windows.Forms.TextBox txtEmailPerson;
        private System.Windows.Forms.TextBox txtSecondNamePerson;
        private System.Windows.Forms.TextBox txtThirdNamePerson;
        private System.Windows.Forms.TextBox txtLastNamePerson;
        private System.Windows.Forms.TextBox txtAdressPerson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPhonePerson;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.PictureBox pictureBoxPerson;
        private System.Windows.Forms.Button btnSavePerson;
        private System.Windows.Forms.Button btnclosePerson;
        private System.Windows.Forms.DateTimePicker dateTimePersonPicker;
        private System.Windows.Forms.ComboBox cbCountries;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox gbGender;
        private System.Windows.Forms.LinkLabel linkLabelRemoveImage;
        private System.Windows.Forms.LinkLabel linkLabelSetImage;
    }
}
