namespace DVLD.People
{
    partial class frmListPeople
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListPeople));
            this.dgvPeople = new System.Windows.Forms.DataGridView();
            this.cmsPeople = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsAddNewPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.lblfilter = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPeopleRecords = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).BeginInit();
            this.cmsPeople.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPeople
            // 
            this.dgvPeople.AllowUserToAddRows = false;
            this.dgvPeople.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeople.ContextMenuStrip = this.cmsPeople;
            this.dgvPeople.Location = new System.Drawing.Point(12, 186);
            this.dgvPeople.Name = "dgvPeople";
            this.dgvPeople.Size = new System.Drawing.Size(776, 173);
            this.dgvPeople.TabIndex = 0;
            this.dgvPeople.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPeople_CellMouseDown);
            // 
            // cmsPeople
            // 
            this.cmsPeople.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsShowDetails,
            this.toolStripSeparator1,
            this.cmsAddNewPerson,
            this.cmsEditPerson,
            this.cmsDelete,
            this.toolStripSeparator2,
            this.cmsSendEmail,
            this.cmsPhoneCall});
            this.cmsPeople.Name = "cmsPeople";
            this.cmsPeople.Size = new System.Drawing.Size(181, 170);
            // 
            // cmsShowDetails
            // 
            this.cmsShowDetails.Image = ((System.Drawing.Image)(resources.GetObject("cmsShowDetails.Image")));
            this.cmsShowDetails.Name = "cmsShowDetails";
            this.cmsShowDetails.Size = new System.Drawing.Size(180, 22);
            this.cmsShowDetails.Text = "Show Details";
            this.cmsShowDetails.Click += new System.EventHandler(this.cmsShowDetails_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // cmsAddNewPerson
            // 
            this.cmsAddNewPerson.Image = ((System.Drawing.Image)(resources.GetObject("cmsAddNewPerson.Image")));
            this.cmsAddNewPerson.Name = "cmsAddNewPerson";
            this.cmsAddNewPerson.Size = new System.Drawing.Size(180, 22);
            this.cmsAddNewPerson.Text = "Add New Person";
            this.cmsAddNewPerson.Click += new System.EventHandler(this.cmsAddNewPerson_Click);
            // 
            // cmsEditPerson
            // 
            this.cmsEditPerson.Image = ((System.Drawing.Image)(resources.GetObject("cmsEditPerson.Image")));
            this.cmsEditPerson.Name = "cmsEditPerson";
            this.cmsEditPerson.Size = new System.Drawing.Size(180, 22);
            this.cmsEditPerson.Text = "Edit";
            this.cmsEditPerson.Click += new System.EventHandler(this.cmsEditPerson_Click);
            // 
            // cmsDelete
            // 
            this.cmsDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmsDelete.Image")));
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(180, 22);
            this.cmsDelete.Text = "Delete";
            this.cmsDelete.Click += new System.EventHandler(this.cmsDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // cmsSendEmail
            // 
            this.cmsSendEmail.Image = ((System.Drawing.Image)(resources.GetObject("cmsSendEmail.Image")));
            this.cmsSendEmail.Name = "cmsSendEmail";
            this.cmsSendEmail.Size = new System.Drawing.Size(180, 22);
            this.cmsSendEmail.Text = "Send Email";
            // 
            // cmsPhoneCall
            // 
            this.cmsPhoneCall.Image = ((System.Drawing.Image)(resources.GetObject("cmsPhoneCall.Image")));
            this.cmsPhoneCall.Name = "cmsPhoneCall";
            this.cmsPhoneCall.Size = new System.Drawing.Size(180, 22);
            this.cmsPhoneCall.Text = "Phone Call";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(349, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Manage People";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Location = new System.Drawing.Point(218, 159);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(125, 20);
            this.txtFilterText.TabIndex = 6;
            this.txtFilterText.Visible = false;
            this.txtFilterText.TextChanged += new System.EventHandler(this.txtFilterText_TextChanged);
            this.txtFilterText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterText_KeyPress);
            // 
            // lblfilter
            // 
            this.lblfilter.AutoSize = true;
            this.lblfilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfilter.Location = new System.Drawing.Point(13, 162);
            this.lblfilter.Name = "lblfilter";
            this.lblfilter.Size = new System.Drawing.Size(72, 16);
            this.lblfilter.TabIndex = 7;
            this.lblfilter.Text = "Filter By :";
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No.",
            "First Name",
            "Second Name",
            "Third Name",
            "Last Name",
            "Nationality",
            "Gendor",
            "Phone",
            "Email"});
            this.cbFilter.Location = new System.Drawing.Point(91, 159);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(121, 21);
            this.cbFilter.TabIndex = 8;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 372);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "People : ";
            // 
            // lblPeopleRecords
            // 
            this.lblPeopleRecords.AutoSize = true;
            this.lblPeopleRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeopleRecords.Location = new System.Drawing.Point(88, 372);
            this.lblPeopleRecords.Name = "lblPeopleRecords";
            this.lblPeopleRecords.Size = new System.Drawing.Size(39, 16);
            this.lblPeopleRecords.TabIndex = 10;
            this.lblPeopleRecords.Text = "????";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(353, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(153, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPerson.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPerson.Image")));
            this.btnAddPerson.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddPerson.Location = new System.Drawing.Point(694, 135);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(94, 45);
            this.btnAddPerson.TabIndex = 1;
            this.btnAddPerson.Text = "Add Person";
            this.btnAddPerson.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(653, 372);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(135, 36);
            this.btnClose.TabIndex = 93;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmListPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 749);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblPeopleRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.lblfilter);
            this.Controls.Add(this.txtFilterText);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.dgvPeople);
            this.Name = "frmListPeople";
            this.Text = "Manage People";
            this.Load += new System.EventHandler(this.frmManagePeople_Load);
            this.Click += new System.EventHandler(this.frmListPeople_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).EndInit();
            this.cmsPeople.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPeople;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip cmsPeople;
        private System.Windows.Forms.ToolStripMenuItem cmsShowDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cmsAddNewPerson;
        private System.Windows.Forms.ToolStripMenuItem cmsEditPerson;
        private System.Windows.Forms.ToolStripMenuItem cmsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cmsSendEmail;
        private System.Windows.Forms.ToolStripMenuItem cmsPhoneCall;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Label lblfilter;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPeopleRecords;
        private System.Windows.Forms.Button btnClose;
    }
}