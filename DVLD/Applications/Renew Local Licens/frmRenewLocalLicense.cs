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

namespace DVLD.Applications.Renew_Local_Licens
{
    public partial class frmRenewLocalLicense : Form
    {
        clsLicense _oldLicense;
        clsLicense _RenewedLicense;


        public frmRenewLocalLicense()
        {
            InitializeComponent();
            AcceptButton = (ctrlDriverLicenseInfoWithFilter1.TxtLicenseID.Focused) ? ctrlDriverLicenseInfoWithFilter1.BtnSearch :
                btnRenewLicense;
        }


        

        private void frmRenewLocalLicense_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = DateTime.Now.ToString();
            lblAppFees.Text = clsApplicationsTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense)
                ._AppTypeFees.ToString();

            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseIdSelect(int obj)
        {
            _oldLicense = clsLicense.Find(obj);

            if (_oldLicense.ExpirationDate > DateTime.Now )
            {
                MessageBox.Show($"Sorry , this License Is't Expire Yet, It'll Expire On {_oldLicense.ExpirationDate.ToString()}" );
                btnRenewLicense.Enabled = false;
                llShowLicenseHistory.Enabled = true;
                return;
            }

            
            if(!_oldLicense.IsActive)
            {
                MessageBox.Show($"Sorry , this Driver have Active License, It'll Expire On {_oldLicense.ExpirationDate.ToString()}");
                btnRenewLicense.Enabled = false;
                llShowLicenseHistory.Enabled = true;
                return;
            }
           

            lblFeesLicense.Text = _oldLicense.PaidFees.ToString();
            lblTotalFees.Text = int.TryParse(lblAppFees.Text, out int s) ? (s + _oldLicense.PaidFees).ToString() : "";
            lblOldLicenseID.Text = _oldLicense.LicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(clsLicenseClasses.Find(_oldLicense.LicenseClassID)
                .DefaultValidityLength).ToString();


            btnRenewLicense.Enabled = true;
            llShowLicenseHistory.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHestory frm = new frmShowLicensesHestory(_oldLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure ?","!", MessageBoxButtons.OKCancel  ) != DialogResult.OK)
            {
                return;
            }
            
            clsLicense NewLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(),
               clsGlobal.CurrentUser.UserID);


            if (NewLicense != null)
            {
                _RenewedLicense = NewLicense;
                lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
               
                lblApplicationID.Text = NewLicense.ApplicationID.ToString();
               
                llShowLicenseInfo.Enabled = true;
               
                MessageBox.Show($"Done Successfully, New License ID Is {NewLicense.LicenseID} .");
               
                btnRenewLicense.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.FilterEnabling = false;

            }                
            else               
            {
                MessageBox.Show("this Operation wasn't Done Successfully");                
            }           
           

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLocalLicense frm = new frmShowLocalLicense(_RenewedLicense.LicenseID);
            frm.ShowDialog();
        }
    }
}
