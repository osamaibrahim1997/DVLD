using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {

        int _LicenseId =-1;
        clsLicense LicenseInfo ;

        public event Action<int> OnLicenseIdSelect;
        public clsDriver  driverInfons;


        private bool filterEnabled = true;

        public bool FilterEnabling
        {
            get {  return filterEnabled; }

            set
            {
                filterEnabled = value;
                gbFilters.Enabled = filterEnabled;
            }
        }
        public TextBox TxtLicenseID
        {
            get { return txtLicenseID; }    
        }
        public int _LicenseID
        {
            get { return _LicenseId; }
        }

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }
            

        public ctrlDriverLicenseInfoWithFilter(int LicenseID)
        {
            InitializeComponent();
            
         
        }
            
        public Button BtnSearch
        {
            get { return btnFind; }
        }
        
        public clsLicense SelectedLicenseInfo
        {
            get { return LicenseInfo; }
        }

        public void SelectLicenseFromOutside(int LicenseID)
        {
            if (clsLicense.LicenseExists(LicenseID))
            {
                _LicenseId = LicenseID;
                LicenseInfo = clsLicense.Find(LicenseID);
                driverInfons = clsDriver.Find(LicenseInfo.DriverID);
                ctrlLicense1._LoadLicenseInfo(LicenseID);
                OnLicenseIdSelect?.Invoke(LicenseID);

                FilterEnabling = false;
                TxtLicenseID.Text = _LicenseID.ToString();
            }
            else
            {
                MessageBox.Show("Sorry, No License With ID " + txtLicenseID.Text, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            
            if (int.TryParse(txtLicenseID.Text.Trim(), out int LicenseID))
            {
                if (clsLicense.LicenseExists(LicenseID))
                {
                    _LicenseId = LicenseID;
                    LicenseInfo = clsLicense.Find(LicenseID);
                    driverInfons = clsDriver.Find(LicenseInfo.DriverID);
                    ctrlLicense1._LoadLicenseInfo(LicenseID);
                    OnLicenseIdSelect?.Invoke(LicenseID);
                }
                else
                {
                        MessageBox.Show("Sorry, No License With ID " + txtLicenseID.Text, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("Sorry, Invalid ID " +  txtLicenseID.Text, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error) ;
                
            }
        }
    }
}
