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
    public partial class ctrlLocalApplicationInfo : UserControl
    {
        clsLocalDrivingLicenseApplication _LocalDrivingLicenceApplication;
        int _LicenseID;
        public ctrlLocalApplicationInfo()
        {
            InitializeComponent();
        }

        private void _ResetThisCtlrInfo()
        {
            ctrApplicationBasicInfo1.ResetApplicationInfo();
            lblLocalAppID.Text = "???";
            lblPassedtests.Text = "???";
            lblAppliedForLicense.Text = "???";
            lblPassedtests.Visible = false;
        }

        public void LoadcontrolInfo(int LocalDrivingLiceseApplicationID)
        {
            _LocalDrivingLicenceApplication = clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(LocalDrivingLiceseApplicationID);

            if (_LocalDrivingLicenceApplication == null)
            {
                _ResetThisCtlrInfo();
                MessageBox.Show("Sorry No app Founded");
                return;                
            }

            _FillAppInfosInTheControl();
        }

        private void _FillAppInfosInTheControl()
        {
            lblLocalAppID.Text = _LocalDrivingLicenceApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedForLicense.Text = _LocalDrivingLicenceApplication.LicenseClassInfo.ClassName;
            lblPassedtests.Text = _LocalDrivingLicenceApplication.GetPassedTestCount().ToString() + "/3";


            _LicenseID = _LocalDrivingLicenceApplication.GetActiveLicenseByPersonIDAndLicenseClass();
            linkLableShowLicenseInfo.Enabled = _LicenseID != 0;
            ctrApplicationBasicInfo1.
                LoadApplicationInfo(_LocalDrivingLicenceApplication.ApplicationID);
        }


        private void linkLableShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }






        private void ctrlLocalApplicationInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
