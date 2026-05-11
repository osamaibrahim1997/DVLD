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
    public partial class frmUserDetails : Form
    {
        int PersonID;
        int UserID;
        clsUser User;
        clsPerson Person;

        public frmUserDetails(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }
        private void LoadUserInfo()
        {
            txtUserID.Text = UserID.ToString();
            txtUsername.Text = User.UserName;
            txtIsActive.Text = User.IsActive ? "YES" : "NO";
        }
        private void frmUserDetails_Load(object sender, EventArgs e)
        {
            User = clsUser.Find(UserID);
            Person = clsPerson.Find(User.PersonId);
            PersonID = Person.PersonID;
            ucPersonDetails1.LoadPersonDetailsById(PersonID);
            LoadUserInfo();
        }
    }
}
