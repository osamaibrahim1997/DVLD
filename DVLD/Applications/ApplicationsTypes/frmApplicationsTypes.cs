using DVLD.People;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmApplicationsTypes : Form
    {
        DataTable dtAppsTypes;
        public frmApplicationsTypes()
        {
            InitializeComponent();
        }

        private void _RefreshAppsTypesTable()
        {
            dtAppsTypes = clsApplicationsTypes.GetAllAppsTypes();
            dgvApssTypes.DataSource = dtAppsTypes;
            lblAppTypesRecords.Text = dgvApssTypes.Rows.Count.ToString();

        }

        private void frmApplicationsTypes_Load(object sender, EventArgs e)
        {
            dtAppsTypes = clsApplicationsTypes.GetAllAppsTypes();
            dgvApssTypes.DataSource = dtAppsTypes;

            if (dtAppsTypes.Rows.Count > 0 )
            {
                dgvApssTypes.Columns[0].HeaderText = "ID";
                dgvApssTypes.Columns[0].Width = 160;

                dgvApssTypes.Columns[1].HeaderText = "Title";
                dgvApssTypes.Columns[1].Width = 390;

                dgvApssTypes.Columns[2].HeaderText = "Fees";
                dgvApssTypes.Columns[2].Width = 160;
            }

            _RefreshAppsTypesTable();
        }

        private void dgvApssTypes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvApssTypes.ClearSelection();
                dgvApssTypes.Rows[e.RowIndex].Selected = true;
                dgvApssTypes.CurrentCell = dgvApssTypes.Rows[e.RowIndex ].Cells[0]; 
            }
        }

        private void editeApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmValidationTest FrmValidationTest = new frmValidationTest();
            FrmValidationTest.ShowDialog();
            if (FrmValidationTest.Validation)
            {
                int appTypeID = (int)dgvApssTypes.CurrentRow.Cells["ApplicationTypeID"].Value;
                frmUpdateAppType frm = new frmUpdateAppType(appTypeID);
                frm.UpdateAppType += _RefreshAppsTypesTable;
                frm.ShowDialog();

            }
        }
    }
}
