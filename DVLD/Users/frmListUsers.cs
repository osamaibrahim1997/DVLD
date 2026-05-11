using DVLD.Global_clases;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmListUsers : Form
    {
        
        public frmListUsers()
        {
            InitializeComponent();
        }       


        private static DataTable dtAllUsers = clsUser.GetAllUsers();
        private void _RefreshUsersListFromDataBase()
        {
            dtAllUsers = clsUser.GetAllUsers();
            
            dgvUsers.DataSource = dtAllUsers;
            lblUsersRecords.Text = dtAllUsers.Rows.Count.ToString();

        }
        private void _RefreshUsersListForEvent(object send , EventArgs e)
        {
            dtAllUsers = clsUser.GetAllUsers();
            
            dgvUsers.DataSource = dtAllUsers;
            lblUsersRecords.Text = dtAllUsers.Rows.Count.ToString();

        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersListFromDataBase();

            cbFilterBy.SelectedIndex = 0;


            if (dtAllUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 100;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 100;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 300;

                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 140;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 120;

            }
           
        }


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 1 ||
                cbFilterBy.SelectedIndex == 2 ||
                cbFilterBy.SelectedIndex == 3 )
            {
                txtFilterText.Text = string.Empty;

                txtFilterText.Visible = true;
                txtFilterText.Enabled = true;
                cbIsActive.Visible = false;
                cbIsActive.SelectedIndex = 0;
            }
            else if (cbFilterBy.SelectedIndex == 0)
            {
                txtFilterText.Visible = true;
                txtFilterText.Text = string.Empty;
                txtFilterText.Enabled = false;
                cbIsActive.Visible = false;
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterText.Text = string.Empty;

                txtFilterText.Visible = false;
                
                cbIsActive.Visible = true;
            }
            lblUsersRecords.Text = dgvUsers.Rows.Count.ToString();

        }

        private void txtFilterText_TextChanged(object sender, EventArgs e)
        {
            string _FilterItem = "";

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    _FilterItem = "PersonID";
                    break;
                case "User ID":
                    _FilterItem = "UserID";
                    break;
                case "User Name":
                    _FilterItem = "Username";
                    break;

                case "Is Active":
                    _FilterItem = "IsActive";
                    break;

                default:
                    break;
            }          


            if (cbFilterBy.SelectedIndex == 0 || txtFilterText.Text == "")
            {
                dtAllUsers.DefaultView.RowFilter = "";
                return;
            }

            
            if (cbFilterBy.SelectedIndex == 1 ||
                cbFilterBy.SelectedIndex == 2)
            {
                dtAllUsers.DefaultView.RowFilter = 
                    string.Format("[{0}] = {1}", _FilterItem, txtFilterText.Text.Trim());
            }
            else
            {
                dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'" , _FilterItem, txtFilterText.Text.Trim());
            }
            lblUsersRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void txtFilterText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex == 2)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

            if (cbFilterBy.SelectedIndex == 3)
            {
                e.Handled =  char.IsWhiteSpace(e.KeyChar);
            }            

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frmAddUser   = new frmAddUpdateUser();
            frmAddUser.OnClosing += _RefreshUsersListForEvent;
            frmAddUser.ShowDialog();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                dtAllUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblUsersRecords.Text = dgvUsers.Rows.Count.ToString();


        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void dgvUsers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvUsers.ClearSelection();
                dgvUsers.Rows[e.RowIndex].Selected = true;
                dgvUsers.CurrentCell = dgvUsers.Rows[e.RowIndex].Cells[0];
            }
        }

        private void ShoeDetailsMenuItem_Click(object sender, EventArgs e)
        {
            
            int userID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            frmUserDetails frm = new frmUserDetails(userID);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            frmAddUpdateUser frmAddUpdateUser = new frmAddUpdateUser(userID);
            frmAddUpdateUser.OnClosing += _RefreshUsersListForEvent;
           
            frmAddUpdateUser.ShowDialog();
        }
        private bool _DeleteUser(int userId)
        { 
            clsUser user = clsUser.Find(userId);
            if (user != null)
            {
                if (user.Delete(userId))
                {
                   return true  ; 
                }
                
            }
        return false ;
        
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;

            if (MessageBox.Show("Are You Sure?", "Delete User?",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (_DeleteUser(userID))
                {
                    MessageBox.Show("Deleted Succefully");
                    _RefreshUsersListFromDataBase();
                }
                else
                {
                    MessageBox.Show("Error Happend, Maybe User Has Relations",
                        "Not Completed", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            
        }

        private void ChangePasswordtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID = (int)dgvUsers.CurrentRow.Cells["PersonID"].Value;
            int userID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            frmChangePassword frmChangePassword = new frmChangePassword(userID,personID);
            frmChangePassword.ShowDialog();
        }

        private void AddNewUserMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frmAdd = new frmAddUpdateUser();
            frmAdd.OnClosing += _RefreshUsersListForEvent;
            frmAdd.ShowDialog();
        }
    }
}
