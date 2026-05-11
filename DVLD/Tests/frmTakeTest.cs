using DVLD.Global_clases;
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

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        int _TestAppointmentID = -1;
        clsTestType.enTestType _TestType;
        clsTestAppointment TestAppointment;
        clsTest _Test;
        public frmTakeTest(int TestAppointmentID, clsTestType.enTestType testType)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestType = testType;
        }

        private void _HandleLockedAppointment()
        {
            rbFail.Enabled = false;
            rbPass.Enabled = false;
            lblUserMessage.Visible = true;
            txtNotes.Enabled = false;
            btnSave.Enabled = false;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (TestAppointment.IsLocked)              
                _HandleLockedAppointment();

            ctrlSchedualedTestInfos1.TestTypeID = _TestType;
            ctrlSchedualedTestInfos1.LoadAppointmentInfos(_TestAppointmentID);

            if (_TestAppointmentID == -1)            
                btnSave .Enabled = false;
            else            
                btnSave .Enabled = true;
            

            int testid   = TestAppointment.GetTestIDUsingTestAppointmentID(_TestAppointmentID);

            if (testid != -1)
            {
                clsTest thisAppointmentTest = clsTest.Find(testid);

                if (thisAppointmentTest.TestResult)                
                    rbPass.Checked = true;                
                else                
                    rbFail.Checked = false;

                txtNotes.Text = thisAppointmentTest.Notes;
                _HandleLockedAppointment();

            }
            else
            {
                 _Test = new clsTest();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmValidationTest test = new frmValidationTest();
            test.ShowDialog();
            if (!test.Validation)
            {
                return;
            }

           

            _Test.Notes = txtNotes.Text;
            _Test.TestResult = (rbPass.Checked)? true : false;
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Test.TestAppointmentID = _TestAppointmentID;

            if (_Test.Save())
            {
                ctrlSchedualedTestInfos1.SetTestIdAfterAddingNewTest(_Test.TestID);
                
                MessageBox.Show("Data Saved Successfully");
                
            }
            else 
            { 
                MessageBox.Show("Error , Data Doesn't Saved Successfully");
            }
        }













        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
