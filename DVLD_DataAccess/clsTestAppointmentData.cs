using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {

        public static bool GetTestAppointmentInfoByID(int TestAppointmentID,
          ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
          ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

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



        public static int AddNewTestAppointment(int testTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
            float PaidFees, int CreatedByUserID  , bool IsLocked, int RetakeTestApplicationID)
        {
            int ID = -1;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO TestAppointments (TestTypeID ,LocalDrivingLicenseApplicationID ,
AppointmentDate ,PaidFees ,CreatedByUserID ,IsLocked, RetakeTestApplicationID)
     VALUES
           (@testTypeID, @LocalDrivingLicenseApplicationID ,@AppointmentDate , @PaidFees , @CreatedByUserID ,@IsLocked ,@RetakeTestApplicationID );
select SCOPE_IDENTITY();";

                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@testTypeID", testTypeID);
                sqlCommand.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                sqlCommand.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                sqlCommand.Parameters.AddWithValue("@PaidFees", PaidFees);
                sqlCommand.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                sqlCommand.Parameters.AddWithValue("@IsLocked", IsLocked);
                if (RetakeTestApplicationID == -1)
                {
                    sqlCommand.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);

                }
                else
                {

                    sqlCommand.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
                }

                connection .Open();

                object result = sqlCommand.ExecuteScalar();
                if (result != null)
                {
                   ID = Convert.ToInt32(result);                        
                }

            }
            return ID;
        }





        public static DataTable GetAllAppointmentsForApplicationGroupByTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
            }
            return dataTable;
        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestID;

        }

    }
}
