using DVLD.Applications;
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

namespace DVLD.Tests.Test_Types
{
    public partial class frmTestTyps : Form
    {

        DataTable dtTestTypesList;




        public frmTestTyps()
        {
            InitializeComponent();
        }

        private void _RefreshTestTypesList()
        {
            dtTestTypesList = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = dtTestTypesList;
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();
        }

        private void frmTestTyps_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();

            if (dtTestTypesList.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 100;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 150;

                dgvTestTypes.Columns[2].HeaderText = "Discription";
                dgvTestTypes.Columns[2].Width = 400;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 100;
                    
            }
        }

        private void dgvTestTypes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvTestTypes.ClearSelection();
               dgvTestTypes.Rows[e.RowIndex].Selected = true;
               dgvTestTypes.CurrentCell = dgvTestTypes.Rows[e.RowIndex].Cells[0];
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmValidationTest FrmValidationTest = new frmValidationTest();
            FrmValidationTest.ShowDialog();
            if (FrmValidationTest.Validation)
            {
                int testTypeID = (int)dgvTestTypes.CurrentRow.Cells["TestTypeID"].Value;
                frmUpdateTestType frm = new frmUpdateTestType(testTypeID);
                //frm.UpdateAppType += _RefreshAppsTypesTable;
                frm.ShowDialog();

            }
            
        }
    }
}
