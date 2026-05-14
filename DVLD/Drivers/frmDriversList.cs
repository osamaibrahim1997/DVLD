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

namespace DVLD.Drivers
{
    public partial class frmDriversList : Form
    {
        private DataTable _AllDriversList;

        public frmDriversList()
        {
            InitializeComponent();
        }

        private void frmDriversList_Load(object sender, EventArgs e)
        {
            _AllDriversList = clsDriver.GetAllDriversList();
                 dgvDrivers.DataSource = _AllDriversList;
            lblRecordsCount.Text = _AllDriversList.Rows.Count.ToString();
            if (_AllDriversList.Rows.Count > 0 )
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 120;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = string.Empty;
            
            if (cbFilterBy.Text != "None")
            {
                txtFilterValue.Visible = true;
            }
            else
            {
                txtFilterValue.Visible = false ;

            }



        }
        
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string _FilterValue = "";
            
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    _FilterValue = "DriverID";
                    break;

                case "Person ID":
                    _FilterValue = "PersonID";
                    break;

                case "National No.":
                    _FilterValue = "NationalNo";
                    break;


                case "Full Name":
                    _FilterValue = "FullName";
                    break;

                default:
                    _FilterValue = "None";
                    break;

            }

            if (cbFilterBy.Text == "None")
            {
                _AllDriversList.DefaultView.RowFilter = "";
            }


            if (txtFilterValue.Text.Trim() == "" || _FilterValue == "None")
            {
                _AllDriversList.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }
         
            if (_FilterValue != "FullName" && _FilterValue != "NationalNo")    
            {
                _AllDriversList.DefaultView.RowFilter = string.Format("[{0}] = {1}", _FilterValue, txtFilterValue.Text.Trim());
            }
            else
            {
                _AllDriversList.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", _FilterValue, txtFilterValue.Text.Trim());

            }

            lblRecordsCount.Text = _AllDriversList.Rows.Count.ToString();
           
        }
    }
}
