namespace DVLD.People.Controls
{
    partial class ctrPersonCardWithFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrPersonCardWithFilter));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblfilter = new System.Windows.Forms.Label();
            this.btnSeachPerson = new System.Windows.Forms.Button();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ucPersonDetails1 = new DVLD.People.ucPersonDetails();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.lblfilter);
            this.groupBox1.Controls.Add(this.btnSeachPerson);
            this.groupBox1.Controls.Add(this.txtFilterText);
            this.groupBox1.Controls.Add(this.cbFilterBy);
            this.groupBox1.Location = new System.Drawing.Point(74, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 70);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::DVLD.Properties.Resources.Add_Person_40;
            this.btnAdd.Location = new System.Drawing.Point(543, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(47, 47);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lblfilter
            // 
            this.lblfilter.AutoSize = true;
            this.lblfilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfilter.Location = new System.Drawing.Point(33, 26);
            this.lblfilter.Name = "lblfilter";
            this.lblfilter.Size = new System.Drawing.Size(85, 20);
            this.lblfilter.TabIndex = 8;
            this.lblfilter.Text = "Filter By :";
            // 
            // btnSeachPerson
            // 
            this.btnSeachPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeachPerson.Image = ((System.Drawing.Image)(resources.GetObject("btnSeachPerson.Image")));
            this.btnSeachPerson.Location = new System.Drawing.Point(490, 14);
            this.btnSeachPerson.Name = "btnSeachPerson";
            this.btnSeachPerson.Size = new System.Drawing.Size(47, 47);
            this.btnSeachPerson.TabIndex = 3;
            this.btnSeachPerson.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSeachPerson.UseVisualStyleBackColor = true;
            this.btnSeachPerson.Click += new System.EventHandler(this.btnSeachPerson_Click);
            // 
            // txtFilterText
            // 
            this.txtFilterText.Enabled = false;
            this.txtFilterText.Location = new System.Drawing.Point(287, 26);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(144, 20);
            this.txtFilterText.TabIndex = 1;
            this.txtFilterText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterText_KeyPress);
            this.txtFilterText.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateEmptyTextBox);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "PersonID",
            "NationalNo"});
            this.cbFilterBy.Location = new System.Drawing.Point(124, 26);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(157, 21);
            this.cbFilterBy.TabIndex = 0;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ucPersonDetails1
            // 
            this.ucPersonDetails1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ucPersonDetails1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPersonDetails1.Location = new System.Drawing.Point(51, 79);
            this.ucPersonDetails1.Name = "ucPersonDetails1";
            this.ucPersonDetails1.Size = new System.Drawing.Size(663, 266);
            this.ucPersonDetails1.TabIndex = 0;
            this.ucPersonDetails1.Tag = "Person Informaition";
            // 
            // ctrPersonCardWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucPersonDetails1);
            this.Name = "ctrPersonCardWithFilter";
            this.Size = new System.Drawing.Size(791, 363);
            this.AutoValidateChanged += new System.EventHandler(this.ctrPersonCardWithFilter_AutoValidateChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ucPersonDetails ucPersonDetails1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Button btnSeachPerson;
        private System.Windows.Forms.Label lblfilter;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnAdd;
    }
}
