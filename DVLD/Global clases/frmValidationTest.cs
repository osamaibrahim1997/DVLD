using DVLD.Global_clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmValidationTest : Form
    {
        public frmValidationTest()
        {
            InitializeComponent();
        }
        bool Valid;
        public bool Validation
        {
            get { return Valid; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Valid = false;
            if (txtPassword.Text.Trim() != clsGlobal.CurrentUser.Password
                || string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtPassword, "Pass Word Dose Not Correct.");
                return;
            }
            Valid = true;
            this.Close  ();
        }
    }
}
