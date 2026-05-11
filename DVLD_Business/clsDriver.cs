using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace DVLD_Business
{
    public  class clsDriver
    {
        public int DriverID { set; get; }
        public int PersonID {  set; get; }
        public int CreatedByUserID {  set; get; }
        public DateTime CreatedDate { set; get; }
        public clsPerson PersonInfo;
        public enum Mode { enAdd = 0, enUpdate = 1 }
        public Mode mode = Mode.enAdd;


        private bool AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            if (this.DriverID != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool _UpdateDriver()
        {
            //call DataAccess Layer 

            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (mode)
            {
                case Mode.enAdd:
                    if (AddNewDriver())
                    {
                        mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateDriver();
                default:
                    break;
            }
            return false;

        }



        public clsDriver() 
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            mode = Mode.enAdd;
        
        }

        private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            this.DriverID = driverID;
            this.PersonID = personID;
            this.CreatedByUserID = createdByUserID;
            this.CreatedDate = createdDate;

            PersonInfo = clsPerson.FindByDriverID(this.DriverID);

            mode = Mode.enUpdate;
        }


        public static clsDriver Find(int driverID)
        {
            int PersonID = -1, CreatedByUserID = -1; DateTime CreateTime = DateTime.Now;
            if (clsDriverData.FindDriverByID(driverID, ref PersonID, ref CreatedByUserID , ref CreateTime ))
            {

                return new clsDriver(driverID ,  PersonID, CreatedByUserID, CreateTime);
            }
            else
            {
                return null;
            }
        }

        public static clsDriver FindByPersonID(int PersonID)
        {

            int DriverID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (clsDriverData.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))

                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }

        public static bool IsDriverHasTheLicense()
        {
            return false;
        }


        public static DataTable GetAllDriversList()
        {
            return clsDriverData.GetAllDrivers();
        }


        public  DataTable GetDriverLocalLicensesData()
        {
            return clsLicense.GetDriverLicensesInfos(this.DriverID);
        }

        public  DataTable GetDriverInternationallLicensesData()
        {
            return clsInternationalLicenses.GetAllInternationalLicenses(this.DriverID);
        }

    }
}
