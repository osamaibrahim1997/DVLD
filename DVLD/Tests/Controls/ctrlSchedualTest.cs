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
using DVLD.Properties;
using DVLD.Global_clases;

namespace DVLD.Tests.Controls
{
    public partial class ctrlSchedualTest : UserControl
    {
        public ctrlSchedualTest()
        {
            InitializeComponent();
        }


        public enum enMode { enAdd = 1 , enUpdate = 2};
        private enMode Mode = enMode.enAdd;

        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;



        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication ;
        private clsTestType.enTestType _testType;
        private int _TestAppointmentID = -1;
        private clsTestAppointment _TestAppointment;

        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
            set
            {
                _TestAppointmentID = value;

            }
        }

        public clsTestType.enTestType TestTypeID
        {
            get { return _testType; } 
            set 
            {
                _testType = value;
                switch (_testType)
                {
                    case clsTestType.enTestType.VisionTest:
                        pbTestTypeImage.Image = Resources.Vision_512;
                        lblTitle.Text = "Vision Test";
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        pbTestTypeImage.Image = Resources.Written_Test_32;
                        lblTitle.Text = "Written Test";
                        break;
                    case clsTestType.enTestType.StreetTest:
                        pbTestTypeImage.Image = Resources.Street_Test_32;
                        lblTitle.Text = "Street Test";
                        break;
                    default:
                        break;
                }

            }
        }
        
      
        public void LoadTestInfo(int LDLAID, int TestAppointmentID = -1 )
        {
            if (TestAppointmentID == -1)
                Mode = enMode.enAdd;            
            else
                Mode= enMode.enUpdate;

            _LocalDrivingLicenseApplicationID = LDLAID;
            _TestAppointmentID = TestAppointmentID;
            
           
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " +
                    _LocalDrivingLicenseApplicationID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return; 
            }

            if (_LocalDrivingLicenseApplication.DoseAttendTestType(_testType)) 
            {
                _CreationMode = enCreationMode.RetakeTestSchedule;
            }
            else
            {
                _CreationMode= enCreationMode.FirstTimeSchedule;
            }

            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeAppFees.Text = clsApplicationsTypes.
                    Find((int)clsApplication.enApplicationType.RetakeTest)._AppTypeFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedual Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                lblRetakeAppFees.Text = "0";
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedual Test";
                lblRetakeTestAppID.Text = "N/A";
            }
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblFullName .Text = _LocalDrivingLicenseApplication.ApplicantFullName;
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;

            lblTrial.Text = _LocalDrivingLicenseApplication.GetTotalTrialsTestsOnThisTestType(_testType).ToString();

            if (Mode == enMode.enAdd)   
            {
                _TestAppointment = new clsTestAppointment();
                lblFees.Text = clsTestType.Find(_testType).TestTypeFees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblFees.Text = clsTestType.Find(TestTypeID).TestTypeFees.ToString();
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                if (!_HandleAndLoadTestAppointmentInfo())
                {
                    return;
                }
            }

            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();


            if (!_HandleActiveAppointmentConstraint())
            {
                return;
            }

            if (!_HandleIfThisTestTypeIsLocked())
            {
                return; 
            }

            if (!_HandleIfUserPassedPreviousTest())
            {
                return;
            }

        }

        private bool _HandleIfUserPassedPreviousTest()
        {
            switch (TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblUserMessage.Enabled = false;
                    return true;

                case clsTestType.enTestType.WrittenTest:

                    if (!_LocalDrivingLicenseApplication.DoesPersonPassThisTest(clsTestType.enTestType.VisionTest)) 
                    {
                        lblUserMessage.Enabled = true;
                        lblUserMessage.Text = "User Should Pass Vision Test First";
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Enabled = false;
                        lblUserMessage.Text = "";
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                        return true;
                    }
                  
                case clsTestType.enTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplication.DoesPersonPassThisTest(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Enabled = true;
                        lblUserMessage.Text = "User Should Pass Written Test First";
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Enabled = false;
                        lblUserMessage.Text = "";
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                        return true;
                    }
                default:
                    break;
            }
            return true;
        }
        private bool _HandleIfThisTestTypeIsLocked()
        {

            if (_TestAppointment.IsLocked)  
            {
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "This Appointment Is Locked.";
                return false;
            }


            return true;
        }
        private bool _HandleActiveAppointmentConstraint()
        {
            if (Mode == enMode.enAdd && 
                _LocalDrivingLicenseApplication.CheckIfIsThereAnActiveScheduledTest((int)TestTypeID))
            {
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                return false;
            }
            return true;
        }
        private bool _HandleAndLoadTestAppointmentInfo()
        {
            //In case the mode is update we retrive the appointment data
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            lblTitle.Text = "Update Schedualed Test";
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            dtpTestDate.MinDate = DateTime.Now;
            
            if (DateTime.Now > _TestAppointment.AppointmentDate)
            {

                dtpTestDate.Value = DateTime.Now;
            }
            else
            {
                dtpTestDate.Value = _TestAppointment.AppointmentDate;
            }

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                    
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {

                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.ApplicationPaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
            }

            return true;
        }


        private void ctrlSchedualTest_Load(object sender, EventArgs e)
        {
            
        }
        private bool _HandleAddingRetakeApplication()
        {
            if (Mode== enMode.enAdd && _CreationMode == enCreationMode.RetakeTestSchedule)
            {   
                clsApplication NewRetakeApp = new clsApplication();

                NewRetakeApp.ApplicationCreatedByUserID = clsGlobal.CurrentUser.UserID;
                NewRetakeApp.ApplicationDate = DateTime.Now;
                NewRetakeApp.ApplicationPaidFees = clsApplicationsTypes.
                    Find((int)clsApplication.enApplicationType.RetakeTest)._AppTypeFees;
                NewRetakeApp.ApplicationStatues = clsApplication.enApplicationStatue.Completed;
                NewRetakeApp.ApplicationPersonID = _LocalDrivingLicenseApplication.ApplicationPersonID;
                NewRetakeApp.ApplicationLastStatueDate = DateTime.Now;
                NewRetakeApp.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;

                if (!NewRetakeApp.Save())
                {
                    _TestAppointment.RetakeTestApplicationID =  -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                _TestAppointment.RetakeTestApplicationID = NewRetakeApp.ApplicationID;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleAddingRetakeApplication())
            {
                return;
            }
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.TestTypeID = TestTypeID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                Mode = enMode.enUpdate;
                MessageBox.Show("Data Saved Successfully.", "Saved", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
