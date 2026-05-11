using DVLD.Global_clases;
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

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmIssueLicenseFirstTime : Form
    {
        int _LocalDrivingLicenseAppilcationID;
        clsLocalDrivingLicenseApplication _LocalDLApplication;
        public frmIssueLicenseFirstTime(int localDrivingLicenseApplication)
        {
            InitializeComponent();
            _LocalDrivingLicenseAppilcationID = localDrivingLicenseApplication;
        }

        private void frmIssueLicenseFirstTime_Load(object sender, EventArgs e)
        {
            _LocalDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID
                (_LocalDrivingLicenseAppilcationID);

            if (_LocalDLApplication == null )
            {
                MessageBox.Show("Sorry No app Founded");
                this    .Close();
                return;
            }


            if (!_HandleIfThereAnActiveLicense())
            {
                this.Close();            
                return;
            }
               

            if (!_HandleIfPersonPassAllTestTypes())
            {
                this.Close();

                return;
            }



            ctrlLocalApplicationInfo1.LoadcontrolInfo(_LocalDrivingLicenseAppilcationID);
        }

        private bool _HandleIfThereAnActiveLicense()
        {
            int LicenseID = _LocalDLApplication.GetActiveLicenseByPersonIDAndLicenseClass();
            if (LicenseID != -1)
            {
                MessageBox.Show($"This Person Already Have Active License From" +
                    $" {_LocalDLApplication.LicenseClassInfo.ClassName} Type", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false    ;
            }
            return true ;
        }

        private bool _HandleIfPersonPassAllTestTypes()
        {
            if (_LocalDLApplication.GetPassedTestCount() < 3)
            {
                MessageBox.Show($"This Person Doesn't Passed All Tests.", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

            int theNewLicenseID = _LocalDLApplication.IssueLicenseForFirstTime(lblNotes.Text.Trim(),
                clsGlobal.CurrentUser.UserID);


            if (theNewLicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + theNewLicenseID.ToString(),
                   "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
       "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





        }
    }
}
