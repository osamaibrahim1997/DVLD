using DVLD_DataAccess;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Business
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public enIssueReason IssueReason;
        //public clsDriver DriverInfo;
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; } 
        public int DriverID { set; get; }
        public int LicenseClassID { set; get; }

        public clsLicenseClasses LicenseClassInfo;
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public float PaidFees { set; get; }
        public bool IsActive { set; get; }
        public int CreatedByUserID { set; get; }
        public clsDriver DriverInfo;
        public clsDetainedLicense DetainedInfo;



        public clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
          DateTime IssueDate, DateTime ExpirationDate, string Notes,
          float PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)

        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.Find(this.DriverID);
            this.LicenseClassInfo = clsLicenseClasses.Find(this.LicenseClassID);
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);

            Mode = enMode.Update;
        }

        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }




        public string IssueReasonText
        {
            get
            {
                return _IsuueReasonText(this.IssueReason);
            }
        }


        private string _IsuueReasonText(enIssueReason issueReason)
        {
            switch (issueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Damaged Replacement";
                case enIssueReason.LostReplacement:
                    return "Lost Replacement";
                default:
                    return "Unknown";
            }
        }

        public static bool ISThisPersonHasThisLicense(int personID , int LicenseClassID)
        {
            return clsLicensesData.IsThisPersonHasThisLicesnseByID(personID, LicenseClassID);   
        }

        public static int GetActiveLicenseByPersonIDAndLicenseClass(int personID , int LicenseClassID)
        {
            return clsLicensesData.GetActiveLicenseIDByPersonID(personID, LicenseClassID);
        }

        private bool AddNewLicense()
        {

            this.LicenseID = clsLicensesData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);


            return (this.LicenseID != -1);
        }

        private bool UpdateLicense()
        {
            //call DataAccess Layer 

            return clsLicensesData.UpdateLicense(this.ApplicationID, this.LicenseID, this.DriverID, this.LicenseClassID,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1; int DriverID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0; bool IsActive = true; int CreatedByUserID = 1;
            byte IssueReason = 1;

            if (clsLicensesData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,
                                     PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            }
            else
                return null;

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (AddNewLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return UpdateLicense();

            }

            return false;
        }




        public static DataTable GetDriverLicensesInfos(int DriverID)
        {
            return clsLicensesData.GetAllLicenses(DriverID);
        }


        public static bool LicenseExists(int LicenseId)
        {
            return clsLicensesData.CheckIfLicenseExistsByID(LicenseId);
        }

        public static int GetLicenseIDForThisPersonByClassID(int PersonID, int LicenseClassID)
        {
            return clsLicensesData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }



        public clsLicense ReplaceLicenseForDamageOrLost(bool DamageOrLost, int CreatedByUserID)
        {

            clsApplication _application = new clsApplication();

            _application.ApplicationTypeID = (DamageOrLost)? (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense
                : (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
            _application.ApplicationDate = DateTime.Now;
            _application.ApplicationPersonID = this.DriverInfo.PersonID;
            _application.ApplicationStatues = clsApplication.enApplicationStatue.Completed;
            _application.ApplicationLastStatueDate = DateTime.Now;
            _application.ApplicationPaidFees = clsApplicationsTypes.Find(_application.ApplicationTypeID)._AppTypeFees;

            _application.ApplicationCreatedByUserID = CreatedByUserID;


            if (!_application.Save())
            {
                return null;

            }


            clsLicense _ReplacedLicense = new clsLicense();

            _ReplacedLicense.ApplicationID = _application.ApplicationID;
            _ReplacedLicense.DriverID = this.DriverID;
            _ReplacedLicense.LicenseClassID = this.LicenseClassID;
            _ReplacedLicense.IssueDate = DateTime.Now;
            _ReplacedLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            _ReplacedLicense.Notes = Notes;
            _ReplacedLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            _ReplacedLicense.IsActive = true;
            _ReplacedLicense.IssueReason = (DamageOrLost) ? clsLicense.enIssueReason.DamagedReplacement :
                clsLicense.enIssueReason.LostReplacement;


            _ReplacedLicense.CreatedByUserID = CreatedByUserID;

            if (!_ReplacedLicense.Save())
            {
                return null;
            }
            this.DeativateLicense();
            return _ReplacedLicense;
        }

        public clsLicense RenewLicense(string Notes , int CreatedByUserID)
        {
            clsApplication _application = new clsApplication();

            _application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            _application.ApplicationDate = DateTime.Now;
            _application.ApplicationPersonID = this.DriverInfo.PersonID;
            _application.ApplicationStatues = clsApplication.enApplicationStatue.Completed;
            _application.ApplicationLastStatueDate = DateTime.Now;
            _application.ApplicationPaidFees = clsApplicationsTypes.Find(_application.ApplicationTypeID)._AppTypeFees;

            _application.ApplicationCreatedByUserID = CreatedByUserID;

            if (!_application.Save())
            {
                return null;

            }

            clsLicense _RenewedLicense = new clsLicense();

            _RenewedLicense.ApplicationID = _application.ApplicationID;
            _RenewedLicense.DriverID = this.DriverID;
            _RenewedLicense.LicenseClassID = this.LicenseClassID;
            _RenewedLicense.IssueDate = DateTime.Now;
            _RenewedLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            _RenewedLicense.Notes = Notes;
            _RenewedLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            _RenewedLicense.IsActive = true;
            _RenewedLicense.IssueReason = clsLicense.enIssueReason.Renew;
            _RenewedLicense.CreatedByUserID = CreatedByUserID;

            if (!_RenewedLicense.Save())
            {
                return null;
            }
            this.DeativateLicense();
            return _RenewedLicense;
        }

        public int GetActivInternationalID()
        {
            return clsInternationalLicenses.GetActiveInternationalLicenseIDByDriverID(this.DriverID) ;
        }

        public bool DeativateLicense()
        {
            return clsLicensesData.DeactivLicenseByLicenseID(this.LicenseID);
        }

      

    }
}
