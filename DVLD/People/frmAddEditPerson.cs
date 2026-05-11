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
using System.IO;




namespace DVLD.People
{
    public partial class frmAddEditPerson : Form
    {
        public delegate void onfrmAddEditClose();
        public event onfrmAddEditClose onfrmAddEditClosing;

        public delegate void onFormClose(object sender,int personID);
        public event onFormClose onfrmClosing; 

        int _PersonIDAddedFromUser ;

        int _FormPersonID = -1;
        public frmAddEditPerson( int PersonID)
        {
            InitializeComponent();
            lblAddEditPerson.Text = "Update Person";
            _FormPersonID = PersonID;
            ucAddEditPerson.OnCloseClicking += CloseTheForm;

        }
        public frmAddEditPerson()
        {
            InitializeComponent();
            _FormPersonID = -1;
            ucAddEditPerson.OnSaveButtonClicking += SetPersonIDLable;
            ucAddEditPerson.OnCloseClicking += CloseTheForm;
        }
        public void CloseTheForm()
        {
            onfrmAddEditClosing?.Invoke();
            onfrmClosing?.Invoke(this, _FormPersonID);
            this.Close();
        }         

                
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            if (_FormPersonID == -1)
            {
                lblPersonID.Text = "N/A";
                lblAddEditPerson.Text = "Add New Person";
                ucAddEditPerson.LoadNewPerson();

            }

            if (_FormPersonID >= 0)
            {
                lblPersonID.Text = _FormPersonID.ToString();
                lblAddEditPerson.Text = "Update Person";
                ucAddEditPerson.LoadPersonForUpdating(_FormPersonID);
            }
        }
        public void SetPersonIDLable(object sender, int PersonID)
        {
            _PersonIDAddedFromUser = PersonID;
            lblPersonID.Text = PersonID.ToString();
            lblAddEditPerson.Text = "Edit Person";
            frmFindPerson.PersonID = PersonID;
            onfrmClosing?.Invoke(this, PersonID);
        }

        private void ucAddEditPerson_Load(object sender, EventArgs e)
        {
            
        }

        private void ucAddEditPerson_OnSaveButtonClicking(object sender, int PersonID)
        {

        }
    }
}
