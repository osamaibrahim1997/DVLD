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
    public partial class frmFindPerson : Form
    {
        static public int PersonID;
        public enum Mode { enFind = 1, enSearch = 2 }
        public Mode _Mode;        

        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public frmFindPerson()
        {
            InitializeComponent();
        }

        public void LoadFoundedPerson(int PersonID)
        {
            ctrPersonCardWithFilter1
            .LoadPersonIfoFromCtrlParent(PersonID);
        }       

        private void frmFindPerson_Load(object sender, EventArgs e)
        {
            switch (_Mode)
            {
                case Mode.enFind:
                    break;
                case Mode.enSearch:
                    break;
                default:
                    break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
