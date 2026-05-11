using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestData
    {
        public static int GetTestIDByTestAppointmentID(int TestAppointmentID)
        {
                int testID = -1;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString)) 
            {
                string query = "select TestID from Tests where Tests.TestAppointmentID = @TestAppointmentID";
                SqlCommand command = new SqlCommand (query, connection);
                command.Parameters.AddWithValue(@"TestAppointmentID", TestAppointmentID);
                connection .Open ();
                object result = command.ExecuteScalar();
                if (result  != null)
                {
                    testID = (int)result;
                }
                
            }
            return testID;
        }
        public static bool FindLastTestByuPersopnAndLicenseAndTestType(int PersonID , int LicenseClassID ,
            int TestTypID ,ref int testID, ref int testAppointmentID,
            ref bool testResult, ref string notes , ref int createdByUserID) 
        {
            bool isFound = false;

            using (SqlConnection connectio = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select top 1 T.TestID , T.TestAppointmentID , T.CreatedByUserID ,
            T.Notes , T.TestResult , A.ApplicantPersonID
            from Tests T join TestAppointments TA on T.TestAppointmentID = TA.TestAppointmentID
            join LocalDrivingLicenseApplications L on TA.LocalDrivingLicenseApplicationID =
            L.LocalDrivingLicenseApplicationID join Applications A on L.ApplicationID = A.ApplicationID
            join TestTypes TT on TA.TestTypeID = TT.TestTypeID
            where TT.TestTypeID = @TestTypID and L.LicenseClassID = @LicenseClassID and 
    A.ApplicantPersonID = @PersonID order by TA.TestAppointmentID desc;";

               

                SqlCommand command = new SqlCommand(query, connectio);

                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                command.Parameters.AddWithValue("@TestTypID", TestTypID);

                connectio.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    testID = (int)reader["TestID"];
                    testAppointmentID = (int)reader["TestAppointmentID"];
                    if (reader["Notes"] != DBNull.Value)
                    {
                        notes = (string)reader["Notes"];
                    }
                    else
                    {
                        notes = "";
                    }
                    testResult = (bool)reader["TestResult"];
                    createdByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();

            }
           return isFound;
        }


        public static byte GetPassedTestsByLocalDrivingLicenseID(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {

                string query = @"select COUNT(*) as PassedTests from Tests T join TestAppointments TS on
        T.TestAppointmentID = TS.TestAppointmentID
    where TS.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID And T.TestResult  =1";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
                }                

                return PassedTestCount;
            }
        }




        public static bool FindByTestID(int testID ,ref int testAppointmentID ,ref bool testResult
            ,ref string Notes ,ref int CreatedByUserID)            
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))   
            {
                string query = "SELECT * FROM Tests WHERE TestID = @testID";
                SqlCommand Command = new SqlCommand(query, connection);
                Command.Parameters.AddWithValue("@testID", testID);

                connection . Open();

                SqlDataReader reader = Command.ExecuteReader();                                    
                    
                if (reader.Read())                    
                {
                    result = true;
                    testAppointmentID = (int)reader["TestAppointmentID"];
                    testResult = (bool)reader["TestResult"];
                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];
                    }
                    Notes = "";

                    CreatedByUserID = (int)reader["CreatedByUserID"];

             
                }
                reader.Close();                

            }
            return result;
        }


        public static int AddNewTest(   int testAppointmentID , bool testResult, string notes,
            int CreatedByUserId)
        {
            int testID = -1;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"
INSERT INTO Tests
           (TestAppointmentID
           ,TestResult
           ,Notes
           ,CreatedByUserID)
     VALUES
           (@testAppointmentID,@testResult ,@notes ,@CreatedByUserId   );
SELECT SCOPE_IDENTITY();
update TestAppointments
set IsLocked = 1  where TestAppointmentID = @testAppointmentID
";
                SqlCommand Command = new SqlCommand(query, connection);
                Command.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);
                Command.Parameters.AddWithValue("@testResult", testResult);
                if (notes == null)
                {
                    Command.Parameters.AddWithValue("@notes", DBNull.Value);

                }
                else
                {

                    Command.Parameters.AddWithValue("@notes", notes);
                }

                Command.Parameters.AddWithValue("@CreatedByUserId", CreatedByUserId);


                connection . Open();

                object result = Command.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    testID = Convert.ToInt32(result);
                }
            }
           return testID;
        }


        public static bool UpdateTest(int testID, int testAppointmentID, bool testResult, string notes,
            int CreatedByUserId)
        {
                int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                

                string query = @"UPDATE Tests
   SET TestAppointmentID = @testAppointmentID
      ,TestResult =@testResult
      ,Notes =@notes
      ,CreatedByUserID =@CreatedByUserId
 WHERE TestID = @testID
";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);
                command.Parameters.AddWithValue("@testResult", testResult);
                command.Parameters.AddWithValue("@notes", notes);
                command.Parameters.AddWithValue("@CreatedByUserId", CreatedByUserId);
                command.Parameters.AddWithValue("@testID", testID);

                connection . Open();
                
                rowsAffected = command.ExecuteNonQuery();
            }

            return rowsAffected > 0;
       
        
        }


        
    }
}
