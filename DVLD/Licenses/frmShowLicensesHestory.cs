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

namespace DVLD.Licenses
{
    public partial class frmShowLicensesHestory : Form
    {
        int _LocalDLicenseAppID;
        int _PersonID;
        public frmShowLicensesHestory(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void frmShowLicensesHestory_Load(object sender, EventArgs e)
        {
            
            if (_PersonID != -1  )
            {
                ucPersonDetails1.LoadPersonDetailsById(_PersonID);
                ctrlDriverLicenses1.LoadLicensesInfosForThisPerson(_PersonID);
                
            }



        }
    }
}
