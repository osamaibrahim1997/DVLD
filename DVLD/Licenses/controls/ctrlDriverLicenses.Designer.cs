namespace DVLD.Licenses.controls
{
    partial class ctrlDriverLicenses
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tcDriverLicenses = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblLocalCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvLocal = new System.Windows.Forms.DataGridView();
            this.LocalcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLicenseInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblLocalinternationalRecords = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvInternationalLicensesHistory = new System.Windows.Forms.DataGridView();
            this.InternationalcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.tcDriverLicenses.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocal)).BeginInit();
            this.LocalcontextMenuStrip.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesHistory)).BeginInit();
            this.InternationalcontextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tcDriverLicenses);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(850, 411);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Licenses";
            // 
            // tcDriverLicenses
            // 
            this.tcDriverLicenses.Controls.Add(this.tabPage1);
            this.tcDriverLicenses.Controls.Add(this.tabPage2);
            this.tcDriverLicenses.Location = new System.Drawing.Point(6, 42);
            this.tcDriverLicenses.Name = "tcDriverLicenses";
            this.tcDriverLicenses.SelectedIndex = 0;
            this.tcDriverLicenses.Size = new System.Drawing.Size(838, 369);
            this.tcDriverLicenses.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblLocalCount);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dgvLocal);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(830, 336);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Local Licenses";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblLocalCount
            // 
            this.lblLocalCount.AutoSize = true;
            this.lblLocalCount.Location = new System.Drawing.Point(100, 267);
            this.lblLocalCount.Name = "lblLocalCount";
            this.lblLocalCount.Size = new System.Drawing.Size(29, 20);
            this.lblLocalCount.TabIndex = 140;
            this.lblLocalCount.Text = "??";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 139;
            this.label2.Text = "# Records:";
            // 
            // dgvLocal
            // 
            this.dgvLocal.AllowUserToAddRows = false;
            this.dgvLocal.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocal.ContextMenuStrip = this.LocalcontextMenuStrip;
            this.dgvLocal.Location = new System.Drawing.Point(6, 48);
            this.dgvLocal.Name = "dgvLocal";
            this.dgvLocal.Size = new System.Drawing.Size(818, 202);
            this.dgvLocal.TabIndex = 138;
            this.dgvLocal.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLocal_CellMouseDown);
            // 
            // LocalcontextMenuStrip
            // 
            this.LocalcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLicenseInformationToolStripMenuItem});
            this.LocalcontextMenuStrip.Name = "LocalcontextMenuStrip";
            this.LocalcontextMenuStrip.Size = new System.Drawing.Size(212, 26);
            // 
            // showLicenseInformationToolStripMenuItem
            // 
            this.showLicenseInformationToolStripMenuItem.Image = global::DVLD.Properties.Resources.License_View_32;
            this.showLicenseInformationToolStripMenuItem.Name = "showLicenseInformationToolStripMenuItem";
            this.showLicenseInformationToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.showLicenseInformationToolStripMenuItem.Text = "Show License Information";
            this.showLicenseInformationToolStripMenuItem.Click += new System.EventHandler(this.showLicenseInformationToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 20);
            this.label3.TabIndex = 137;
            this.label3.Text = "Local Licenses History :";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblLocalinternationalRecords);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.dgvInternationalLicensesHistory);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(830, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Internatioal Licenses";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblLocalinternationalRecords
            // 
            this.lblLocalinternationalRecords.AutoSize = true;
            this.lblLocalinternationalRecords.Location = new System.Drawing.Point(110, 277);
            this.lblLocalinternationalRecords.Name = "lblLocalinternationalRecords";
            this.lblLocalinternationalRecords.Size = new System.Drawing.Size(29, 20);
            this.lblLocalinternationalRecords.TabIndex = 138;
            this.lblLocalinternationalRecords.Text = "??";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 277);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 20);
            this.label5.TabIndex = 137;
            this.label5.Text = "# Records:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 20);
            this.label1.TabIndex = 136;
            this.label1.Text = "International Licenses History :";
            // 
            // dgvInternationalLicensesHistory
            // 
            this.dgvInternationalLicensesHistory.AllowUserToAddRows = false;
            this.dgvInternationalLicensesHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvInternationalLicensesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalLicensesHistory.ContextMenuStrip = this.InternationalcontextMenuStrip;
            this.dgvInternationalLicensesHistory.Location = new System.Drawing.Point(6, 61);
            this.dgvInternationalLicensesHistory.Name = "dgvInternationalLicensesHistory";
            this.dgvInternationalLicensesHistory.Size = new System.Drawing.Size(818, 202);
            this.dgvInternationalLicensesHistory.TabIndex = 0;
            this.dgvInternationalLicensesHistory.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvInternationalLicensesHistory_CellMouseDown);
            // 
            // InternationalcontextMenuStrip
            // 
            this.InternationalcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.InternationalcontextMenuStrip.Name = "contextMenuStrip1";
            this.InternationalcontextMenuStrip.Size = new System.Drawing.Size(212, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::DVLD.Properties.Resources.License_View_32;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(211, 22);
            this.toolStripMenuItem1.Text = "Show License Information";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // ctrlDriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrlDriverLicenses";
            this.Size = new System.Drawing.Size(868, 448);
            this.groupBox1.ResumeLayout(false);
            this.tcDriverLicenses.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocal)).EndInit();
            this.LocalcontextMenuStrip.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesHistory)).EndInit();
            this.InternationalcontextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tcDriverLicenses;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvInternationalLicensesHistory;
        private System.Windows.Forms.DataGridView dgvLocal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLocalCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLocalinternationalRecords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip LocalcontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showLicenseInformationToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip InternationalcontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
