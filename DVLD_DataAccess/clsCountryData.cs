using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DVLD_DataAccess.Countries_Data
{
    public class clsCountryData
    {
        public static DataTable GetAllCountries()
        {
                DataTable CountriesDataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    string query = "select * from Countries order by CountryName";

                    SqlCommand command = new SqlCommand(query, connection);
                
                    connection.Open();
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            CountriesDataTable.Load(reader);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return CountriesDataTable;
        }


        public static bool FindCountryByID(int id, ref string CountryName) 
        {
            bool isFound;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "select * from Countries where Countries.CountryID = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id" , id);    

                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        isFound= true;

                        CountryName  = (string )reader["CountryName"];

                    }
                    else
                    {
                        isFound = false;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    isFound = false;
                }
            }

            return isFound;
        }


        public static bool FindCountryByName(ref int CountryId, string CountryName)
        {
            bool isFound;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "select * from Countries where CountryName = @CountryName";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CountryName", CountryName);

                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        isFound = true;

                        CountryId = (int)reader["CountryID"];

                    }
                    else
                    {
                        isFound = false;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    isFound = false;
                }
            }

            return isFound;
        }





    }
}
