using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmShowPersonDetails : Form
    {
      
        
        public frmShowPersonDetails(int personID)
        {
            InitializeComponent();            
           
            ucPersonDetails1.LoadPersonDetailsById(personID);
        }
        
        public frmShowPersonDetails(string NationalNo)
        {
            InitializeComponent();            
           
            ucPersonDetails1.LoadPersonDetailsByNationalNo(NationalNo);
        }

        private void frmShowPersonDetails_Load(object sender, EventArgs e)
        {
            //person = clsPerson.Find(_PersonId);

            //lblfrmTitle.Text = person.FirstName + " " + person.LastName + " Details";
            //lblfrmTitle.Text = ucPersonDetails1._Person.FirstName + " " + ucPersonDetails1._Person.LastName + " Details";
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

    }
}
