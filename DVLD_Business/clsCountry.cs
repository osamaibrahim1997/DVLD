using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess.Countries_Data;
using System.Data.SqlTypes;
using System.Reflection.Emit;

namespace DVLD_Business.Countries
{
    public class clsCountry
    {


        public string CountryName { get; set; }
        public int CountryID { get; set; }

        public clsCountry()
        {
            this.CountryID = -1;
            this.CountryName = "";
        }
        private clsCountry(int ID , string countryName) 
        {
            CountryID = ID;
            CountryName = countryName;
        }   

        public static DataTable LaodCoutriesFromDataBase()
        {
            DataTable dataTable = clsCountryData.GetAllCountries();
            return dataTable;
        }

        
        public static clsCountry FindCountryByID(int CountryID)
        {
            string CountryName = "";
            bool isFound = clsCountryData.FindCountryByID(CountryID, ref CountryName); 

            if (isFound)
            {
                return new clsCountry(CountryID, CountryName);    
            }
            else
            { 
                return null;
            }
        }



        public static clsCountry FindCountryByName(string CountryName)
        {
            int CountryID = -1;
            bool isFound = clsCountryData.FindCountryByName(ref CountryID,  CountryName); 

            if (isFound)
            {
                return new clsCountry(CountryID, CountryName);    
            }
            else
            { 
                return null;
            }
        }





    }
}
