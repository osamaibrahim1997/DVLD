using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }

        public float FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsReleased { set; get; }
        public DateTime ReleaseDate { set; get; }
        public int ReleasedByUserID { set; get; }
        public int ReleaseApplicationID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }
        public clsUser ReleasedByUserInfo { set; get; }

        //clsLicense _LicenseInfos;

        public clsDetainedLicense()
        {
            DetainID = -1;
            Mode = enMode.AddNew;
        }

        private clsDetainedLicense( int detainID, int licenseID, DateTime detainDate,
            float fineFees, int createdByUserID, bool isReleased, DateTime releaseDate, int releasedByUserID,
            int releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;

            CreatedByUserInfo = clsUser.Find(createdByUserID);
            ReleasedByUserInfo  = clsUser.Find(releasedByUserID);
            //_LicenseInfos = clsLicense.Find(licenseID);
            Mode = enMode.Update;
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(
                this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);

            return (this.DetainID != -1);
        }

        private bool _UpdateDetainedLicense()
        {

            return clsDetainedLicenseData.UpdateDetainedLicense(
                this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
        }

        public static clsDetainedLicense Find(int DetainID)
        {
            int LicenseID = -1; DateTime DetainDate = DateTime.Now;
            float FineFees = 0; int CreatedByUserID = -1;
            bool IsReleased = false; DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = -1; int ReleaseApplicationID = -1;

            if (clsDetainedLicenseData.GetDetainedLicenseInfoByID(DetainID,
            ref LicenseID, ref DetainDate,
            ref FineFees, ref CreatedByUserID,
            ref IsReleased, ref ReleaseDate,
            ref ReleasedByUserID, ref ReleaseApplicationID))

                return new clsDetainedLicense(DetainID,
                     LicenseID, DetainDate,
                     FineFees, CreatedByUserID,
                     IsReleased, ReleaseDate,
                     ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1; DateTime DetainDate = DateTime.Now;
            float FineFees = 0; int CreatedByUserID = -1;
            bool IsReleased = false; DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = -1; int ReleaseApplicationID = -1;

            if (clsDetainedLicenseData.GetDetainedLicenseInfoByLicenseID(LicenseID,
            ref DetainID, ref DetainDate,
            ref FineFees, ref CreatedByUserID,
            ref IsReleased, ref ReleaseDate,
            ref ReleasedByUserID, ref ReleaseApplicationID))

                return new clsDetainedLicense(DetainID,
                     LicenseID, DetainDate,
                     FineFees, CreatedByUserID,
                     IsReleased, ReleaseDate,
                     ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }


        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();

        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDetainedLicense();

            }

            return false;
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }

        private int CreateReleaseApplication()
        {
            clsApplication releaseApp = new clsApplication();

            releaseApp.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            releaseApp.ApplicationDate = DateTime.Now;
            //releaseApp.ApplicationPersonID = this._LicenseInfos.DriverInfo.PersonID;
            releaseApp.ApplicationPersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;
            releaseApp.ApplicationStatues = clsApplication.enApplicationStatue.Completed;
            releaseApp.ApplicationLastStatueDate = DateTime.Now;
            releaseApp.ApplicationPaidFees = clsApplicationsTypes.Find(releaseApp.ApplicationTypeID)._AppTypeFees;

            releaseApp.ApplicationCreatedByUserID = CreatedByUserID;

            if (!releaseApp.Save())
            {
                return -1;

            }
                return releaseApp.ApplicationID;
        }
        public bool ReleaseDetainedLicense(  )
        {
            this.ReleaseApplicationID = CreateReleaseApplication();
            if (this.ReleaseApplicationID == -1)
            {
                return false;
            }

            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID,
                   this.ReleasedByUserID, ReleaseApplicationID);
        }

        public static int DetainLicenseAndGetDetainID(int LicenseID,float FineFees, int UserId )
        {
            clsDetainedLicense _DetainedLicense = new clsDetainedLicense();
            _DetainedLicense.LicenseID = LicenseID;
            _DetainedLicense.FineFees = FineFees;
            _DetainedLicense.DetainDate = DateTime.Now;
            _DetainedLicense.CreatedByUserID = UserId ;

            if (_DetainedLicense.Save())            
                return _DetainedLicense.DetainID;            
            else            
                return -1;
        }


    }


}

