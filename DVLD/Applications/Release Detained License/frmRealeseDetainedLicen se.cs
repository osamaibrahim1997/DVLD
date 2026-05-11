using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace DVLD.Applications.Release_Detained_License
{
    public partial class frmRealeseDetainedLicen_se : Form
    {
        int _licensID;
        clsLicense _License;

        int _DetainID;
        clsDetainedLicense _DetainedLicense;
        public frmRealeseDetainedLicen_se()
        {
            InitializeComponent();
        }
        public frmRealeseDetainedLicen_se(int LicenseID, int DetainID)
        {
            _licensID = LicenseID;
            _DetainID = DetainID;
            
            InitializeComponent();

            ctrlDriverLicenseInfoWithFilter1.SelectLicenseFromOutside(LicenseID);
        }
        private void LoadDetainInfo()
        {
            _DetainedLicense = clsDetainedLicense.FindByLicenseID(_licensID);

            if (_DetainedLicense == null)
                return;

            lblDetainID .Text= _DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToString("g");
            lblApplicationFees.Text = clsApplicationsTypes.
                Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense)._AppTypeFees.ToString();
            lblLicenseID.Text = _licensID.ToString();
            lblCreatedByUser.Text = clsUser.Find(_License.CreatedByUserID).UserName;
            lblFineFees.Text = _DetainedLicense.FineFees.ToString();
            lblTotalFees.Text = float.TryParse(lblApplicationFees.Text, out float F) ?
                (F + _DetainedLicense.FineFees).ToString() : "" ;

        }
        private void ResetDetainInfos()
        {
            lblDetainID.Text = "[???]";
            lblDetainDate.Text = "[???]";
            lblApplicationFees.Text = "[???]";
            lblCreatedByUser.Text = "[???]";
            lblFineFees.Text = "[???]";
            lblTotalFees.Text = "[???]";
            lblApplicationID.Text = "[???]";
            lblLicenseID.Text = "[???]";
        }
        private bool CheckIfValid()
        {
            if (_License == null) return false  ;

                if (!_License.IsActive)
            { MessageBox.Show("Sorry, This License Not Active Yet."); return false; }


            if (_License.DetainedInfo != null && _License.DetainedInfo.IsReleased)
            { MessageBox.Show("Sorry, This License Not Detained Yet."); return false; }

            return true;
        }
        
        private void frmRealeseDetainedLicen_se_Load(object sender, EventArgs e)
        {
            _License = clsLicense.Find(_licensID);

            ResetDetainInfos();

            if (!CheckIfValid())           
                return;

            LoadDetainInfo();


            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;
            btnRelease.Enabled = true;

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLocalLicense frm = new frmShowLocalLicense(_licensID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHestory frm = new frmShowLicensesHestory(_License.DriverInfo.PersonID);
            frm.ShowDialog();   
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseIdSelect(int obj)
        {
            _licensID = obj;
            _License = clsLicense.Find(_licensID);

            if (_License == null)
            { MessageBox.Show("Sorry, This License Wasn't Found"); return ; }

            ResetDetainInfos();

            if (!CheckIfValid())
                return;

            LoadDetainInfo();

            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;
            btnRelease.Enabled = true;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "!", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) != DialogResult.OK)
                return;


            if (_DetainedLicense.ReleaseDetainedLicense())
            {
                lblApplicationID.Text = _DetainedLicense.ReleaseApplicationID.ToString();

                ctrlDriverLicenseInfoWithFilter1.FilterEnabling = false;
                btnRelease.Enabled = false;
            }
            else
            {
                MessageBox.Show("Sorry, This Operation Wasn't Done Successfully");
            }
        }
    }
}
