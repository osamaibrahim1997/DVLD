using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestType
    {

        public enum Mode { enAdd = 0 , enUpdate =1}

        public Mode _Mode;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }
        public enTestType _EnTestTypeID;
        //public int TestTypeID {  get;  set; }
        public string TypeTitle {  get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; } 

        public clsTestType()
        {
            _EnTestTypeID = enTestType.VisionTest;
            _Mode = Mode.enAdd;
        }
        private clsTestType(enTestType testTypeID, string title,string discription ,float fees)
        {
            _EnTestTypeID = testTypeID;
            TypeTitle = title;
            TestTypeDescription = discription;
            TestTypeFees = fees;
            _Mode=Mode.enUpdate;
        }

        //public enum enTestType
        //{
        //    VisionTest = 1, WrittenTest, StreetTest
        //}

        public static clsTestType Find(enTestType TypeID)
        {
            string title = "", discrbtion = ""; float fees = 0;
            if (clsTestTypesData.IsTestTypeExists((int)TypeID,ref title,ref discrbtion,ref fees))
            {
                return new clsTestType(TypeID,  title,  discrbtion,  fees);
            }
            else
            {
                return null;
            }
        }

        private bool _Update()
        {
            return clsTestTypesData.UpdateTestType((int)this._EnTestTypeID, this.TypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAdd:
                    break;
                case Mode.enUpdate:

                    return _Update();
                default:
                    break;
            }
            return false;
        }


        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestsTypes();
        }

    }
}
