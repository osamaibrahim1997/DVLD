using DVLD.Licenses.International;
using DVLD.Licenses.Local_Licenses;
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

namespace DVLD.Licenses.controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        int _DriverID;
        clsDriver _Driver;
        DataTable _dtLocalLicense;
        DataTable _dtInternationaLicense;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicenseInofs()
        {
            _dtLocalLicense = _Driver.GetDriverLocalLicensesData();
            dgvLocal.DataSource = _dtLocalLicense;
            lblLocalCount.Text  = _dtLocalLicense.Rows.Count.ToString();


            if (_dtLocalLicense.Rows.Count > 0)
            {
                dgvLocal.Columns[0].HeaderText = "Lic.ID";
                dgvLocal.Columns[0].Width = 110;

                dgvLocal.Columns[1].HeaderText = "App.ID";
                dgvLocal.Columns[1].Width = 110;


                dgvLocal.Columns[2].HeaderText = "Driver ID";
                dgvLocal.Columns[2].Width = 110;

                dgvLocal.Columns[3].HeaderText = "Class Name";
                dgvLocal.Columns[3].Width = 270;


                dgvLocal.Columns[4].HeaderText = "Issue Date";
                dgvLocal.Columns[4].Width = 170;

                dgvLocal.Columns[5].HeaderText = "Expiration Date";
                dgvLocal.Columns[5].Width = 270;

                dgvLocal.Columns[6].HeaderText = "Notes";
                dgvLocal.Columns[6].Width = 170;

                dgvLocal.Columns[7].HeaderText = "Paid Fees";
                dgvLocal.Columns[7].Width = 110;

                dgvLocal.Columns[8].HeaderText = "Is Active";
                dgvLocal.Columns[8].Width = 110;

                dgvLocal.Columns[9].HeaderText = "Issue Reason";
                dgvLocal.Columns[9].Width = 110;

                dgvLocal.Columns[10].HeaderText = "Created By User ID";
                dgvLocal.Columns[10].Width = 110;




            }
        }
        private void _LoadInternationalLicenses()
        {
            _dtInternationaLicense = _Driver.GetDriverInternationallLicensesData();
            dgvInternationalLicensesHistory.DataSource = _dtInternationaLicense;
            lblLocalinternationalRecords.Text = _dtInternationaLicense.Rows.Count.ToString();

            if (_dtInternationaLicense.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 130;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 130;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 180;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 180;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 120;

            }

        }

        public void LoadLicensesInfosForThisPerson(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);
            if (_Driver == null) return;

            _LoadLocalLicenseInofs();
            _LoadInternationalLicenses();
        }

        private void showLicenseInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            int LicenseID = (int)dgvLocal.CurrentRow.Cells[0].Value;
            frmShowLocalLicense frm = new frmShowLocalLicense(LicenseID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            int InternationalLicenseID = (int)dgvLocal.CurrentRow.Cells[0].Value;
            frmShowInternaionalLicenseInfos frm = new frmShowInternaionalLicenseInfos(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void dgvLocal_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvLocal.ClearSelection();
                dgvLocal.Rows[e.RowIndex].Selected = true;
                dgvLocal.CurrentCell = dgvLocal.Rows[e.RowIndex].Cells[0];
            }
        }

        private void dgvInternationalLicensesHistory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvInternationalLicensesHistory.ClearSelection();
                dgvInternationalLicensesHistory.Rows[e.RowIndex].Selected = true;
                dgvInternationalLicensesHistory.CurrentCell = dgvInternationalLicensesHistory.Rows[e.RowIndex].Cells[0];
            }
        }
    }
}
