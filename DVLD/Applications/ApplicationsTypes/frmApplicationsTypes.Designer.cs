namespace DVLD.Applications
{
    partial class frmApplicationsTypes
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
            this.dgvApssTypes = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblAppTypesRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editeApplicationTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApssTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvApssTypes
            // 
            this.dgvApssTypes.BackgroundColor = System.Drawing.Color.White;
            this.dgvApssTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvApssTypes.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvApssTypes.Location = new System.Drawing.Point(12, 162);
            this.dgvApssTypes.Name = "dgvApssTypes";
            this.dgvApssTypes.Size = new System.Drawing.Size(773, 234);
            this.dgvApssTypes.TabIndex = 0;
            this.dgvApssTypes.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvApssTypes_CellMouseDown);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(653, 403);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(135, 36);
            this.btnClose.TabIndex = 98;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblAppTypesRecords
            // 
            this.lblAppTypesRecords.AutoSize = true;
            this.lblAppTypesRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppTypesRecords.Location = new System.Drawing.Point(100, 425);
            this.lblAppTypesRecords.Name = "lblAppTypesRecords";
            this.lblAppTypesRecords.Size = new System.Drawing.Size(39, 16);
            this.lblAppTypesRecords.TabIndex = 97;
            this.lblAppTypesRecords.Text = "????";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 425);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 96;
            this.label2.Text = "Records  : ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Application_Types_512;
            this.pictureBox1.Location = new System.Drawing.Point(304, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(153, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 95;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(246, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 24);
            this.label1.TabIndex = 94;
            this.label1.Text = "Manage Applications Types";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editeApplicationTypeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(192, 48);
            // 
            // editeApplicationTypeToolStripMenuItem
            // 
            this.editeApplicationTypeToolStripMenuItem.Image = global::DVLD.Properties.Resources.edit_32;
            this.editeApplicationTypeToolStripMenuItem.Name = "editeApplicationTypeToolStripMenuItem";
            this.editeApplicationTypeToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.editeApplicationTypeToolStripMenuItem.Text = "Edite Application Type";
            this.editeApplicationTypeToolStripMenuItem.Click += new System.EventHandler(this.editeApplicationTypeToolStripMenuItem_Click);
            // 
            // frmApplicationsTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblAppTypesRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvApssTypes);
            this.Name = "frmApplicationsTypes";
            this.Text = "frmApplicationsTypes";
            this.Load += new System.EventHandler(this.frmApplicationsTypes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvApssTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvApssTypes;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblAppTypesRecords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editeApplicationTypeToolStripMenuItem;
    }
}