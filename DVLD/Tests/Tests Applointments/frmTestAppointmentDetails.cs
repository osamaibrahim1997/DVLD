using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD.Properties;
using System.Windows.Forms;

namespace DVLD.Tests.Tests_Applointments
{
    public partial class frmTestAppointment : Form
    {
       private  DataTable _AppointmentsTable;
       private  clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplcation;
       private  int _LocalDrivingLicenseApplcationID;

         clsTestType.enTestType _enTestType;

        public frmTestAppointment( int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplcationID = LocalDrivingLicenseApplicationID;
            
            _enTestType = TestType;

        }

        private void _RestTitleAndImage()
        {
            switch (_enTestType)
            {
                case clsTestType.enTestType.VisionTest:
                    pbTestTypeImage.Image = Resources.Vision_512;
                    this.Text = lblTitle.Text;
                    lblTitle.Text = "Vision Test Appointments";
                    break;
                case clsTestType.enTestType.WrittenTest:
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    this.Text = lblTitle.Text;
                    lblTitle.Text = "Written Test Appointments";
                    break;
                case clsTestType.enTestType.StreetTest:
                    pbTestTypeImage.Image = Resources.driving_test_512;
                    this.Text = lblTitle.Text;
                    lblTitle.Text = "Street Test Appointments";
                    break;
                default:
                    break;
            }
        }

        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            _RestTitleAndImage();


            ctrlLocalApplicationInfo1.LoadcontrolInfo(_LocalDrivingLicenseApplcationID);

            _AppointmentsTable = clsTestAppointment.
            GetAllAppointmentsGroupByTestTypeForThisApplication(_LocalDrivingLicenseApplcationID, _enTestType);
                    
            dgvLicenseTestAppointments.DataSource = _AppointmentsTable;

            lblRecordsCount.Text = _AppointmentsTable.Rows.Count.ToString();

            if (_AppointmentsTable.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;

            }

        }


        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplcation = clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplcationID);

            if (_LocalDrivingLicenseApplcation.CheckIfIsThereAnActiveScheduledTest((int)_enTestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment",
                                   "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = _LocalDrivingLicenseApplcation.GetLastTestByTestType(_enTestType);

            if (LastTest == null)
            {
                frmSchedualTest frmSchedual = new frmSchedualTest(_LocalDrivingLicenseApplcationID , _enTestType);
                frmSchedual.ShowDialog();
                frmTestAppointment_Load(null, null);
                return;
            }

            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmSchedualTest frmSchedual1 = new frmSchedualTest(LastTest.AppointmentInfo.LocalDrivingLicenseApplicationID,
                _enTestType);
            frmSchedual1.ShowDialog();
            frmTestAppointment_Load(null, null);

        }


        private void ctrDrivingLicenseApplication1_Load(object sender, EventArgs e)
        {


        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_AppointmentsTable.Rows.Count <= 0)
            {
                MessageBox.Show("No Row For Updating"); return;
            }
            int testAppointment = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;
            frmSchedualTest frmSchedual1 = new frmSchedualTest(_LocalDrivingLicenseApplcationID,
                _enTestType, testAppointment);
            frmSchedual1.ShowDialog();
            frmTestAppointment_Load(null, null);

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_AppointmentsTable.Rows.Count <= 0)
            {
                MessageBox.Show("No Row For Taking Test"); return;
            }
            int tesAppointment = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;
            frmTakeTest frmTakeTest = new frmTakeTest(tesAppointment, _enTestType);
            frmTakeTest.ShowDialog();
            frmTestAppointment_Load(null, null);

        }
    }
}
