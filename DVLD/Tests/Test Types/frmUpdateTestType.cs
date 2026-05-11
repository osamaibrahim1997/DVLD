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

namespace DVLD.Tests.Test_Types
{
    public partial class frmUpdateTestType : Form
    {
        int TestTypeId;
        clsTestType testType;

        public frmUpdateTestType(int testTypeID)
        {
            InitializeComponent();
            this.TestTypeId = testTypeID;
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }


        }

       
        private bool ValidateTexts()
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()) || string.IsNullOrEmpty(txtFees.Text.Trim()) || 
                string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                MessageBox.Show("Some Fields Note Valid");
                return false;
            }
            return true;
        }
        private void _FillObjectFromControls()
        {
            testType.TypeTitle = txtTitle.Text.Trim();
            testType.TestTypeDescription = txtDescription.Text.Trim();
            testType.TestTypeFees = Convert.ToSingle(txtFees.Text.Trim());
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateTexts())
            {
                _FillObjectFromControls();
                if (testType.Save())
                {

                }
            }
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            testType = clsTestType.Find((clsTestType.enTestType)TestTypeId);
            if (testType != null)
            {
                lblTestTypeID.Text = TestTypeId.ToString();
                txtTitle.Text = testType.TypeTitle;
                txtDescription.Text = testType.TestTypeDescription;
                txtFees.Text = testType.TestTypeFees.ToString();
            }
            else
            {
                MessageBox.Show("Sorry, Test Not Valid Yet");
            }
        }
    }
}
