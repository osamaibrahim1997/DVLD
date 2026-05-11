using DVLD.Properties;
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

namespace DVLD.Tests.Controls
{
    public partial class ctrlSchedualedTestInfos : UserControl
    {
        private int _TestAppointmentID;
        private clsTestAppointment _TestAppointment;
        private clsTestType.enTestType _enTestType;
        private int _TestID;
        public int TestId
        {
            get { return _TestID; }
        }

        public clsTestType.enTestType TestTypeID
        {
            get { return _enTestType; }
            set 
            { 
                _enTestType = value;

                switch (_enTestType)
                {
                    case clsTestType.enTestType.VisionTest:
                        pbTestTypeImage.Image = Resources.Vision_512;
                        lblTitle.Text = "Vision Test";
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        lblTitle.Text = "Written Test";
                        break;
                    case clsTestType.enTestType.StreetTest:
                        pbTestTypeImage.Image = Resources.Schedule_Test_512;
                        lblTitle.Text = "Schedule Test";
                        break;
                    default:
                        break;
                }
            }
        }
        public ctrlSchedualedTestInfos()
        {
            InitializeComponent();
        }

        public void LoadAppointmentInfos(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("No test Appointment With ID :" + TestAppointmentID);
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblFullName.Text = _TestAppointment.ApplicationInfos.ApplicantFullName;
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToString();
            lblDrivingClass.Text = _TestAppointment.ApplicationInfos.LicenseClassInfo.ClassName;
            lblTrial.Text = _TestAppointment.ApplicationInfos.GetTotalTrialsTestsOnThisTestType(TestTypeID)
                .ToString();
            lblTestID.Text = ((_TestAppointment.TestID) != -1) ? _TestAppointment.TestID.ToString()
                : "Not Taken Yet";

        }

        public void SetTestIdAfterAddingNewTest(int TestId)
        {
            _TestID = TestId;
            lblTestID.Text = TestId.ToString();
        }







    }
}
