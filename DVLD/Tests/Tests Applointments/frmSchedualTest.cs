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

namespace DVLD.Tests.Tests_Applointments
{
    public partial class frmSchedualTest : Form
    {
        private int _LocalDrivingLicenseAppID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private int _AppointmentID = -1;

        public frmSchedualTest(int LDLAID , clsTestType.enTestType testType, int AppointmentID = -1)
        {
            InitializeComponent();
            _TestTypeID =  testType;
            _LocalDrivingLicenseAppID = LDLAID;
            _AppointmentID = AppointmentID;
        }

        private void frmSchedualTest_Load(object sender, EventArgs e)
        {
            ctrlSchedualTest1.TestTypeID = _TestTypeID;
            ctrlSchedualTest1.LoadTestInfo(_LocalDrivingLicenseAppID, _AppointmentID );
        }
    }
}
