using DVLD.Licenses.International.Controls;
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
    public partial class frmShowInternaionalLicenseInfos : Form
    {
        int _InternationalLicenseID;
        public frmShowInternaionalLicenseInfos(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;

        }

        private void frmShowInternaionalLicenseInfos_Load(object sender, EventArgs e)
        {
            if (
            !ctrlDriverInternationalLicenseInfo1.LoadInfo(_InternationalLicenseID))
            {
                this.Close();
               
            }
        }
    }
}
