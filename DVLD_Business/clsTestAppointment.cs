using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { set; get; }
        public int LocalDrivingLicenseApplicationID { set; get; }
        public DateTime AppointmentDate { set; get; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }

        public clsLocalDrivingLicenseApplication ApplicationInfos;
        public int RetakeTestApplicationID { set; get; }
        public clsApplication RetakeTestAppInfo { set; get; }

        public clsTestType.enTestType TestTypeID { set; get; }

        public int TestID
        {
            get
            {
                return _GetTestID(); 
            }

        }

        public clsTestAppointment()

        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
            this.IsLocked = false;
            Mode = enMode.AddNew;

        }

        public clsTestAppointment(int TestAppointmentID, clsTestType.enTestType TestTypeID,
           int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees,
           int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)

        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestAppInfo = clsApplication.Find(RetakeTestApplicationID);
            Mode = enMode.Update;
            ApplicationInfos = clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
        }


        //public clsTestAppointment()
        //{
        //    this.TestAppointmentID = -1;

        //    this.Mode = enMode.AddNew;
        //}

        //private clsTestAppointment(int TestAppointmentId, int LocalDrivingLicenseApplicationId, DateTime appointmentDate,
        //    float paidFees , int createdByUserID, bool isLocked , int retakeTestApplicationID)
        //{
        //    this.TestAppointmentID = TestAppointmentId;
        //    this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationId;
        //    this.AppointmentDate = appointmentDate;
        //    this.PaidFees = paidFees;
        //    this.CreatedByUserID = createdByUserID;
        //    this.IsLocked = isLocked;
        //    this.RetakeTestApplicationID = retakeTestApplicationID;
        //    this.Mode = enMode.Update;


        //}

        public int GetTestIDUsingTestAppointmentID(int testAppointment)
        {
            return clsTestData.GetTestIDByTestAppointmentID(testAppointment);
        }
        private bool _AddTestAppointment()
        {
            
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees , this.CreatedByUserID , this.IsLocked, this.RetakeTestApplicationID);
            return this.TestAppointmentID != -1;
        }

        private bool _Update()
        {
            return this.TestAppointmentID != -1;
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddTestAppointment())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _Update();
                    
                default:
                    break;
            }
            return false;
        }

        public static DataTable GetAllAppointmentsGroupByTestTypeForThisApplication(int LocalAppID,  clsTestType.enTestType type)
        {
            return clsTestAppointmentData.GetAllAppointmentsForApplicationGroupByTestType(LocalAppID,(int)type);
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {

            int TestTypeID = 1; int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
            ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;
        }






        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }
    }
}
