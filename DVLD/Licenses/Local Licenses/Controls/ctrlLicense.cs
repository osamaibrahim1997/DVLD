using DVLD.Classes;
using DVLD.Properties;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlLicense : UserControl
    {
        private clsLicense _License;
        private int _LicenseID;



        public ctrlLicense()
        {
            InitializeComponent();
        }

        public void _LoadLicenseInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(LicenseID);

            if (_License != null)
            {
                lblClass.Text = _License.LicenseClassInfo.ClassName;
                lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
                lblLicenseID.Text = LicenseID.ToString();   
                lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo.ToString();
                lblGendor.Text = (_License.DriverInfo.PersonInfo.Gender) == 0 ? "Male" : "Female";
                lblIssueDate.Text  = _License.IssueDate.ToString();
                lblIssueReason.Text = _License.IssueReason.ToString();
                lblNotes.Text = _License.Notes != "" ? _License.Notes.ToString() : "No Notes.";
                lblIsActive.Text = (_License.IsActive) ? "Yes" : "No";
                lblDateOfBirth.Text = clsFormat.DateToString(_License.DriverInfo.PersonInfo.DateOfBirth);
                lblDriverID.Text = _License.DriverID.ToString();
                lblExpirationDate.Text = clsFormat.DateToString(_License.ExpirationDate);
                lblIsDetained.Text = clsDetainedLicense.IsLicenseDetained(LicenseID) ? "Yes" : "No";

                _LoadPersonImage();


            }

        }
        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gender == 0) 
            {
                pbPersonImage.Image = Resources.MaleDfaultPic;
            }
            else
            {
                pbPersonImage.Image = Resources.FemaleDfaultPic;
            }
            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if (_License.DriverInfo.PersonInfo.ImagePath != "") 
            {
                if (File.Exists(_License.DriverInfo.PersonInfo.ImagePath))
                {
                    pbPersonImage.Load(_License.DriverInfo.PersonInfo.ImagePath);
                }
                else
                {
                    //    MessageBox.Show("Could not find this image: = "
                    //        + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

    }
}
