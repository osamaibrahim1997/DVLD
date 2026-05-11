using DVLD.Classes;
using DVLD.People;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications
{
    public partial class ctrApplicationBasicInfo : UserControl
    {
        private clsApplication _Application;
        private int _ApplicationID ;
        public ctrApplicationBasicInfo()
        {
            InitializeComponent();
        }
        public void ResetApplicationInfo()
        {
            _ApplicationID = -1;

            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblType.Text = "[????]";
            lblFees.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
        }

        private void FillApplicationInfoInTheControls()
        {
            _ApplicationID = _Application.ApplicationID;
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.StatuesText;
            lblType.Text = _Application.ApplicationTypeInfo._AppTypeTitle;
            lblFees.Text = _Application.ApplicationPaidFees.ToString();
            lblApplicant.Text = _Application.ApplicantFullName;
            lblDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(_Application.ApplicationLastStatueDate);
            lblCreatedByUser.Text = _Application.CreatedByUserInfo.UserName;
        }

       
        public void LoadApplicationInfo(int ApplicationID)
        {
            _Application = clsApplication.Find(ApplicationID);
            if (_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                FillApplicationInfoInTheControls();
            }
        }


        private void ctrApplicationBasicInfo_Load(object sender, EventArgs e)
        {

        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonDetails frmShowPersonDetails = new frmShowPersonDetails(_Application.ApplicationPersonID);
            frmShowPersonDetails.ShowDialog();
            LoadApplicationInfo(_Application.ApplicationID);
        }
    }
}
