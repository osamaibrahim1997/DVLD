using DVLD.Properties;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class ucPersonDetails : UserControl
    {
        private clsPerson _Person;

        private int _PersonID = -1;

        public clsPerson SelectedPerson
        {
            get {  return _Person; }
        }


        public int PersonID
        {
            get { return _PersonID; }
        }


        public ucPersonDetails()
        {
            InitializeComponent();
            
        }

        private void ucPersonDetails_Load(object sender, EventArgs e)
        {
            
        }

        private void _LoadImage()
        {
            if (!string.IsNullOrEmpty(_Person.ImagePath) && File.Exists(_Person.ImagePath))
            {
                pBPerson.ImageLocation = _Person.ImagePath;
                return;
            }
            pBPerson.Image = (_Person.Gender == 0) ?  Properties.Resources.MaleDfaultPic :
                Properties.Resources.FemaleDfaultPic;

        }

        private void _LoadPersonDataInTheLables()
        {

            _PersonID = _Person.PersonID;
            lblID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " +
                (string.IsNullOrEmpty(_Person.ThirdName) ? "" : _Person.ThirdName) + " " +
                _Person.LastName;

            lblNationalNo.Text = _Person.NationalNo;
            lblGender.Text = (_Person.Gender == 0) ? "Male" : "Female";

            lblEmail.Text = _Person.Email;
            lblAdress.Text = string.IsNullOrEmpty(_Person.Address) ? "" : _Person.Address;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text = _Person.CountryName;

            pictureBoxMan.Visible = _Person.Gender == 0;
            pictureBoxWomen.Visible = _Person.Gender == 1;

            lblPersonName.Text = _Person.FirstName + " " + _Person.LastName + " Details";
            _LoadImage();

        }

        private void _ResetDefaults()
        {
            _PersonID = -1;
            lblID.Text = "[???]";
            lblAdress.Text = "[???]";
            lblCountry.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblEmail.Text = "[???]";
            lblGender.Text = "[???]";
            lblName.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblPhone.Text = "[???]";
            pBPerson.Image = Resources.MaleDfaultPic;
            pictureBoxWomen.Visible = false;

        }

        public void LoadPersonDetailsById(int PersonID)
        {
            
            _Person = clsPerson.Find(PersonID);
                                   

            if (_Person == null)
            {
                _ResetDefaults();
                MessageBox.Show("Sorry No Result Match!");
                return;
            }
            _LoadPersonDataInTheLables();

        }

        public void LoadPersonDetailsByNationalNo(string NationalNo)
        {
            
            _Person = clsPerson.Find(NationalNo);

            if (_Person == null)
            {
                _ResetDefaults();
                MessageBox.Show("Sorry No Result Match!");
                return;
            }
            _LoadPersonDataInTheLables();

        }


        private void linkLabelEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmValidationTest frm = new frmValidationTest();
            frm.ShowDialog();
            if (!frm.Validation)
            {
                return;
            }

            if (_Person == null) { return; }
            frmAddUpdatePerson frmAddEditPerson = new frmAddUpdatePerson(_Person.PersonID);
            frmAddEditPerson.ShowDialog();
            LoadPersonDetailsById(_PersonID);
        }

        private void pictureBoxPerson_Click(object sender, EventArgs e)
        {

        }

      
    }
}
