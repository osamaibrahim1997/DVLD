using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplicationsTypes
    {
        public enum Mode { enAdd =0 , enUpdate = 1 }
       

        public Mode _Mode {  get; set; }
        public int _AppTypeID { get; private set; }
        public string _AppTypeTitle { get;  set; }
        public float _AppTypeFees {  get;  set; }



        private clsApplicationsTypes(int appTypeID, string ppTypeTitle, float appTypeFees) 
        {
            this._AppTypeID = appTypeID;
            this._AppTypeTitle = ppTypeTitle;
            this._AppTypeFees = appTypeFees;
           this._Mode = Mode.enUpdate;

        }

        public clsApplicationsTypes()
        {
            _AppTypeID = -1;
            _AppTypeTitle = "";
            _AppTypeFees = -1;
            _Mode = Mode.enAdd;
        }

        public static clsApplicationsTypes Find(int AppTypeId)
        {
            string appTitle = ""; float appFees = 0;
            
            if (clsApplicationsTypesData.IsAppTypeExistsByIdd(AppTypeId, ref appTitle,ref appFees) )
            {
                return new clsApplicationsTypes(AppTypeId, appTitle, appFees);                
            }
            else
            {
                return null;
            }
        }

        private  bool _UpdateAppType()
        {
            return clsApplicationsTypesData.UpdateAppType(this._AppTypeID, this._AppTypeTitle, this._AppTypeFees);
        }

        public  bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAdd:
                    return true;
                case Mode.enUpdate:
                    return _UpdateAppType();
                    
                default:
                    break;
            }
            return true;
        }


        public static DataTable GetAllAppsTypes()
        {
            return clsApplicationsTypesData.GetAllAppsTypes();
        }
    }
}
