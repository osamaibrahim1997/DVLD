using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD.Tests.Test_Types;
using DVLD.Tests.Tests_Applointments;
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
using static DVLD_Business.clsTestType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.Applications.Local_Driving_Licens
{
    public partial class frmLocalDrivingLicensesApplicationsList : Form
    {      
      
        DataTable _dtAllLocalDrivingLicenseApplications;

        public frmLocalDrivingLicensesApplicationsList()
        {
            InitializeComponent();
        }
        private void dgvLocalDrivingLicenseApplications_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvLocalDrivingLicenseApplications.ClearSelection();
                dgvLocalDrivingLicenseApplications.Rows[e.RowIndex].Selected = true;
                dgvLocalDrivingLicenseApplications.CurrentCell = dgvLocalDrivingLicenseApplications.Rows[e.RowIndex].Cells[0];
            }
        }

        private void frmLocalDrivingLicensesApp_icationsList_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicensesApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;
            lblRecordsCount.Text = _dtAllLocalDrivingLicenseApplications.Rows.Count.ToString();
            if ( _dtAllLocalDrivingLicenseApplications.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 80;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 330;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 100;
            }

            cbFilterBy.SelectedIndex = 0;
          
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            int LocalDrivingLicenseAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmLocalDrivingLicenseApplicationDetails frmLocalDrivingLicenseApplicationDetails =
                new frmLocalDrivingLicenseApplicationDetails(LocalDrivingLicenseAppID);
            frmLocalDrivingLicenseApplicationDetails.ShowDialog();
            frmLocalDrivingLicensesApp_icationsList_Load(null, null);

        }
         
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterKeyword = "";

            switch (cbFilterBy.Text)
            {
               
                case "L.D.L.AppID":
                    filterKeyword = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    filterKeyword = "NationalNo";
                    break;

                case "Full Name":
                    filterKeyword = "FullName";
                    break;

                case "Status":
                    filterKeyword = "Status";
                    break;

                default :
                    filterKeyword = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim()  == "" || txtFilterValue.Text == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtAllLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            if (filterKeyword == "LocalDrivingLicenseApplicationID")
            
    _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterKeyword, txtFilterValue.Text.Trim());

            else
            {

                 
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterKeyword, txtFilterValue.Text.Trim());
            }

                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            
           
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Status");
            cbStatues.Visible = (cbFilterBy.Text == "Status");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();


            }
            if (cbFilterBy.SelectedIndex == 0)
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();

            }
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
            frmLocalDrivingLicensesApp_icationsList_Load(null, null);
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                int LocalApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
                int ApplicationID = clsApplication.GetApplicationIDByLocalDrivingApplication(LocalApplicationID);
                if (ApplicationID != -1)
                {
                    if (clsApplication.CancelAnApplication(ApplicationID))
                    {
                        MessageBox.Show($"Application with ID {ApplicationID} Canceled!");
                    }

                    _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicensesApplications();
                    dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;

                }
                else
                {
                    MessageBox.Show($"No Active Application For This Local Driving License App");
                }
            }

        }

      
        private void _SechedualTest(clsTestType.enTestType TestType)
        {
            
            int LocalDrivingLicenseAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmTestAppointment frmtestAppointment = new frmTestAppointment(LocalDrivingLicenseAppID, TestType);
            frmtestAppointment.ShowDialog();
            frmLocalDrivingLicensesApp_icationsList_Load(null, null);
        }



        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SechedualTest(clsTestType.enTestType.VisionTest);
        }
       

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SechedualTest(clsTestType.enTestType.WrittenTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SechedualTest(clsTestType.enTestType.StreetTest);
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "Delete Application", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) != DialogResult.OK)
            {   
                return;
            }

            int LocalDrivingLicenseAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            if (clsLocalDrivingLicenseApplication.DoesHasAppointments(LocalDrivingLicenseAppID))
            {
                MessageBox.Show("It cannot be deleted. There is information associated with this application.");
                return ;
            }
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseAppID);

            if (localDrivingLicenseApplication.Delete())
            {
                MessageBox.Show("Done?", "Deleted Application", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Information);
            frmLocalDrivingLicensesApp_icationsList_Load(null, null);
            }
            else
            {
                MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ScheduleTestsMenue_Click(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmAddUpdateLocalDrivingLicenseApplication frmAddUpdateApplication = 
                new frmAddUpdateLocalDrivingLicenseApplication(LocalDrivingLicenseAppID);
            frmAddUpdateApplication.ShowDialog();

            frmLocalDrivingLicensesApp_icationsList_Load(null, null);

        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication _LocalDLApplication = clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseAppID);

            int LicenseID = _LocalDLApplication.GetActiveLicenseByPersonIDAndLicenseClass();

            if (LicenseID != -1)
            {   
                frmShowLocalLicense frm = new frmShowLocalLicense(LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry, The client Doesn't Have A License.", "!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmIssueLicenseFirstTime frmIssue = new frmIssueLicenseFirstTime(LocalDrivingLicenseAppID);
            frmIssue.ShowDialog();
            frmLocalDrivingLicensesApp_icationsList_Load(null, null);

        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;





            clsLocalDrivingLicenseApplication Local = clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseAppID);

            int LicenseID = Local.GetLicenseIDForThisPerson();

            bool IsLicenseExists = LicenseID != -1;
            int totalTestsPassed = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (totalTestsPassed == 3 && !IsLicenseExists);

            editToolStripMenuItem.Enabled = (Local.ApplicationStatues == clsApplication.enApplicationStatue.New && !IsLicenseExists);

            showLicenseToolStripMenuItem.Enabled = IsLicenseExists;

            DeleteApplicationToolStripMenuItem.Enabled = Local.ApplicationStatues == clsApplication.enApplicationStatue.New;

            CancelApplicaitonToolStripMenuItem.Enabled = Local.ApplicationStatues == clsApplication.enApplicationStatue.New;

            ScheduleTestsMenue.Enabled = IsLicenseExists == false;

            bool DoesPassVisionTest = Local.DoesPersonPassThisTest(enTestType.VisionTest);
            bool DoesPassWrittenTest = Local.DoesPersonPassThisTest(enTestType.WrittenTest);
            bool DoesPassStreetTest = Local.DoesPersonPassThisTest(enTestType.StreetTest);

            ScheduleTestsMenue.Enabled = (!DoesPassVisionTest || !DoesPassWrittenTest || !DoesPassStreetTest) &&
                Local.ApplicationStatues == clsApplication.enApplicationStatue.New;

            

            if (ScheduleTestsMenue.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !DoesPassVisionTest;
                scheduleWrittenTestToolStripMenuItem.Enabled = !DoesPassWrittenTest && DoesPassVisionTest;
                scheduleStreetTestToolStripMenuItem.Enabled = !DoesPassStreetTest && DoesPassVisionTest && DoesPassWrittenTest;

            }

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string National = (string)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value;

            int PersonId = clsPerson.GetPersonIdByNationalNo(National);

            frmShowLicensesHestory frm = new frmShowLicensesHestory(PersonId);
            frm.ShowDialog();


            frmLocalDrivingLicensesApp_icationsList_Load(null, null);

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

       
    }
}
