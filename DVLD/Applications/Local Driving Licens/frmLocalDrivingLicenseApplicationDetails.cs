using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_Licens
{
    public partial class frmLocalDrivingLicenseApplicationDetails : Form
    {
        int _LDLAID;
        public frmLocalDrivingLicenseApplicationDetails(int LDLAID)
        {
            InitializeComponent();
            _LDLAID = LDLAID;
        }

        private void frmLocalDrivingLicenseApplicationDetails_Load(object sender, EventArgs e)
        {
            ctrlLocalApplicationInfo1.LoadcontrolInfo(_LDLAID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
