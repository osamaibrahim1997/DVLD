using DVLD.Global_clases;
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

namespace DVLD.People.Controls
{
    public partial class ctrPersonCardWithFilter : UserControl
    {
        bool _FlagForValidation = false;
        // Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }

        public bool CbFilter
        {
            set
            {
                groupBox1.Enabled = value;
            }
        }




        public ctrPersonCardWithFilter( )
        {
            InitializeComponent();           
        }

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get 
            { 
                return _ShowAddPerson; 
            }            
            set
            {
                _ShowAddPerson = value;
                btnAdd.Visible = _ShowAddPerson;
            }
        }

       

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            set
            {
                _FilterEnabled = value;
                groupBox1.Visible = _FilterEnabled;
            }

            get { return _FilterEnabled; }
        }

        public int PersonIDfromUserControl
        {
            get { return ucPersonDetails1.PersonID; }
        }

        public clsPerson SelectedPerson
        {
            get { return ucPersonDetails1.SelectedPerson; }
        }


        public void LoadPersonIfoFromCtrlParent(int PersonID)
        {

            cbFilterBy.SelectedIndex = 0;
            txtFilterText.Text = PersonID.ToString();
            ucPersonDetails1.LoadPersonDetailsById(PersonID);

        }

        public void SearchPerson(object sender, int PersonID)
        {
            cbFilterBy.SelectedIndex=0;
            txtFilterText.Text = PersonID.ToString();
            ucPersonDetails1.LoadPersonDetailsById(PersonID);           
        }


        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAddEdit = new frmAddUpdatePerson();
           
            frmAddEdit.OnSAve += SearchPerson;
            frmAddEdit.ShowDialog();          
            
        }       

        private void _FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "PersonID":

                    if (!string.IsNullOrEmpty(txtFilterText.Text) &&
                        clsValidations.IsNumber(txtFilterText.Text))
                    {
                        int personID = int.Parse(txtFilterText.Text);
                        ucPersonDetails1.LoadPersonDetailsById(personID);
                    }
                    else
                    {
                        errorProvider1.SetError(txtFilterText, "Invalid ID");
                        return;
                    }
                    break;

                case "NationalNo":
                    if (!string.IsNullOrEmpty(txtFilterText.Text.Trim()))
                    {
                        ucPersonDetails1.LoadPersonDetailsByNationalNo(txtFilterText.Text.Trim());
                        txtFilterText.Text = txtFilterText.Text.Trim();
                    }
                    break;
                default:
                    break;
            }


            if (OnPersonSelected != null && FilterEnabled)                
                OnPersonSelected(ucPersonDetails1.PersonID);

        }


        private void btnSeachPerson_Click(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex < 0)
            {
                errorProvider1.SetError(cbFilterBy, "Please Select The Filter Type!");
                return;
            }
            else
            {
                errorProvider1.SetError(cbFilterBy, "");
            }
            this.ValidateChildren();
            if (_FlagForValidation)
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FindNow();

        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            
            TextBox theControlScanned = ((TextBox)sender);
            if (string.IsNullOrEmpty(theControlScanned.Text.Trim()))
            {
                _FlagForValidation = true;

                errorProvider1.SetError(theControlScanned, "This field is required!");
            }
            else
            {
                _FlagForValidation = false;
                errorProvider1.SetError(theControlScanned, null);
            }

        }


        private void txtFilterText_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtFilterText, "");

            if (cbFilterBy.Text == "PersonID")
            {               
                if (!clsValidations.IsDigit(txtFilterText) && 
                    !string.IsNullOrEmpty(txtFilterText.Text.Trim()))
                {
                    errorProvider1.SetError(txtFilterText, "Digits Only");
                    return;
                }
            }
        }

        private void txtFilterText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "PersonID" && !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
            }

            if (e.KeyChar == (char)13)
            {
                btnSeachPerson.PerformClick();
            }
        }

      
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex > 0)
            {
                txtFilterText.Focus();
            }
            errorProvider1.SetError(cbFilterBy, "");
            txtFilterText.Enabled = (cbFilterBy.SelectedIndex > 0);   
        }

        private void ctrPersonCardWithFilter_AutoValidateChanged(object sender, EventArgs e)
        {
            
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAddEdit = new frmAddUpdatePerson();

            frmAddEdit.OnSAve += SearchPerson;
            frmAddEdit.ShowDialog();
        }
    }
}
