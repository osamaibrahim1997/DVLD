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
    public partial class frmShowLocalLicense : Form
    {
        int _LicenseID;

        public frmShowLocalLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void frmShowLocalLicense_Load(object sender, EventArgs e)
        {
            ctrlLicense1._LoadLicenseInfo(_LicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close      ();
        }
    }
}
