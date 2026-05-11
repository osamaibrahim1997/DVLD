using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.clsApplication;

namespace DVLD_Business
{
    public  class clsApplication
    {
        public enum enApplicationStatue { New = 1, Completed = 2, Cancled = 3 }
        public enApplicationStatue ApplicationStatues { get; set; }

        public enum Mode { enAdd = 0, enUpdate = 1 }
        public Mode mode = Mode.enAdd;

        public enum enApplicationType
        {

                  NewLocalDrivingLicenseService = 1,
            RenewDrivingLicense = 2,
            ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4,
            ReleaseDetainedDrivingLicsense = 5,
            NewInternationalLicense = 6,
            RetakeTest = 7
        }


        public int ApplicationID { get;  set; }
        public int ApplicationPersonID { get; set; }
        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(ApplicationPersonID).FullName;
            }
        }
        public DateTime ApplicationDate { get; set; }
        public DateTime ApplicationLastStatueDate { get; set; }
        public int ApplicationTypeID { get; set; }

        public clsApplicationsTypes ApplicationTypeInfo;

        public string StatuesText
        {
            get
            {
                switch (ApplicationStatues)
                {
                    case enApplicationStatue.New:
                        return "New";

                    case enApplicationStatue.Completed:
                        return "Completed";

                    case enApplicationStatue.Cancled:
                        return "Cancled";

                    default:
                        return "Unknown";
                }
            }
        }

        public float ApplicationPaidFees { get;  set; }
        public int ApplicationCreatedByUserID { get; set; }

        public clsUser CreatedByUserInfo;


        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicationPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationLastStatueDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationPaidFees = 0;
            this.ApplicationStatues = enApplicationStatue.New;
            this.mode  = Mode.enAdd;
        }
   
        private clsApplication(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatue ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicationPersonID= ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatues= ApplicationStatus;
            this.ApplicationLastStatueDate= LastStatusDate;
            this.ApplicationPaidFees= PaidFees;
            this.ApplicationCreatedByUserID = CreatedByUserID;
            this.ApplicationTypeInfo = clsApplicationsTypes.Find(ApplicationTypeID);
            this.CreatedByUserInfo = clsUser.Find(CreatedByUserID);
            this.mode = Mode.enUpdate;
        }

        private bool _AddApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApp(this.ApplicationPersonID, this.ApplicationDate, this.ApplicationTypeID
                , (byte)this.ApplicationStatues, this.ApplicationLastStatueDate, this.ApplicationPaidFees, this.ApplicationCreatedByUserID);

            return this.ApplicationID != -1;
        }


        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            byte ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0; int CreatedByUserID = -1;

            bool IsFound = clsApplicationData.GetApplicationInfoByID
                                (
                                    ApplicationID, ref ApplicantPersonID,
                                    ref ApplicationDate, ref ApplicationTypeID,
                                    ref ApplicationStatus, ref LastStatusDate,
                                    ref PaidFees, ref CreatedByUserID
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsApplication(ApplicationID, ApplicantPersonID,
                                     ApplicationDate, ApplicationTypeID,
                                    (enApplicationStatue)ApplicationStatus, LastStatusDate,
                                     PaidFees, CreatedByUserID);
            else
                return null;
        }








        public bool SetComplete()

        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);
        }
        private bool _UpdateApplication()
        {

            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicationPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatues,
                this.ApplicationLastStatueDate, this.ApplicationPaidFees, this.ApplicationCreatedByUserID);

        }
        public bool Save()
        {
            switch (mode)
            {
                case Mode.enAdd:
                    if (_AddApplication())
                    {
                        this.mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                   
                case Mode.enUpdate:

                    return _UpdateApplication();

            }
            return false;
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }
        public bool CheckIfThisPersonHaveActiveAndUncompletdApp(int PersonId, byte licenseClassID)
        {
            return clsApplicationData.CheckIfThisPersonHaveActiveAndUncompletdApplication(PersonId,
                this.ApplicationTypeID, Convert.ToByte(enApplicationStatue.New), licenseClassID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID,
              clsApplication.enApplicationType type, int LicenseClassId)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClassData(PersonID, LicenseClassId, (int)type);
        }

        public static bool CancelAnApplication(int ApplicationID)
        {
            return clsApplicationData.CancelAnApplicatioinByID(ApplicationID);
        }


        public static int GetApplicationIDByLocalDrivingApplication(int LDLA)
        {
            return clsApplicationData.GetApplicationIDByLocalDrivingApplication(LDLA);
        }

        public static clsApplication Find(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            byte ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0; int CreatedByUserID = -1;

            bool IsFound = clsApplicationData.GetApplicationInfoByID
                                (
                                    ApplicationID, ref ApplicantPersonID,
                                    ref ApplicationDate, ref ApplicationTypeID,
                                    ref ApplicationStatus, ref LastStatusDate,
                                    ref PaidFees, ref CreatedByUserID
                                );

            if (IsFound)
               
                return new clsApplication(ApplicationID, ApplicantPersonID,
                                     ApplicationDate, ApplicationTypeID,
                                    (enApplicationStatue)ApplicationStatus, LastStatusDate,
                                     PaidFees, CreatedByUserID);
            else
                return null;
        }














        enApplicationType appType;


        public float AppFees()
        {
            byte appTypeID = 0;
            switch (appType)
            {
                case enApplicationType.NewLocalDrivingLicenseService:
                    appTypeID = 1;
                    break;
                case enApplicationType.RenewDrivingLicense:
                    appTypeID = 2;
                    break;
                case enApplicationType.ReplaceLostDrivingLicense:
                    appTypeID = 3;
                    break;
                case enApplicationType.ReplaceDamagedDrivingLicense:
                    appTypeID = 4;
                    break;
                case enApplicationType.ReleaseDetainedDrivingLicsense:
                    appTypeID = 5;
                    break;
                case enApplicationType.NewInternationalLicense:
                    appTypeID = 6;
                    break;
                case enApplicationType.RetakeTest:
                    appTypeID = 7;
                    break;
                default:
                    break;
            }
            return clsApplicationData.AppTypeAndFees(appTypeID);
        }

    }
}
