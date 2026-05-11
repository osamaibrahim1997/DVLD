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

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        int Userid = -1;
        int PersonID = -1;
        clsUser User;
        bool _FlagForValidation = false;
        public frmChangePassword(int userID, int personID)
        {
            InitializeComponent();
            Userid = userID;
            PersonID = personID; 
        }
        private void _LoadTheUserData()
        {
            User = clsUser.Find(Userid);
            txtUserID.Text = User.UserID.ToString();
            txtUsername.Text = User.UserName;
            txtIsActive.Text = (User.IsActive) ? "Yes" : "No";

        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ucPersonDetails1.LoadPersonDetailsById(PersonID);
            _LoadTheUserData();
        }


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

        private void btnSave_Click(object sender, EventArgs e)
        {
            _FlagForValidation = false;

            if (this.ValidateChildren() && _FlagForValidation)
            {
                return ;
            }


            if (txtCurrentPassword.Text.Trim() != User.Password)
            {
                errorProvider1.SetError(txtCurrentPassword, "Pass Word Dose Not Correct.");
                return ;
            }


            if (txtNerwPassword.Text.Trim() != txtConfirmNewPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmNewPassword, "Pass Word Dose Not Match.");
                return ;
            }

            if (User._UpdatePassword(txtNerwPassword.Text.Trim()))
            {
                MessageBox.Show("Password Updated Succesfully.");

            }
            else
            {
                MessageBox.Show("Password Dosn't Updated Succesfully.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _LoadTheUserData();
        }
    }
}
