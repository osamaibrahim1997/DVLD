using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTest
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { set; get; }
        public int TestAppointmentID { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }
        public clsTestAppointment AppointmentInfo; 

      
        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);
            if (this.TestID != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateTest();
                    
                default:
                    break;
            }
            return false;   
        }

        public clsTest()
        {
            Mode = enMode.AddNew;
            TestID = -1 ;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = string.Empty;
            CreatedByUserID = -1;
        }

        private clsTest( int testID, int testAppointmentID, bool testResult, string notes,int createdByUserID)
        {
            Mode = enMode.Update;
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserID;

            AppointmentInfo = clsTestAppointment.Find(testAppointmentID);
        }

        public static clsTest Find( int testID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;
            if (clsTestData.FindByTestID(testID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))

            {
                return new clsTest(testID, TestAppointmentID, TestResult, Notes, CreatedByUserID);

            }
            else 
            { 
                return null; 
            }
        }

        public static byte GetPassedTestsByLocalDrivingLicense(int LocalDrivingLicenseID)
        {
            return clsTestData.GetPassedTestsByLocalDrivingLicenseID(LocalDrivingLicenseID);
        }

        public static clsTest FindLastTestByPersonIDAndTestTypeAndLicenseClass(int PersonID , int LicenseClassID , clsTestType.enTestType testTypeID)
        {
            
                int testID = -1; int testAppointmentID = -1; bool testResult = false;
                string notes = ""; int createdByUserID = -1;
            if (clsTestData.FindLastTestByuPersopnAndLicenseAndTestType(PersonID, LicenseClassID, (int)testTypeID, 
                ref testID,ref testAppointmentID,ref testResult ,ref notes,ref createdByUserID))
            {
               return new clsTest(testID, testAppointmentID, testResult, notes, createdByUserID);

            }
            else
            {
                return null;
            }




        }

    }
}
