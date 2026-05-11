using DVLD.Licenses;
using DVLD.Licenses.Detain_License;
using DVLD.Licenses.Local_Licenses;
using DVLD.People;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Release_Detained_License
{
    public partial class frmListDetainedLicenses : Form
    {
        DataTable _dtDetainedLicenses;





        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;
            lblTotalRecords.Text = _dtDetainedLicenses.Rows.Count.ToString();

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 150;

            }
        }






        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "Is Released" && cbFilterBy.Text != "None") ? true : false;
            cbIsReleased.Visible = (cbFilterBy.Text == "Is Released") ? true : false;

        }
      
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterValue = "";

            switch (cbFilterBy.Text)
            {
                case "None":
                    FilterValue = "None";
                    break;
                case "Detain ID":
                    FilterValue = "DetainID";
                    break;
                case "Is Released":
                    FilterValue = "IsReleased";
                    break;
                case "National No.":
                    FilterValue = "NationalNo";
                    break;
                case "Full Name":
                    FilterValue = "FullName";
                    break;
                case "Release Application ID":
                    FilterValue = "ReleaseApplicationID";
                    break;
                default:
                    break;
            }

            if (FilterValue == "None" || cbFilterBy.Text == "")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblTotalRecords.Text = _dtDetainedLicenses.Rows.Count.ToString();
                return;
            }

            if (FilterValue == "DetainID" || FilterValue == "ReleaseApplicationID")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterValue, txtFilterValue, Text.Trim());
            }
            else
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("{0} = {1}", FilterValue, txtFilterValue.Text.Trim());
            }
            lblTotalRecords.Text = _dtDetainedLicenses.Rows.Count.ToString();


        }

        private void dgvDetainedLicenses_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvDetainedLicenses.ClearSelection();
                dgvDetainedLicenses.Rows[e.RowIndex].Selected = true;
                dgvDetainedLicenses.CurrentCell = dgvDetainedLicenses.Rows[e.RowIndex].Cells[0];
            }
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonId = clsPerson.Find((string)dgvDetainedLicenses.CurrentRow.Cells[6].Value).PersonID;
            if (PersonId == 0)
            {
                return;
            }
            frmShowPersonDetails frmShowPersonDetails = new frmShowPersonDetails(PersonId);
            frmShowPersonDetails.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicensID =(int) dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            if (LicensID == 0)
            {
                return;
            }
            frmShowLocalLicense frmShowLocalLicense = new frmShowLocalLicense(LicensID);
            frmShowLocalLicense.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonId = clsPerson.Find((string)dgvDetainedLicenses.CurrentRow.Cells[6].Value).PersonID;
            if (PersonId == 0)
            {
                return;
            }
            frmShowLicensesHestory frm = new frmShowLicensesHestory(PersonId);
            frm.ShowDialog();

        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicensID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int DetanID = (int)dgvDetainedLicenses.CurrentRow.Cells[0].Value;
            if (LicensID == 0 || DetanID == 0)            
                return;
            
            frmRealeseDetainedLicen_se frm = new frmRealeseDetainedLicen_se(LicensID, DetanID);
            frm.ShowDialog();
        }
    }
}
