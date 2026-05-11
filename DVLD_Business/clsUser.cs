using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsUser
    {
        public enum enMode { enAdd, enEdit }
        public enMode _Mode = enMode.enAdd;

        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }
        public int PersonId { set; get; }
        public clsPerson PersonInfo;

        public clsUser()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            this._Mode = enMode.enAdd;
        }


        private clsUser(int UserId, string userName, string password, bool isActive, int personId,
            enMode _mode)
        {
            this.UserID = UserId;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
            this.PersonId = personId;
            this.PersonInfo=clsPerson.Find(personId);

            this._Mode = enMode.enEdit;
        }

        public static clsUser Find(int UserId)
        {
            string userName = "", password = "";
            bool isActive = false;
            int personId = -1;

            if (clsUserData.GetUserInfoByUserID(UserId, ref personId,
                ref userName, ref password, ref isActive))
            {
                return new  clsUser(UserId,  userName, password, isActive,personId, enMode.enEdit);
            }
            else
            {
                return null;
            }
        }

        public static clsUser Find(string username)
        {
            string password = "";
            bool isActive = false;
            int personId = -1;
            int userID = 0;

            if (clsUserData.GetUserInfoByUsername(ref userID, ref personId, username, ref password, ref isActive))
            {
                return new  clsUser(userID, username, password, isActive,personId, enMode.enEdit);
            }
            else
            {
                return null;
            }
        }
       
        private bool _AddNewUser()
        {


            this.UserID = clsUserData.AddNewUser(this.PersonId,this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            if(clsUserData.UpdateUserByID(this.UserID, this.PersonId,
                this.UserName, this.Password, this.IsActive))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool _UpdatePassword(string newPasswod)
        {
            if (clsUserData.UpdatePassword(this.UserID, newPasswod)) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ISUserNameUsedByAnotherPerson(string Username, int personID)
        {
            return clsUserData.IsUsernameUsedByAnotherPerson(Username, personID);
        }

        public  bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAdd:
                    if (_AddNewUser())
                    {
                        this._Mode = enMode.enEdit;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enEdit:
                    return _UpdateUser();
                    
                default:
                    break;
            }
            return false;

        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool IsThisPersonAUser(int personID)
        {
            return clsUserData.isUserExistsByPersonID(personID);
        }

        public static bool FindByUsername(string username)
        {
            return clsUserData.IsUserExistsByUsername(username);
        }

        public  bool Delete(int userId)
        {
            return clsUserData.DeleteUserByUserID(userId);
        }

    }
}
