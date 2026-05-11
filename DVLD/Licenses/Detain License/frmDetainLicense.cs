using DVLD.Global_clases;
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

namespace DVLD.Licenses.Detain_License
{
    public partial class frmDetainLicense : Form
    {
        clsLicense _License;
        int _LicenseID;

        public frmDetainLicense()
        {
            InitializeComponent();
            this.AcceptButton = ctrlDriverLicenseInfoWithFilter1.BtnSearch;
        }

        private void SetDefaults()
        {
            lblLicenseID.Text = _LicenseID.ToString();
            lblDetainDate.Text = DateTime.Now.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            
        }


        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseIdSelect(int obj)
        {
            lblLicenseID.Text = "";
            _LicenseID = obj;
            _License = clsLicense.Find(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Sorry, This License Wasn't Found!");
                return;
            }
            if (!_License.IsActive)
            {
                MessageBox.Show("Sorry, This License Isn't Active Any More!");
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                btnDetain.Enabled = false;
                return;
            }

            if (  _License.DetainedInfo != null   )
            {
                if (!_License.DetainedInfo.IsReleased)
                {
                    MessageBox.Show("Sorry, This License Already Detained!");
                    llShowLicenseHistory.Enabled = true;
                    llShowLicenseInfo.Enabled = true;
                    btnDetain.Enabled = false;

                    return;
                }
            }

            SetDefaults();

            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;
            btnDetain.Enabled = true;
        }





        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHestory frmShow = (_LicenseID != -1)? new frmShowLicensesHestory(_License.DriverInfo.PersonID) : null;
            frmShow.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLocalLicense frm = new frmShowLocalLicense(_LicenseID);
            frm.ShowDialog();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text))
            {
                txtFineFees.Focus();
                return;
            }

            if (MessageBox.Show("Are You Sure?", "!", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) != DialogResult.OK)            
                return;

            int DetainID = clsDetainedLicense.DetainLicenseAndGetDetainID(_LicenseID, Convert.ToSingle(txtFineFees.Text),
                clsGlobal.CurrentUser.UserID);

            if (DetainID != -1)
            {
                lblDetainID.Text = DetainID.ToString();

                MessageBox.Show($"License With ID {_LicenseID} Detained Successfully.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ctrlDriverLicenseInfoWithFilter1.FilterEnabling = false;
            }
            else
            {
                MessageBox.Show($"License With ID {_LicenseID} Doesn't Detained Successfully.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }

        }
    }
}
