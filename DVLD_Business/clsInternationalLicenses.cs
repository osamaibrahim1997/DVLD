using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.clsApplication;

namespace DVLD_Business
{
    public class clsInternationalLicenses : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsDriver DriverInfo;
        public int InternationalLicenseID { set; get; }
        public int DriverID { set; get; }
        public int IssuedUsingLocalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }



        public clsInternationalLicenses()
        {
            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;

            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;

            this.IsActive = true;


            Mode = enMode.AddNew;

        }

        private clsInternationalLicenses(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate,
             enApplicationStatue ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID,
             int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
        {
            base.ApplicationID = ApplicationID;
            base.ApplicationPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationStatues = ApplicationStatus;
            base.ApplicationLastStatueDate = LastStatusDate;
            base.ApplicationPaidFees = PaidFees;
            base.ApplicationCreatedByUserID = CreatedByUserID;
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;   
            this.IssuedUsingLocalLicenseID= IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.ApplicationCreatedByUserID = CreatedByUserID;  

            this.DriverInfo = clsDriver.Find(DriverID);

            Mode = enMode.Update;
        }

 
        private bool _AddNew()
        {
           
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(base.ApplicationID,
                this.DriverID, this.  IssuedUsingLocalLicenseID ,DateTime.Now ,
                this.ExpirationDate, this.IsActive,this.ApplicationCreatedByUserID   );

            if (this.InternationalLicenseID != -1)  
            {
                return true;

            }
            else
            {
                return false;
            }



        }

        private bool _Update()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicenseID,
                this.ApplicationID, this.DriverID
                , IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive 
                , ApplicationCreatedByUserID );


        }


        public bool Save()
        {
            base.mode = (clsApplication.Mode)mode;
            if (!base.Save())
            {
                return false;
            }

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())  
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _Update();
                default:
                    break;
            }
            return false;

        }

        public static clsInternationalLicenses Find(int internationalLicenseID)
        {
            int ApplictaionID = -1; int DriverID = -1; int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true; int CreatedByUserID = 1;
            if (clsInternationalLicenseData.GetInternationalLicenseInfoByID(internationalLicenseID,ref ApplictaionID,ref DriverID,
                ref IssuedUsingLocalLicenseID, ref IssueDate,ref ExpirationDate,ref IsActive,ref CreatedByUserID))
            {
                clsApplication App = clsApplication.Find(ApplictaionID);
                if (App == null)
                {
                    return null;
                }else
                {
                    return new clsInternationalLicenses(ApplictaionID, App.ApplicationPersonID, App.ApplicationDate, App.ApplicationStatues,
                        App.ApplicationLastStatueDate, App.ApplicationPaidFees, CreatedByUserID, internationalLicenseID, DriverID
                        , IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive);
                }
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();

        }

        public static DataTable GetAllInternationalLicenses(int DriverId)
        {
            return clsInternationalLicenseData.GetAllInternationalLicensesForThisDriver(DriverId);

        }



        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }

    }
}
