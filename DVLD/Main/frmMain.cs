//using DVLD.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.People;
using DVLD.Users;
using DVLD.Global_clases;
using DVLD.Applications;
using DVLD.Tests.Test_Types;
using DVLD.Applications.Local_Driving_Licens;
using DVLD.Drivers;
using DVLD.Licenses.International;
using DVLD.Applications.International;
using DVLD.Applications.Renew_Local_Licens;
using DVLD.Applications.Replce_License_For_Lost_Or_Damage;
using DVLD.Licenses.Detain_License;
using DVLD.Applications.Release_Detained_License;

namespace DVLD
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public delegate void OnUserSignOut(object sender, EventArgs e);
        public event OnUserSignOut onUserSigningOut;

       

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            onUserSigningOut?.Invoke(this, e);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();

            frm.ShowDialog();
        }

        private void currentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails frm = new frmUserDetails(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID, clsGlobal.CurrentUser.PersonId);
            frm.ShowDialog();
        }


        private void manageAplicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplicationsTypes frmApplicationsTypes = new frmApplicationsTypes();
            frmApplicationsTypes.ShowDialog();
        }

        private void mangeTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestTyps frm = new frmTestTyps();
            frm.ShowDialog();
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicensesApplicationsList frmLocalDrivingLicensesApp_IcationsList = new frmLocalDrivingLicensesApplicationsList();
            frmLocalDrivingLicensesApp_IcationsList.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmDriversList Driverslist = new frmDriversList();
            Driverslist.ShowDialog();
        }

        private void internationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frm = new frmNewInternationalLicense();
            frm.ShowDialog();
        }

        private void internationalDrivingLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIntrnationalAppsManage frm = new frmIntrnationalAppsManage();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalLicense frm = new frmRenewLocalLicense();
            frm.ShowDialog();
        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLicenseForLostOrDamage lostOrDamage = new frmReplaceLicenseForLostOrDamage();
            lostOrDamage.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frmListDetainedLicenses = new frmListDetainedLicenses();
            frmListDetainedLicenses.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRealeseDetainedLicen_se frm = new frmRealeseDetainedLicen_se();
            frm.ShowDialog();
        }
    }
}
