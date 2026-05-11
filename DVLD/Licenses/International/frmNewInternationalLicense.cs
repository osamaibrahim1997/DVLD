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

namespace DVLD.Licenses.International
{
    public partial class frmNewInternationalLicense : Form
    {
        int _LocalLicenseID;
        clsLicense license;
        int _internationalLicensID = -1;

        public frmNewInternationalLicense()
        {
            InitializeComponent();

            AcceptButton = ctrlDriverLicenseInfoWithFilter1.BtnSearch;
            CancelButton = btnClose;
        }
       
        private void frmNewInternationalLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString("F");
           
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString();
            
            lblIssueDate.Text = DateTime.Now.ToString();
            lblFees.Text = clsApplicationsTypes.
                Find((int)clsApplication.enApplicationType.NewInternationalLicense)._AppTypeFees.ToString();
            

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseIdSelect(int obj)
        {

            int SelectedLicenseID = obj;


            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);
            if (SelectedLicenseID == -1)
            {
                return;
            }

            license = clsLicense.Find(obj);
            if (!license.IsActive || license.LicenseClassInfo.LicenseClassID != 3)
            {
                MessageBox.Show("Sorry, This License With ID " + obj + " ins't Active," +
                    " Or it's not Odrinary License.Yo Shoult Choose Ordinary License.", "Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _internationalLicensID = license.GetActivInternationalID();

            if (_internationalLicensID != -1)
            {
                MessageBox.Show("Sorry, This Person Already Have Active Internaional Licens.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;

                return;
            }

            _LocalLicenseID = obj;

            if (_LocalLicenseID != -1)
            {               
               
                lblLocalLicenseID.Text = _LocalLicenseID.ToString();
              
                btnIssueLicense.Enabled = true;
                llShowLicenseHistory.Enabled = true;
                
            }

        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {


            clsInternationalLicenses iL = new clsInternationalLicenses();

            iL.ApplicationPersonID = ctrlDriverLicenseInfoWithFilter1.driverInfons.PersonID;
            iL.ApplicationDate = DateTime.Now;
            iL.ApplicationStatues = clsApplication.enApplicationStatue.Completed;
            iL.ApplicationLastStatueDate = DateTime.Now;
            iL.ApplicationPaidFees = clsApplicationsTypes.
                Find((int)(clsApplication.enApplicationType.NewInternationalLicense))._AppTypeFees;
            iL.ApplicationCreatedByUserID = clsGlobal.CurrentUser.UserID;
            iL.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;



            iL.DriverID = license.DriverID;
            iL.IssuedUsingLocalLicenseID = license.LicenseID;
            iL.IssueDate = DateTime.Now;
            iL.ExpirationDate = DateTime.Now.AddYears(1);
            iL.ApplicationCreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!iL.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            llShowLicenseInfo.Enabled = (iL.InternationalLicenseID != -1);
            _internationalLicensID = iL.InternationalLicenseID;
            _internationalLicensID = iL.InternationalLicenseID;

            lblApplicationID.Text = iL.ApplicationID.ToString();
            lblInternationalLicenseID.Text = iL.InternationalLicenseID.ToString();

            MessageBox.Show("International License Issued Successfully with ID = " +
                iL.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ctrlDriverLicenseInfoWithFilter1.FilterEnabling = false;

            btnIssueLicense.Enabled = false;

        }

        private void ctrlDriverLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (license.LicenseID == -1)
            {
                MessageBox.Show("No International License Yet!");
                return;
            }
            //int Appid = clsInternationalLicenses.Find(_internationalLicensID).ApplicationID;

            int PersonId = clsLocalDrivingLicenseApplication.FindByApplicationID(license.ApplicationID).ApplicationPersonID;
            frmShowLicensesHestory frmShow = new frmShowLicensesHestory(PersonId);
            frmShow.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_internationalLicensID == -1)
            {
                MessageBox.Show("No International License Yet!");
                return;
            }
            frmShowInternaionalLicenseInfos frm = new frmShowInternaionalLicenseInfos(_internationalLicensID);
            frm.ShowDialog();
        }
    }
}
