using DVLD.Global_clases;
using DVLD.People.Controls;
using DVLD_Business;
using DVLD_Business.Countries;
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
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        public enum Mode { enAdd = 0, enUpdate = 1 }
        public Mode mode;

        public enum AppStatue { New = 1, Completed = 2, Cancled = 3 }

        int _SelectedPersonID;
      
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplications;

        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            mode = Mode.enAdd;           

        }
        public frmAddUpdateLocalDrivingLicenseApplication(int personId)
        {
            InitializeComponent();
            mode = Mode.enUpdate;
            _SelectedPersonID = personId;
        }

        private void _LoadLicenseClassesInTheComboBox()
        {

            cbLicenseClass.DataSource = clsLicenseClasses.GetAllLicensClasses();
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";
            cbLicenseClass.SelectedIndex = 2;

        }
        private void _RestValues()
        {

            _LoadLicenseClassesInTheComboBox();

            switch (mode)
            {
                case Mode.enAdd:

                    lblTitle.Text = "Add New Local Driving License Application";

                    

                    lblApplicationDate.Text = DateTime.Now.ToString();

                    lblFees.Text = clsApplicationsTypes.Find((int)clsApplication.enApplicationType.NewLocalDrivingLicenseService)._AppTypeFees.ToString();

                    tpApplicationInfo.Enabled = false;

                    lblLocalDrivingLicebseApplicationID.Text = "????";



                    break;

                case Mode.enUpdate:

                    lblTitle.Text = "Edit Local Driving License Application";

                    lblApplicationDate.Text = string.Empty;

                    tpApplicationInfo.Enabled = true;


                    break;
                default:
                    break;
            }

            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            
            lblLocalDrivingLicebseApplicationID.Text = string.Empty;
        }
        private void frmAddUpdateApplication_Load(object sender, EventArgs e)
        {
           
            _RestValues();

        }
        private void NextButtonIfModeIsADD()
        {
            if (ctrPersonCardWithFilter1.PersonIDfromUserControl == -1)
            {
                MessageBox.Show("Please Select A Person!");
                return;
            }
            
            btnApplicationInfoNext.Visible = false;
            _SelectedPersonID = ctrPersonCardWithFilter1.PersonIDfromUserControl;

            btnSave.Enabled = true;

            tpApplicationInfo.Enabled = true;
            tpPersonInfo.Enabled = false;

            tcLocalDrivingLicenseApp.SelectedTab = tcLocalDrivingLicenseApp.TabPages["tpApplicationInfo"];

        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {
          
            switch (mode)
            {
                case Mode.enAdd:

                    NextButtonIfModeIsADD();

                    break;

                case Mode.enUpdate:

                    break;

                default:

                    break;
            }

        }

        private void FillTheObjectFromControls(int LicenseClassID = 1)
        {

            _LocalDrivingLicenseApplications = new clsLocalDrivingLicenseApplication();
            _LocalDrivingLicenseApplications.ApplicationPersonID = _SelectedPersonID;
            _LocalDrivingLicenseApplications.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplications.ApplicationLastStatueDate = DateTime.Now;
            _LocalDrivingLicenseApplications.ApplicationPaidFees = Convert.ToSingle(lblFees.Text);

            _LocalDrivingLicenseApplications.ApplicationCreatedByUserID= clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplications.LicenseClassID = LicenseClassID;
            _LocalDrivingLicenseApplications.ApplicationStatues = clsApplication.enApplicationStatue.New;
            _LocalDrivingLicenseApplications.ApplicationTypeID = (int)clsApplication.enApplicationType.NewLocalDrivingLicenseService; 

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = clsLicenseClasses.Find(cbLicenseClass.Text).LicenseClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID,
      clsApplication.enApplicationType.NewLocalDrivingLicenseService, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show(@"Choose another License Class, the selected Person Already have an
active application for the selected class with id = " + ActiveApplicationID, "Error",
MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }



            if (clsLicense.ISThisPersonHasThisLicense(_SelectedPersonID, LicenseClassID))
            {
                MessageBox.Show("This Person Already Has Active License From This Type", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FillTheObjectFromControls(LicenseClassID);

            if (_LocalDrivingLicenseApplications.Save())
            {
                this.mode = Mode.enUpdate;
                lblLocalDrivingLicebseApplicationID.Text = 
                    _LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID.ToString();
                MessageBox.Show("Application Added");
              

                return;
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

       
    }
}
