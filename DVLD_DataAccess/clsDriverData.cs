using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDriverData
    {

        public static bool IsDriverHasActiveLicense()
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {

                string query = @"";

                SqlCommand command = new SqlCommand(query, connection);

                //command.Parameters.AddWithValue("@appTypeID", appTypeID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

            

                }

            }

            return isFound;
        }

        public static bool FindDriverByID(int driverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreateTime)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @" select * from Drivers where DriverID = @driverID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@driverID", driverID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreateTime = Convert.ToDateTime(reader["CreatedDate"]);

                }


            }
            return isFound;
            
        }

        public static DataTable GetAllDrivers()
        {
                DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {          
                string query = "SELECT * FROM Drivers_View order by FullName";

                SqlCommand command = new SqlCommand(query, connection);

           
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }          

            return dt;
        }

        public static int GetDriverIDIfFoundedByPersonID(int PersonID)
        {
            int driverID = -1;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select top 1 D.DriverID  from Drivers D where D.PersonID = @PersonID";


                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue(@"PersonID", PersonID);

                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    driverID = Convert.ToInt32(result);
                }

            }
            return driverID;
        }

        public static int AddNewDriver(int PersonId, int CreatedByUserID, DateTime CreatedDateTime)
        {
            int driverID = -1;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"Insert Into Drivers (PersonID,CreatedByUserID,CreatedDate)
                            Values (@PersonId,@CreatedByUserID,@CreatedDateTime);
                          
                            SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand (query, connection);
                command.Parameters.AddWithValue(@"PersonId", PersonId);
                command.Parameters.AddWithValue(@"CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue(@"CreatedDateTime", CreatedDateTime);

                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    driverID =  Convert.ToInt32(result);
                }


            }
            return driverID;
        }

        public static bool UpdateDriver(int driverID, int PersonID, int CreatedByUserID)
        {
            int rowsAffected;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"Update  Drivers  
                            set PersonID = @PersonID,
                                CreatedByUserID = @CreatedByUserID
                                where DriverID = @driverID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue(@"driverID", driverID);
                command.Parameters.AddWithValue(@"PersonID", PersonID);
                command.Parameters.AddWithValue(@"CreatedByUserID", CreatedByUserID);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }






            
        }


        public static bool GetDriverInfoByPersonID(int PersonID, ref int DriverID,
           ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {           

                string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@PersonID", PersonID);

               
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];

                }
                else
                {
                    isFound = false;
                }
              

            }
            return isFound;
        }



    }
}
