using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.clsApplication;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum Mode { enAdd = 0, enUpdate = 1 }
        public Mode mode = Mode.enAdd;
        public clsLicenseClasses LicenseClassInfo;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LocalDLAPPID
        {
            get { return LocalDrivingLicenseApplicationID; }
        }
        public int LicenseClassID { get; set; }

        public string PersonFullName
        {
            get
            {
                return clsPerson.Find(ApplicationPersonID).FullName;
            }
        }

        public clsLocalDrivingLicenseApplication() 
        {
            this.mode = Mode.enAdd;
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;
        
        }

        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID, int appID ,
             int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatue ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int licenseClassID)
        {
            this.mode = Mode.enUpdate;
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;            
            this.LicenseClassID = licenseClassID;

            this.ApplicationID = appID;
            this.ApplicationPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatues = (enApplicationStatue)ApplicationStatus;
            this.ApplicationLastStatueDate = LastStatusDate;
            this.ApplicationPaidFees = PaidFees;
            this.ApplicationCreatedByUserID = CreatedByUserID;
            this.LicenseClassInfo = clsLicenseClasses.Find(licenseClassID);
        }

        private bool _AddNewLocalDrivingLicense()
        {
            this.LocalDrivingLicenseApplicationID =
                clsLocalDrivingLicenseApplicationsData.AddNewLocalDrivingApp(this.ApplicationID,
                this.LicenseClassID);

            return this.LocalDrivingLicenseApplicationID != -1;
        }

        public bool Save()
        {
            base.mode =(clsApplication.Mode)mode;
            if (!base.Save())
            {
                return false;
            }
            switch (mode)
            {
                case Mode.enAdd:
                   
                    if (_AddNewLocalDrivingLicense())
                    {
                        this.mode = Mode.enUpdate;
                        return true;
                    }
                    break;
                case Mode.enUpdate:
                    break;
                default:
                    break;
            }
            return false;
        }

        public bool GetLicenseIDForThisPersonIfFounded()
        {
            return GetLicenseIDForThisPerson() != -1;
        }

        public static bool DoesHasAppointments(int LDLID)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesHasAppointments(LDLID);
        }

        public  int GetLicenseIDForThisPerson()
        {
            return clsLicense.GetActiveLicenseByPersonIDAndLicenseClass(this.ApplicationPersonID, this.LicenseClassID);
        }
        public int GetActiveLicenseByPersonIDAndLicenseClass()
        {
            return clsLicense.
                GetActiveLicenseByPersonIDAndLicenseClass(this.ApplicationPersonID, this.LicenseClassID);
        }

        public static DataTable GetAllLocalDrivingLicensesApplications()
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalDrivingLicensesApplicationsData();
        }

        public static bool CheckIfThisUserHasActiveAppFromThisLicense(int personId , int typeId , byte statu
            , byte licenseClassId)
        {
            return clsLocalDrivingLicenseApplicationsData.
                CheckIfThisPersonHaveActiveAndUncompletdApplication(personId, typeId, statu, licenseClassId);
        }




        public static clsLocalDrivingLicenseApplication 
            FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            // 
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationInfoByID 
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
                clsApplication Application = clsApplication.Find(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicationPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatue)Application.ApplicationStatues, Application.ApplicationLastStatueDate,
                                     Application.ApplicationPaidFees, Application.ApplicationCreatedByUserID, LicenseClassID);
            }
            else
                return null;


        }

        public bool Delete()
        {
            bool IsLocalApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;

            IsLocalApplicationDeleted = clsLocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalApplicationDeleted)
     
                return false;


            IsBaseApplicationDeleted = base.Delete();


            return IsBaseApplicationDeleted;
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestsByLocalDrivingLicense(this.LocalDrivingLicenseApplicationID);
        }


        public bool CheckIfIsThereAnActiveScheduledTest(int TestTpe)
        {
            return clsLocalDrivingLicenseApplicationsData.
                IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, TestTpe);
        }

        public clsTest GetLastTestByTestType(clsTestType.enTestType testTypeID)
        {
            return clsTest.FindLastTestByPersonIDAndTestTypeAndLicenseClass(this.ApplicationPersonID, 
                this.LicenseClassID, testTypeID);
        } 

        public bool DoseAttendTestType(clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationsData.
                CheckIfThisPersonAttendTestByTestType((int)testType,this.LocalDrivingLicenseApplicationID);
        }
        public byte GetTotalTrialsTestsOnThisTestType(clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationsData.
                GetTotalTrialsTestsOnThisTestType(this.LocalDrivingLicenseApplicationID, (int)testType); 
        }

        public bool DoesPersonPassThisTest(clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesPassThisTest( (int)testType, this.LocalDrivingLicenseApplicationID) ;
        }

        private int GetDriverIDIfFound()
        {
            return clsDriverData.GetDriverIDIfFoundedByPersonID(this.ApplicationPersonID);    
        }

        public int RenewLicensAndGetNewLicenseID(string notes, int UserID)
        {
            return 0;
        }

        public int IssueLicenseForFirstTime(string notes, int UserID)
        {
            int DriverId = GetDriverIDIfFound();
            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicationPersonID);
            if (Driver == null)
            {
                 Driver = new clsDriver();

                Driver.CreatedByUserID = UserID;
                Driver.CreatedDate = DateTime.Now;
                Driver.PersonID = this.ApplicationPersonID;

                if (!Driver.Save())
                {
                    return -1;
                }
                else
                {
                    DriverId = Driver.DriverID;
                }

            }
            else
            {
                DriverId = Driver.DriverID;
            }
        
            clsLicense License = new clsLicense();
            License.CreatedByUserID = UserID;
            License.Notes = notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.LicenseClassID = this.LicenseClassInfo.LicenseClassID;
            License.DriverID = DriverId;
            License.IsActive = true;
            License.ApplicationID = this.ApplicationID;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.IssueDate = DateTime.Now;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;

            if (License.Save())
            {
                this.SetComplete();
                return License.LicenseID;

            }
            else
            {
                return -1;
            }



        }



        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        { 
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicationPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatue)Application.ApplicationStatues, Application.ApplicationLastStatueDate,
                                     Application.ApplicationPaidFees, Application.ApplicationCreatedByUserID, LicenseClassID);
            }
            else
                return null;


        }







    }
}
