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

namespace DVLD.Applications
{
    public partial class frmUpdateAppType : Form
    {
        public delegate void UpdateAppTypeEventHandler();
        public UpdateAppTypeEventHandler UpdateAppType;

        int _AppID;
        clsApplicationsTypes _AppType;




        public frmUpdateAppType(int AppID)
        {
            InitializeComponent();
            _AppID = AppID;
        }

        private void frmUpdateAppType_Load(object sender, EventArgs e)
        {
            _AppType = clsApplicationsTypes.Find(_AppID);
            lblId.Text = _AppID.ToString();
            txtTitle.Text = _AppType._AppTypeTitle;
            txtFees.Text = _AppType._AppTypeFees.ToString() ;
        }


        private bool ValidateTexts()
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()) || string.IsNullOrEmpty(txtFees.Text.Trim()))    
            {
                MessageBox.Show("Some Fields Note Valid");
                return false;
            }
            return true;
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        private void FillAppTypeFromControls()
        {
            _AppType._AppTypeTitle = txtTitle.Text.Trim();
                float.TryParse(txtFees.Text.Trim(), out float f);
            _AppType._AppTypeFees = f;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateTexts())
            {
                FillAppTypeFromControls();
                if (_AppType.Save())
                {
                    
                    MessageBox.Show("Updating Done", "Successfull Updating", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateAppType?.Invoke();
                }
                else
                {
                    MessageBox.Show("Updating Dosn't Done", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
