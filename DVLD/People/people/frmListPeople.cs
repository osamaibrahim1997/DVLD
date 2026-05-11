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

namespace DVLD.People
{
    public partial class frmListPeople : Form
    {
        public frmListPeople()
        {
            InitializeComponent();
        }
        string _FilterItem;

        private static DataTable _listPeople = clsPerson.GetAllPersons();

        private DataTable _dtPeople = _listPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
             "FirstName", "SecondName", "ThirdName", "LastName", "GendorCaption",
             "DateOfBirth", "CountryName",
             "Phone", "Email");


        private void _RefreshPersonsData()
        {
            _listPeople = clsPerson.GetAllPersons();

            dgvPeople.DataSource = _dtPeople;
            lblPeopleRecords.Text = dgvPeople.Rows.Count.ToString();
        }
       

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;
            cbFilter.SelectedIndex = 0;
            lblPeopleRecords.Text = dgvPeople.Rows.Count.ToString();

            if (_dtPeople.Rows.Count > 0 )
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 120;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;


                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 140;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;


                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;


                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 170;
            }

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {            
            frmAddUpdatePerson frmAddUpdatePerson = new frmAddUpdatePerson();
            frmAddUpdatePerson.ShowDialog();
        } 

        private void dgvPeople_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)   
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvPeople.ClearSelection();
                dgvPeople.Rows[e.RowIndex].Selected = true;
                dgvPeople.CurrentCell = dgvPeople.Rows[e.RowIndex].Cells[0];
            }
        }


        private void cmsDelete_Click(object sender, EventArgs e)
        {
            int personID =(int)dgvPeople.CurrentRow.Cells["PersonID"].Value;


            if (MessageBox.Show("Are You Sure About Deleting?", "Confrim Delete",
                MessageBoxButtons.YesNo) == DialogResult.No) return;

            
            if (clsPerson.DeletePerson(personID))
                MessageBox.Show("Deleted Successfully");            
            else            
                MessageBox.Show("Delete Failed");
            _RefreshPersonsData();

        }

        private void cmsEditPerson_Click(object sender, EventArgs e)
        {
            int personID = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;

            

            frmAddUpdatePerson frmAddUpdatePerson = new frmAddUpdatePerson(personID);
            frmAddUpdatePerson.ShowDialog();

            _RefreshPersonsData();
        }

        private void cmsAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmAddEdit = new frmAddEditPerson();

            frmAddEdit.ShowDialog();
            _RefreshPersonsData();
        }

        private void cmsShowDetails_Click(object sender, EventArgs e)
        {
            int personId = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;

            frmShowPersonDetails frm =  new frmShowPersonDetails(personId);
            frm.ShowDialog();
        }



        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterItem = cbFilter.SelectedItem.ToString();
            txtFilterText.Visible = (cbFilter.SelectedIndex != 0)? true: false;
        }

        private void txtFilterText_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value contains nothing.

            if (txtFilterText.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblPeopleRecords.Text = dgvPeople.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterText.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterText.Text.Trim());

            lblPeopleRecords.Text = dgvPeople.Rows.Count.ToString();

            //_RefreshPersonsDataForTheFilter();
        }      

       

        private void txtFilterText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void frmListPeople_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
