using DVLD_Business.Countries;
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
    public class clsPerson
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode = enMode.AddNew;
        

        public int PersonID { get; private set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }
        public string CountryName { get; set; }

        public string FullName
        {
            get {  return FirstName + " " + SecondName + " " + LastName; }
        }
        public clsCountry Country { get; set; }
        public clsPerson()
        {
            this.PersonID = -1;

            Mode = enMode.AddNew;
        }

       

        private clsPerson(int personID, string nationalNo, string firstName,
            string secondName, string thirdName, string lastName, DateTime dateOfBirth,
            byte gender, string address, string phone, string email, int nationalityCountryID,
            string imagePath, string countryName, enMode mode)
        {
            PersonID = personID;
            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            Phone = phone;
            Email = email;
            NationalityCountryID = nationalityCountryID;
            ImagePath = imagePath;
            CountryName = countryName;
            this.Country = clsCountry.FindCountryByID(this.NationalityCountryID);

            this.Mode = enMode.Update;
        }
       

        public static clsPerson Find(int PersonID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "",
                LastName = "", Email = "", ImagePath = null, Adress = "", Phone = "";
            DateTime DateOfBirth = DateTime.Now;
            byte gender = 0;
            int nationalityCountryID = 0;
            string CountryName = "";

            if (clsPersonData.GetPersonInfoById(PersonID, ref NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref gender, ref Adress,
                ref Phone, ref Email, ref nationalityCountryID, ref ImagePath,ref CountryName))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName
                    , LastName, DateOfBirth, gender, Adress, Phone, Email,
                    nationalityCountryID, ImagePath, CountryName, enMode.Update);
            }
            else
            {
                return null;
            }
        }

        public static clsPerson Find(string NationalNo)
        {
            int PersonID = -1 ,nationalityCountryID = -1;
             string FirstName = "", SecondName = "", ThirdName = "",
                LastName = "", Email = "", ImagePath = null, Adress = "", Phone = "";
            DateTime DateOfBirth = DateTime.Now;
            byte gender = 0;
            string CountryName = "";

            if (clsPersonData.GetPersonInfoByNationalNo(ref PersonID, NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref gender, ref Adress,
                ref Phone, ref Email, ref nationalityCountryID, ref ImagePath, ref CountryName))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName
                    , LastName, DateOfBirth, gender, Adress, Phone, Email,
                    nationalityCountryID, ImagePath, CountryName, enMode.Update);
            }
            else
            {
                return null;
            }

        }
        public static clsPerson FindByDriverID(int DrivrID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "",
                LastName = "", Email = "", ImagePath = null, Adress = "", Phone = "";
            DateTime DateOfBirth = DateTime.Now;
            byte gender = 0;
            int nationalityCountryID = 0, PersonID = -1 ;
            string CountryName = "";

            if (clsPersonData.GetPersonInfoByDriverId(DrivrID, ref PersonID, ref NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref gender, ref Adress,
                ref Phone, ref Email, ref nationalityCountryID, ref ImagePath, ref CountryName))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName
                    , LastName, DateOfBirth, gender, Adress, Phone, Email,
                    nationalityCountryID, ImagePath, CountryName, enMode.Update);
            }
            else
            {
                return null;
            }
        }


        public static DataTable SearchPersonsByKeyword(string keyword)
        {
            return clsPersonData.SearchPersons(keyword);
        }


        public static DataTable GetAllPersons()
        {
            return clsPersonData.GetAllPersonsWithCountries();
        }


        public static DataTable GetAllPersonsForTheFilter(string filterType, string Value)

        {
            return clsPersonData.GetAllPersonsWithCountriesForTheFilter(filterType,Value);
        }

       
        public static bool IsPersonExistsByNationalNo(string NationalNo)
        {
            return clsPersonData.IsPersonExistsByNationalNoo(NationalNo); 
        }

        public static bool IsNationalNoUsedByAnotherPerson(string nationalNo, int personID)
        {
            return clsPersonData.IsNationalNoUsedByAnotherPerson(nationalNo, personID);
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePersonByID(PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPersonn())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdatePerson(this.PersonID);
                   
                default:
                    break;
            }
            return false;
        }

        private bool _AddNewPersonn()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo,this.FirstName,this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth
                , this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);

            return PersonID != -1;
        }
        public bool _UpdatePerson(int PersonID)
        {
            return clsPersonData.UpdatePerson(PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth
                , this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);

        }

        public static int GetPersonIdByNationalNo(string NationalNO)
        {
            return clsPersonData.GetPersonIDByNationalNo(NationalNO);
        }
    }
}
