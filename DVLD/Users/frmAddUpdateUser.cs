using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {

        public enum Mode { enAdd = 0, enUpdate = 1 }
        public Mode _Mode = Mode.enAdd;

        private int _UserID = -1;
        private clsUser _User;
        private int _CurrentPersonID = -1;


        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = Mode.enAdd;

        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = Mode.enUpdate;
        }

        private bool _CheckIFThisPersonIsAUser()
        {
            if (ctrPersonCardWithFilter1.PersonIDfromUserControl == -1)
            {
                MessageBox.Show("Please Define A Person.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (clsUser.IsThisPersonAUser(ctrPersonCardWithFilter1.PersonIDfromUserControl))
            {
                MessageBox.Show("Sorry, This Person Is Already A User.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            return true;            
        }

      

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == Mode.enAdd)
            {
                if (!_CheckIFThisPersonIsAUser())
                {
                    return;
                }
                
                _ResetValues();
          
                _CurrentPersonID = ctrPersonCardWithFilter1.PersonIDfromUserControl;
                btnSave.Enabled = true;
                tbLoginInfo.Enabled = true;

                tcAddUpdateUser.SelectedTab = tcAddUpdateUser.TabPages["tbLoginInfo"];
                return;
            }

            if (_Mode == Mode.enUpdate && !_CheckIFThisPersonIsAUser())
            {
                MessageBox.Show("Sorry Invalid");
                return;

            }
            else
            {
                _CurrentPersonID = _User.PersonId;
                btnSave.Enabled = true;
                tbLoginInfo.Enabled = true;
                
                tcAddUpdateUser.SelectedTab = tcAddUpdateUser.TabPages["tbLoginInfo"];
            }
        }

       

        bool _FlagForValidation = false;
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            TextBox theControlScanned = ((TextBox)sender);
            if (string.IsNullOrEmpty(theControlScanned.Text.Trim()))
            {

                _FlagForValidation = true;
                errorProvider1.SetError(theControlScanned, "This field is required!");
                return;
            }

            errorProvider1.SetError(theControlScanned, null);
        }

        private void _FillUserFromControls()
        {
            _User.UserName = this.txtUserName.Text.Trim();
            _User.Password = this.txtPassword.Text.Trim();
            _User.PersonId = _CurrentPersonID;
            _User.IsActive = (chkIsActive.Checked);
            
        }


        private void _LoadData()
        {
            _User = clsUser.Find(_UserID);
            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _UserID,
                    "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }


            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
            lblUserID.Text = _UserID.ToString();
            tcAddUpdateUser.SelectedTab = tcAddUpdateUser.TabPages["tbLoginInfo"];
            ctrPersonCardWithFilter1.CbFilter = false;
            ctrPersonCardWithFilter1.LoadPersonIfoFromCtrlParent(_User.PersonId);
            _CurrentPersonID = _User.PersonId;
        }

        void _ResetValues()
        {
            if (_Mode == Mode.enAdd)
            {
                lblTitle.Text = "Add New User";
                _User = new clsUser();
            }
            else
            {
                lblTitle.Text = "Update User";                
            }

            _FlagForValidation = false;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            chkIsActive.Checked = true;

        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetValues();

            if (_Mode == Mode.enUpdate)
            {
                _LoadData();
            }
        }
        private bool _IsUsernameAndPassWordValid(string username)
        {

            if (clsUser.FindByUsername(username))
            {
                errorProvider1.SetError(txtUserName, "This Username Already Exists");
                return false;
            }


            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmPassword, "Pass Word Not Match Confirm");
                return false;
            }


            return true;
        }
        private bool SaveNewUser()
        {
            _FlagForValidation = false;
            if (this.ValidateChildren() && _FlagForValidation)
            {
                //MessageBox.Show("Sorry, Invalid");
                return false;
            }

            if (!_IsUsernameAndPassWordValid(txtUserName.Text.Trim()))
            {
               
                return false;
            }

                _User = new clsUser();

                _FillUserFromControls();

            if (_User.Save())
            {
                MessageBox.Show("done");
                lblUserID.Text = _User.UserID.ToString();
                _Mode = Mode.enUpdate;

                lblTitle.Text = "UPDATE USER";
            }
            else
            {
                MessageBox.Show("Operation Dosn't Completed.");

            }

            return true;
        }

        private bool _UpdateUser()
        {
            //validate empty box
            _FlagForValidation = false;
            if (this.ValidateChildren() && _FlagForValidation)
            {
                MessageBox.Show("Sorry, Invalid");
                return false;
            }

            if (_User.ISUserNameUsedByAnotherPerson(txtUserName.Text.Trim(), _CurrentPersonID))
            {
                MessageBox.Show("Sorry, Username Used By Another Person");
                return false;
            }


            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmPassword, "Pass Word Not Match Confirm");
                return false;
            }

            _FillUserFromControls();

            if (_User.Save())
            {
                MessageBox.Show("done");
                

            }
            else
            {
                MessageBox.Show("Operation Dosn't Completed.");

            }


            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == Mode.enAdd)
            {
                SaveNewUser();
                return;
            }

            if (_Mode == Mode.enUpdate)
            {
                _UpdateUser();
            }
            
           
        }

   

        public delegate void OnClose(object sender, EventArgs e);
        public event OnClose OnClosing;
        private void btnClose_Click(object sender, EventArgs e)
        {
            OnClosing?.Invoke(this, e);
            this.Close();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
