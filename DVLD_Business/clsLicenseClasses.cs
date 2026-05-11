using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public  class clsLicenseClasses
    {
        public enum Mode { enAdd = 0, enUpdate = 1 }

        public Mode _Mode { get; set; }

        public int LicenseClassID { get; private set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }

        public float ClassFees { get; set; }

        public clsLicenseClasses()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;

            _Mode = Mode.enAdd;
        }

        public clsLicenseClasses(int id, string className, string classDiscription, byte minimumAllowedAge ,
            byte defaultValidtyLength , float classFees)
        {
            _Mode = Mode.enUpdate;
            LicenseClassID = id;
            ClassName = className;
            ClassDescription = classDiscription;
            MinimumAllowedAge = minimumAllowedAge;
            DefaultValidityLength = defaultValidtyLength;
            ClassFees = classFees;
        }


        public static DataTable GetAllLicensClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClassesData();
        }

        public static clsLicenseClasses Find(int classID)
        {
            string className = "" , classDiscription = "" ; byte minimumAllowedAge = 0 , DefaultValidityLength = 0;
            float classFees = 0;

            if (clsLicenseClassesData.IsLicenseClassExistsByID(classID, ref className, ref classDiscription,ref minimumAllowedAge,ref DefaultValidityLength, ref classFees))
            {
                return new clsLicenseClasses(classID, className, classDiscription , minimumAllowedAge , DefaultValidityLength, classFees);
            }
            else
            {
                return null;
            }
        }

        public static clsLicenseClasses Find(string ClassName)
        {
            int LicenseClassID = -1; string ClassDescription = "";
            byte MinimumAllowedAge = 18; byte DefaultValidityLength = 10; float ClassFees = 0;

            if (clsLicenseClassesData.GetLicenseClassInfoByClassName(ClassName, ref LicenseClassID, ref ClassDescription,
                    ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))

                return new clsLicenseClasses(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;

        }

    }
}
