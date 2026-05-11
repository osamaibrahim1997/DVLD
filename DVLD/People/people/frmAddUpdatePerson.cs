using DVLD.Global_clases;
using DVLD.Properties;
using DVLD_Business;
using DVLD_Business.Countries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Business.clsPerson;


namespace DVLD.People
{
    public partial class frmAddUpdatePerson : Form
    {

        public delegate void OnSAveButtonClick(object sender, int PersonID);
        public event OnSAveButtonClick OnSAve;

        public enum enMode { AddNew = 1, Update = 2 };
        public enMode _Mode;

        clsPerson _Person;
        int _PersonID;

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;

        }
        private void _LoadCoutriesInTheComboBox()
        {
            cbCountries.DataSource = clsCountries.LaodCoutriesFromDataBase();
            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "CountryID";
            cbCountries.SelectedIndex = -1;

        }
        private void _ResetValues()
        {
            _LoadCoutriesInTheComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate;


            pbPersonImage.Image = (rbFemale.Checked) ? Resources.FemaleDfaultPic : Resources.MaleDfaultPic;

            txtFirstName.Text = string.Empty;
            txtSecondName.Text = string.Empty;
            txtThirdName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtNationalNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAdress.Text = string.Empty;
            txtPhone.Text = string.Empty;
            rbFemale.Checked = false;
        }

        private void _LoadPersonData()
        {
            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID,
                    "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            txtNationalNo.Text = _Person.NationalNo.ToString();

            txtFirstName.Text = _Person.FirstName.ToString();
            txtSecondName.Text = _Person.SecondName.ToString();
            txtThirdName.Text = _Person.ThirdName.ToString();
            txtLastName.Text = _Person.LastName.ToString();
            dateTimePicker1.Value = _Person.DateOfBirth;

            rbMale.Checked = (_Person.Gender == 0);
            rbFemale.Checked = (_Person.Gender == 1);

            txtAdress.Text = _Person.Address.ToString();
            txtPhone.Text = _Person.Phone.ToString();
            txtEmail.Text = _Person.Email.ToString();
            cbCountries.SelectedValue = _Person.NationalityCountryID;

            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }

            lblRemoveImage.Visible = (_Person.ImagePath != "");

        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetValues();

            if (_Mode == enMode.Update)
            {
                _LoadPersonData();
            }

        }

        private bool _HandelPersonImage()
        {
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != null)
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException e)
                    {

                        throw;
                    }
                }
                if (pbPersonImage.ImageLocation != null)
                {
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtilts.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void ValidPhoneNumber(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }


            if (!long.TryParse(txtPhone.Text, out _))
            {
                errorProvider1.SetError(txtPhone, "Digits only");
                return;
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }

        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        private void ValidateNationalNo(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null);
            }



            if (txtNationalNo.Text.Trim() != _Person.NationalNo &&
                clsPerson.IsPersonExistsByNationalNo(txtNationalNo.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This Number Is Already Exists!");
                return;
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
            {
                pbPersonImage.Image = Resources.MaleDfaultPic;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
            {
                pbPersonImage.Image = Resources.FemaleDfaultPic;
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "Close?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                return;
            }
            this.Close();
        }

        private void _FillPersonFromControls()
        {
            int CountryId = -1;
            if (!string.IsNullOrEmpty(cbCountries.Text))
            {

                CountryId = clsCountries.FindCountryByName(cbCountries.Text).CountryID;
            }

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAdress.Text.Trim();
            _Person.DateOfBirth = dateTimePicker1.Value;
            _Person.Gender = rbMale.Checked ? (byte)0 : (byte)1;

            _Person.NationalityCountryID = CountryId;
            if (pbPersonImage.ImageLocation != null)
            {
                _Person.ImagePath = pbPersonImage.ImageLocation;
            }
            else
            {
                _Person.ImagePath = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandelPersonImage())
            {
                MessageBox.Show("Image Not Valid, Try To Change It! Or Complete Without It!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(cbCountries.SelectedIndex <0)
            {
                MessageBox.Show("Please Select Country", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonFromControls();

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
               
                _Mode = enMode.Update;
                lblTitle.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                OnSAve?.Invoke(this, _Person.PersonID);

        }

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                lblRemoveImage.Visible = true;
                // ...

            }
        }

        private void lblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            pbPersonImage.Image = (rbMale.Checked)? Resources.MaleDfaultPic : Resources.FemaleDfaultPic;

            lblRemoveImage.Visible=false;

        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
