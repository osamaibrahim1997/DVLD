using DVLD.Licenses;
using DVLD.Licenses.International;
using DVLD.People;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.International
{
    public partial class frmIntrnationalAppsManage : Form
    {
        private DataTable _InternationalAppsList;


        public frmIntrnationalAppsManage()
        {
            InitializeComponent();
        }

        private void frmIntrnationalAppsManage_Load(object sender, EventArgs e)
        {
            _InternationalAppsList = clsInternationalLicenses.GetAllInternationalLicenses();
            dgvInternationalLicenses.DataSource = _InternationalAppsList;
            lblInternationalLicensesRecords.Text = _InternationalAppsList.Rows.Count.ToString();

            if (_InternationalAppsList.Rows.Count > 0 ) 
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 160;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 150;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 130;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 130;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 180;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 180;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 120;

            }
        }

        private void dgvInternationalLicenses_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
             dgvInternationalLicenses   .ClearSelection();
                dgvInternationalLicenses.Rows[e.RowIndex].Selected = true;
                dgvInternationalLicenses.CurrentCell = dgvInternationalLicenses.Rows[e.RowIndex].Cells[0];
            }
        }

           private void btnNewApplication_Click(object sender, EventArgs e)
        {
            
            frmNewInternationalLicense frm = new frmNewInternationalLicense();
            frm.ShowDialog();
            frmIntrnationalAppsManage_Load(null,null);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {   
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.SelectedIndex = 0;
                cbIsReleased.Focus();
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else
                {
                    txtFilterValue.Enabled = true;

                }
                txtFilterValue.Focus();
                txtFilterValue.Text = "";

            }



          
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "Application ID":                    
                        FilterColumn = "ApplicationID";
                        break;                    

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            if (txtFilterValue.Text.Trim() == "" || cbFilterBy.Text == "None")  
            {
                _InternationalAppsList.DefaultView.RowFilter = txtFilterValue.Text;
                lblInternationalLicensesRecords.Text = _InternationalAppsList.Rows.Count.ToString();
                return;
            }

            _InternationalAppsList.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            lblInternationalLicensesRecords.Text = _InternationalAppsList.Rows.Count.ToString();

        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.Find(DriverID).PersonID;
            frmShowPersonDetails frmShowPerson = new frmShowPersonDetails(PersonID);
            frmShowPerson.ShowDialog();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;
            frmShowInternaionalLicenseInfos frm = new frmShowInternaionalLicenseInfos(InternationalLicenseID);
            frm.ShowDialog();


        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.Find(DriverID).PersonID;
            frmShowLicensesHestory frm = new frmShowLicensesHestory(PersonID);
            frm.ShowDialog();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
