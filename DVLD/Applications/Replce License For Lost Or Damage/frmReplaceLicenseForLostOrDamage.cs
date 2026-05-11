using DVLD.Global_clases;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
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

namespace DVLD.Applications.Replce_License_For_Lost_Or_Damage
{
    public partial class frmReplaceLicenseForLostOrDamage : Form
    {
        int _licenseID;
        clsLicense _oldLicense;
        int _ReplacedLicenseID;

        public frmReplaceLicenseForLostOrDamage()
        {
            InitializeComponent();
            this.AcceptButton = ctrlDriverLicenseInfoWithFilter1.BtnSearch;
        }

        private void FillReplaceOrDamagedAppInfos()
        {
            if ( rbDamagedLicense.Checked)
            {
                lblFeesApplication.Text = clsApplicationsTypes.
                    Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense)._AppTypeFees.ToString();
            }

            if (rbLostLicens.Checked)
            {

                lblFeesApplication.Text = clsApplicationsTypes.
                    Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense)._AppTypeFees.ToString();
            }

        }
        private void ResetEveryThing()
        {
            lblOldLicenseID.Text = _oldLicense.LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;

            btnReplaceLicense.Enabled = false;
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseIdSelect(int obj)
        {
            _licenseID = obj;
            _oldLicense = clsLicense.Find(_licenseID);
            if (!_oldLicense.IsActive)
            {
                MessageBox.Show("Selected License Isn't Active.");
                ResetEveryThing();
                return;
            }

            FillReplaceOrDamagedAppInfos();

            lblOldLicenseID.Text = _oldLicense.LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;

            btnReplaceLicense.Enabled = true;
        }

        private void frmReplaceLicenseForLostOrDamage_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = DateTime.Now.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            FillReplaceOrDamagedAppInfos();
        }

        private void rbLostLicens_CheckedChanged(object sender, EventArgs e)
        {
            FillReplaceOrDamagedAppInfos();
        }

        private void btnReplaceLicense_Click(object sender, EventArgs e)
        {

            bool DamagedOrLost = (rbDamagedLicense.Checked);

            clsLicense ReplacedLicense = _oldLicense.ReplaceLicenseForDamageOrLost(DamagedOrLost, clsGlobal.CurrentUser.UserID);

            if (ReplacedLicense == null)
            {
                MessageBox.Show("License Doesn't Replaced.");
                return;
            }

            _ReplacedLicenseID= ReplacedLicense.LicenseID;
            lblApplicationID.Text = ReplacedLicense.ApplicationID.ToString();
            lblRenewedLicenseID.Text = ReplacedLicense.LicenseID.ToString();



            ctrlDriverLicenseInfoWithFilter1.FilterEnabling = false;
            gbReplaceFor.Enabled = false;
            btnReplaceLicense.Enabled = false;
            llShowNewLicenseInfo.Enabled = true;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLocalLicense frmlocalLicense = new frmShowLocalLicense(_ReplacedLicenseID);
            frmlocalLicense.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHestory frmShowLicensesHestory = new frmShowLicensesHestory(_oldLicense.DriverInfo.PersonID);
            frmShowLicensesHestory.ShowDialog();
        }
    }
}
