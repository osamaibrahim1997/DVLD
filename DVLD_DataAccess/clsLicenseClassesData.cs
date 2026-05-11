using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicenseClassesData
    {

        public static DataTable GetAllLicenseClassesData()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string quer = @"select LicenseClassID, ClassName  , ClassDescription, MinimumAllowedAge , DefaultValidityLength , ClassFees from LicenseClasses";

                SqlCommand command = new SqlCommand(quer, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
            }
            return dataTable;
        }


        public static bool IsLicenseClassExistsByID(int LicenseClassID, ref string ClassName,
            ref string ClassDescription, ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref float classFees)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {

                string query = @"select * from LicenseClasses where LicenseClassID = @LicenseClassID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    classFees = Convert.ToSingle(reader["ClassFees"]);
                   
                }

            }

            return isFound;

        }

        public static bool GetLicenseClassInfoByClassName(string ClassName, ref int LicenseClassID,
           ref string ClassDescription, ref byte MinimumAllowedAge,
          ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicenseClasses WHERE ClassName = @ClassName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

    }
}
