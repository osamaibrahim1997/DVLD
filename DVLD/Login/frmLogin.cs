using DVLD.Global_clases;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        clsUser _User;
        private bool IsValidCridintials()
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Sorry Some Fields Empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

              _User = clsUser.Find(txtUserName.Text);

            if (_User == null) 
            {
                MessageBox.Show("Sorry, This Username Isn't Exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;

            }

            if (_User.UserName == txtUserName.Text.Trim() &&
                _User.Password != txtPassword.Text.Trim())
            {
                MessageBox.Show("Sorry, Invalid Username/Password!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_User.IsActive != true)
            {
                MessageBox.Show("Sorry, Your Account Dactived! Please Contact Your Admin", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }

       
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsValidCridintials())
            {
                if (cbRememberMe.Checked)              
                    clsGlobal.RememberUsernameAndPassword(_User.UserName.Trim(), _User.Password.Trim());
                else
                    clsGlobal.RememberUsernameAndPassword("", "");

                
                clsGlobal.CurrentUser = _User;
                this.Hide();

                frmMain frmMain = new frmMain();
                frmMain.onUserSigningOut += _OnSignOutEvent;
                frmMain.ShowDialog();
            }
            else
            {
                txtUserName.Focus();
            }

        }

        private void _OnSignOutEvent(object sender, EventArgs e)
        {
            if (!cbRememberMe.Checked)
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtUserName.Focus();

            }
            this.Show();
            
        }
     

        private void btnCloseLoginScreen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string username = ""; string password = "";

            if (clsGlobal.GetStoredCredential(ref username,ref password))
            {
                txtUserName.Text = username;
                txtPassword.Text = password;
                cbRememberMe.Checked = true;

            }
            else 
            {cbRememberMe.Checked=false;}
        }
    }
}
