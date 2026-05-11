using DVLD_Business;
using DVLD_Business.Countries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DVLD.People
{
    public partial class ucAddEditPerson : UserControl
    {
        clsPerson _Person;

        public event Action<int> onSavineClick;
        protected virtual void onSaveButtonClicking(int personID)
        {
            Action<int> handler = onSavineClick;
            if (handler != null)
            {
                handler(personID);
            }
        }

        public clsPerson ThePersonInfos
        {
            get { return _Person; }
        }


        public delegate void OnSaveButtonClick(object sender, int PersonID);
        public event OnSaveButtonClick OnSaveButtonClicking;
        public event OnSaveButtonClick OnCloseButtonClicking;

        public delegate void OnCloseButtonClick();
        public event OnCloseButtonClick OnCloseClicking;


        public delegate void OnCloseAddingPerson( int PersonID);
        public event OnCloseAddingPerson OnCloseAddingPersonClicking;

        private void _LoadCoutriesInTheComboBox()
        {
            cbCountries.DataSource = clsCountry.LaodCoutriesFromDataBase();
            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "CountryID";
            cbCountries.SelectedIndex = -1;

        }

        public ucAddEditPerson()
        {
            InitializeComponent();
            _LoadCoutriesInTheComboBox();

        }
        private void _ResetControls()
        {
            txtNationalNoPerson.Text = string.Empty;
            txtFirstNamePerson.Text = string.Empty;
            txtSecondNamePerson.Text = string.Empty;
            txtThirdNamePerson.Text = string.Empty;
            txtLastNamePerson.Text = string.Empty;

            rbMale.Checked = false;
            rbFemale.Checked = false;
            txtAdressPerson.Text = string.Empty;
            txtPhonePerson.Text = string.Empty;
            txtEmailPerson.Text = string.Empty;
            txtNationalNoPerson.Text = string.Empty;

            pictureBoxPerson.Image = Properties.Resources.MaleDfaultPic;
        }

        

        public void LoadNewPerson()
        {
            _Person = new clsPerson();
            _ResetControls();
        }
        private void ucAddEditPerson_Load(object sender, EventArgs e)
        {
            dateTimePersonPicker.MaxDate = DateTime.Now.AddYears(-18);           

        }
        

        private void _LaodImage()
        {
            if (!string.IsNullOrEmpty(_Person.ImagePath) && File.Exists(_Person.ImagePath))
            {
                linkLabelRemoveImage.Visible = true;
                //using (var fs = new FileStream(_Person.ImagePath, FileMode.Open, FileAccess.Read))
                //{
                //    pictureBoxPerson.Image = Image.FromStream(fs);
                //}
                pictureBoxPerson.ImageLocation = _Person.ImagePath;

               
                _SelectedImagePath = _Person.ImagePath;
            }
            else
            {
                pictureBoxPerson.Image = (_Person.Gender == 0) ? Properties.Resources.MaleDfaultPic :
                    Properties.Resources.FemaleDfaultPic;
            }
        }

        private void _FillControlsWithPersonFounded()
        {
            txtNationalNoPerson.Text = _Person.NationalNo.ToString();

            txtFirstNamePerson.Text = _Person.FirstName.ToString();
            txtSecondNamePerson.Text = _Person.SecondName.ToString();
            txtThirdNamePerson.Text = _Person.ThirdName.ToString();
            txtLastNamePerson.Text = _Person.LastName.ToString();
            dateTimePersonPicker.Value = _Person.DateOfBirth;

            rbMale.Checked = (_Person.Gender == 0);
            rbFemale.Checked = (_Person.Gender == 1);

            txtAdressPerson.Text = _Person.Address.ToString();
            txtPhonePerson.Text = _Person.Phone.ToString();
            txtEmailPerson.Text = _Person.Email.ToString();
            cbCountries.SelectedValue = _Person.NationalityCountryID;

            _LaodImage();


        }

        public void LoadPersonForUpdating(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                MessageBox.Show("Sorry");
                return;
            }

            _FillControlsWithPersonFounded();

        }

       
        private void _FillPersonFromControls()
        {
            _Person.NationalNo = txtNationalNoPerson.Text;
            _Person.FirstName = txtFirstNamePerson.Text;
            _Person.SecondName = txtSecondNamePerson.Text;
            _Person.ThirdName = txtThirdNamePerson.Text;
            _Person.LastName = txtLastNamePerson.Text;
            _Person.DateOfBirth = dateTimePersonPicker.Value;
            _Person.Gender = rbMale.Checked ? (byte)0 : (byte)1;
            _Person.Address = txtAdressPerson.Text;
            _Person.Phone = txtPhonePerson.Text;
            _Person.Email = txtEmailPerson.Text;
            _Person.NationalityCountryID = (int)cbCountries.SelectedValue;
        }
        private void btnSavePerson_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "Save Details", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            if (!_IsValidForSaveButtonClicking())
            {
                MessageBox.Show("Not All Fields Valid"); return;
            }
            _FillPersonFromControls();
            _Person.ImagePath =  _SavePersonImage(_Person.ImagePath);

            if (_Person.Save())
            {
                OnSaveButtonClicking?.Invoke(this, _Person.PersonID);
                OnCloseAddingPersonClicking?.Invoke(_Person.PersonID);

                MessageBox.Show("Saving Done");

            }
            else
            {
                MessageBox.Show("Saving Faild!");
            }
            
        }
        private void btnclosePerson_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?","Close?", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            OnCloseClicking?.Invoke();
            OnCloseButtonClicking?.Invoke(this ,_Person.PersonID);
            OnCloseAddingPersonClicking?.Invoke( _Person.PersonID);
        }


        /// <summary>
        /// ////////////
        /// </summary>
        /// <returns> Validations </returns>
        /// 

        //private void 
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        private bool ValidateName(TextBox txt, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                errorProvider1.SetError(txt, $"{fieldName} is required");
                return false;
            }

            if (txt.Text.Length < 2 || txt.Text.Length > 20)
            {
                errorProvider1.SetError(txt, $"{fieldName} length must be 2–20");
                return false;
            }

            if (!txt.Text.All(char.IsLetter))
            {
                errorProvider1.SetError(txt, $"{fieldName} Should Be letters only");
                return false;
            }

            errorProvider1.SetError(txt, "");
            return true;
        }

        private bool ValidateNationalNo()
        {
            if (_Person.Mode == clsPerson.enMode.AddNew &&
                clsPerson.IsPersonExistsByNationalNo(txtNationalNoPerson.Text))
            {
                errorProvider1.SetError(txtNationalNoPerson, "Already exists");
                return false;
            }

            if (_Person.Mode == clsPerson.enMode.Update &&
                clsPerson.IsNationalNoUsedByAnotherPerson(
                    txtNationalNoPerson.Text, _Person.PersonID))
            {
                errorProvider1.SetError(txtNationalNoPerson, "Already exists");
                return false;
            }

            errorProvider1.SetError(txtNationalNoPerson, "");
            return true;
        }

        private bool ValidatePhoneNumber()
        {
            if (string.IsNullOrEmpty(txtPhonePerson.Text))
            {
                errorProvider1.SetError(txtPhonePerson, "This Field Is Recuired");
                return false;
            }
            

            if (!long.TryParse(txtPhonePerson.Text, out _))
            {
                errorProvider1.SetError(txtPhonePerson, "Digits only");
                return false;
            }
           
            if (txtPhonePerson.Text.Length > 15 || txtPhonePerson.Text.Length < 9)
            {
                errorProvider1.SetError(txtPhonePerson, "Invalid Phone Length");
                return false;
            }            
                
            errorProvider1.SetError(txtPhonePerson, "");
            return true;
            
        }
        private bool _IsValidForSaveButtonClicking()
        {
            // National No?           
            if (!ValidateNationalNo())            
                return false;            
           

            //  FirstName Person? // Second Name ? //Lsat Name
            if (!ValidateName(txtFirstNamePerson, "First Name")
                || !ValidateName(txtSecondNamePerson, "Second Name")||
                !ValidateName(txtLastNamePerson, "Last Name"))            
                return false;                        


            // Third Name
            if (!string.IsNullOrEmpty(txtThirdNamePerson.Text) && 
                !ValidateName(txtThirdNamePerson, "Third Name"))            
                return false;                    
                        
           

            //Email Person
            if (!IsValidEmail(txtEmailPerson.Text))
            {
                errorProvider1.SetError(txtEmailPerson, "Invalid Email!");
                return false;
            }
            else
            {
                errorProvider1.SetError(txtEmailPerson, "");

            }

            //Phone Person
            if (!ValidatePhoneNumber())            
                return false;           
                errorProvider1.SetError(txtPhonePerson, "");

            

            //Adress Person
            if (!string.IsNullOrEmpty(txtAdressPerson.Text))
            {
                if (txtAdressPerson.Text.Length < 3 || txtAdressPerson.Text.Length > 50)
                {
                    errorProvider1.SetError(txtAdressPerson, "Invalid Length");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(txtAdressPerson, "");
                    
                }
            }


            //Combo box
            if (cbCountries.SelectedIndex == -1)
            {
                errorProvider1.SetError(cbCountries, "Please Select The Country");
                return false;
            }
            else
            {
                errorProvider1.SetError(cbCountries, "");
            }

            // Are Gender Valid?
            if (!rbMale.Checked && !rbFemale.Checked)
            {
                errorProvider1.SetError(rbMale, "Please Select The Gender");
                return false;
            }
            else
            {
                errorProvider1.SetError(rbMale, "");
                
            }


            return true;
        }
        private bool _IsValidNationalNo()
        {
            if (string.IsNullOrWhiteSpace(txtNationalNoPerson.Text))
            {
                errorProvider1.SetError(txtNationalNoPerson, "Required");
                return false;
            }

            if (_Person.Mode == clsPerson.enMode.AddNew &&
                clsPerson.IsPersonExistsByNationalNo(txtNationalNoPerson.Text))
            {
                errorProvider1.SetError(txtNationalNoPerson, "Already exists");
                return false;
            }

            if (_Person.Mode == clsPerson.enMode.Update &&
                clsPerson.IsNationalNoUsedByAnotherPerson(txtNationalNoPerson.Text, _Person.PersonID))
            {
                errorProvider1.SetError(txtNationalNoPerson, "National No already exists");
                errorProvider1.Icon.ToString();
                return false;
            }

            errorProvider1.SetError(txtNationalNoPerson, "");
            return true;
        }


        private void txtNationalNoPerson_Validating(object sender, CancelEventArgs e)
        {
             _IsValidNationalNo();
        }


        private void txtFirstNamePerson_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);           

            foreach (char c in txtFirstNamePerson.Text)
            {
                if (!char.IsLetter(c))
                {
                    errorProvider1.SetError(txtFirstNamePerson, "Invalid Letters");
                    return;
                }
            }

            if ( txtFirstNamePerson.Text.Length > 20)
            {
                errorProvider1.SetError(txtFirstNamePerson, "The Name Is So Long");
                return;                
            }
            if (txtFirstNamePerson.Text.Length < 2)
            {
                errorProvider1.SetError(txtFirstNamePerson, "The Name Is So Short");
                return;
            }

            errorProvider1.SetError(txtFirstNamePerson, "");

        }
        private void txtSecondNamePerson_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
            if (txtSecondNamePerson.Text.Length > 20)
            {
                errorProvider1.SetError(txtSecondNamePerson, "The Name Is So Long");
                return;

            }
            if (txtSecondNamePerson.Text.Length < 2)
            {
                errorProvider1.SetError(txtSecondNamePerson, "The Name Is So Short");
                return;

            }
            foreach (char c in txtSecondNamePerson.Text)
            {
                if (!char.IsLetter(c))
                {
                    errorProvider1.SetError(txtSecondNamePerson, "Invalid Name");
                    return;
                }
            }
            errorProvider1.SetError(txtSecondNamePerson, "");
        }
        private void txtThirdNamePerson_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtThirdNamePerson.Text))
            {
                if (txtThirdNamePerson.Text.Length > 20)
                {
                    errorProvider1.SetError(txtThirdNamePerson, "The Name Is So Long"); return;

                }
                if (txtThirdNamePerson.Text.Length < 2)
                {
                    errorProvider1.SetError(txtThirdNamePerson, "The Name Is So Short"); return;

                }
                foreach (char c in txtThirdNamePerson.Text)
                {
                    if (!char.IsLetter(c))
                    {
                        errorProvider1.SetError(txtThirdNamePerson, "Invalid Name"); return;
                    }
                }
            }

            errorProvider1.SetError(txtThirdNamePerson, "");
        }
        private void txtLastNamePerson_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);

            if (txtLastNamePerson.Text.Length > 20)
            {
                errorProvider1.SetError(txtLastNamePerson, "The Name Is So Long");
                return;

            }
            if (txtLastNamePerson.Text.Length < 2)
            {
                errorProvider1.SetError(txtLastNamePerson, "The Name Is So Short")
                    ; return;

            }
            foreach (char c in txtLastNamePerson.Text)
            {
                if (!char.IsLetter(c))
                {
                    errorProvider1.SetError(txtLastNamePerson, "Invalid Name");
                    return;
                }
            }
            errorProvider1.SetError(txtLastNamePerson, "");
        }
        bool IsValidEmail(string email)
        {
            if (!string.IsNullOrEmpty(txtEmailPerson.Text))
            {   
                return Regex.IsMatch(email,@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            }
            return true;
        }
        private void txtEmailPerson_Validating(object sender, CancelEventArgs e)
        {
            if (!IsValidEmail(txtEmailPerson.Text))
            {
                errorProvider1.SetError(txtEmailPerson, "Invalid Email!");
                return;
            }

            errorProvider1.SetError(txtEmailPerson, "");
        }
        private void txtPhonePerson_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);

            if (!long.TryParse(txtPhonePerson.Text, out _))
            {
                errorProvider1.SetError(txtPhonePerson, "Digits only");
                return;
            }

            if (txtPhonePerson.Text.Length > 15 || txtPhonePerson.Text.Length < 9)  
            {
                errorProvider1.SetError(txtPhonePerson, "Invalid Phone Length");
                return;
            }



            errorProvider1.SetError(txtPhonePerson, "");
        }
        private void txtAdressPerson_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAdressPerson.Text))
            {
                if (txtAdressPerson.Text.Length < 3 || txtAdressPerson.Text.Length > 50)
                {
                    errorProvider1.SetError(txtAdressPerson, "Invalid Length");
                    return;
                }
            }
                errorProvider1.SetError(txtAdressPerson, "");
        }
        private void cbCountries_Validating(object sender, CancelEventArgs e)
        {
            
            if (cbCountries.SelectedIndex == -1)
            {
                errorProvider1.SetError(cbCountries, "Please Select The Country");
                return;
            }
            errorProvider1.SetError(cbCountries, "");
        }
        private void pictureBoxPerson_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                string ext = Path.GetExtension(_Person.ImagePath).ToLower();

                if (ext != ".jpg" && ext != ".png")
                {
                    errorProvider1.SetError(pictureBoxPerson, "Invalid image type");
                    return ;
                }
            }
        }
        /// <summary>
        /// //////////////////
        /// </summary>


        ////////////
        /// Images//////
        string _SelectedImagePath = null;
        private void linkLabelSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.jpg;*.png;*.jpeg";

            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            _SelectedImagePath = ofd.FileName;
            linkLabelRemoveImage.Visible = (_SelectedImagePath != null)?  true : false;
            using (var fs = new FileStream(_SelectedImagePath, FileMode.Open, FileAccess.Read))
            {
               pictureBoxPerson.Image = Image.FromStream(fs);            
            }            
           
        }
        private string _SavePersonImage( string oldImagePath)
        {
            if (string.IsNullOrEmpty(_SelectedImagePath))
                return oldImagePath;

            if (!string.IsNullOrEmpty(oldImagePath) && File.Exists(oldImagePath ))
            {
                File.Delete(oldImagePath);
            }



            string imagesFolder = @"D:\DVLD\DVLD\Images\People";

            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            string newImagePath = Path.Combine(imagesFolder,
                Guid.NewGuid() + Path.GetExtension(_SelectedImagePath) );

            File.Copy(_SelectedImagePath, newImagePath, true);

            return newImagePath;
        }
        private void linkLabelRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "Delete The Image!",
                MessageBoxButtons.OKCancel) != DialogResult.OK     )
            {
                return;
            }
            
            if (!string.IsNullOrEmpty(_Person.ImagePath) &&
                File.Exists(_Person.ImagePath))
            {
                try
                {
                    File.Delete(_Person.ImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting image file:\n" + ex.Message);
                }
            }
            pictureBoxPerson.Image = (_Person.Gender == 0)? 
                    Properties.Resources.MaleDfaultPic : Properties.Resources.FemaleDfaultPic;
                _Person.ImagePath = null;
                _SelectedImagePath = null;
            linkLabelRemoveImage.Visible = false;

        }
     

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked  && _Person.ImagePath == null && _SelectedImagePath == null) 
            {
                pictureBoxPerson.Image = Properties.Resources.MaleDfaultPic;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked  && _Person.ImagePath == null && _SelectedImagePath == null  )
            {
                pictureBoxPerson.Image = Properties.Resources.FemaleDfaultPic;
            }
           
        }

        private void txtNationalNoPerson_Validating_1(object sender, CancelEventArgs e)
        {
            if (!_IsValidNationalNo())
            {
                return;
            }
        }





        //////////////





    }
}
