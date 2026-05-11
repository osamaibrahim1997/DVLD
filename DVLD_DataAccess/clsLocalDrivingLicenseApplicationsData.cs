using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationsData
    {

        public static bool GetLocalDrivingLicenseApplicationInfoByApplicationID(
      int ApplicationID, ref int LocalDrivingLicenseApplicationID,
      ref int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];

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


        public static bool IsLocalDrivingLicenseAppExistsByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "Select 1 from LocalDrivingLicenseApplications where ApplicationID = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                object result = command.ExecuteScalar();
                return (result != null);

            }
        }


        public static bool DoesPassThisTest(int testTypeID, int LocalDLAID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"
select top 1 1 from Tests T join TestAppointments TA on T.TestAppointmentID = TA.TestAppointmentID where 
TA.LocalDrivingLicenseApplicationID = @LocalDLAID and
TA.TestTypeID = @testTypeID and
T.TestResult = 1 ";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDLAID", LocalDLAID);
                command.Parameters.AddWithValue("@testTypeID", testTypeID);
                connection.Open();

                object result = command.ExecuteScalar();
                return (result != null);

            }
        }

        public static int AddNewLocalDrivingApp(int appID, int licenseClassID)
        {
            int appId = -1;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO LocalDrivingLicenseApplications
           (ApplicationID ,LicenseClassID)
     VALUES
           (@appID, @licenseClassID )  
        SELECT SCOPE_IDENTITY(); ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@appID", appID);
                command.Parameters.AddWithValue("@licenseClassID", licenseClassID);


                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    appId = (Convert.ToInt32(result));
                }

            }
            return appId;

        }

        public static bool DeleteLocalDrivingLicenseApplication(int LocalaDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"Delete LocalDrivingLicenseApplications 
                                where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalaDrivingLicenseApplicationID);

                connection.Open();
              rowsAffected = command.ExecuteNonQuery();

            }

            return rowsAffected > 0;
        }

        public static bool DeleteLocalDrivingLicenseApplication2(int localDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    string query = @"DELETE LocalDrivingLicenseApplications
                             WHERE LocalDrivingLicenseApplicationID = @ID";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ID", localDrivingLicenseApplicationID);

                    connection.Open();

                    rowsAffected = command.ExecuteNonQuery();
                }

                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                // 547 = Foreign Key Constraint Error
                if (ex.Number == 547)
                {
                    MessageBox.Show(
                        "لا يمكن حذف الطلب لأنه مرتبط بمواعيد أو اختبارات.",
                        "Delete Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        ex.Message,
                        "Database Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unexpected Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        public static bool DoesHasAppointments(int LDLAID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select 1 where exists (select   t.LocalDrivingLicenseApplicationID from TestAppointments T 
where T.LocalDrivingLicenseApplicationID = @LDLAID) ; ";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LDLAID", LDLAID);

                connection.Open();

                object result = command.ExecuteScalar();
                return (result != null);

            }
        }





        public static byte GetTotalTrialsTestsOnThisTestType(int LocalDrivingLicenseApplicationID , int TestType)
        {
            byte totalTrials = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select COUNT(T.TestID) as Trial from Tests T join TestAppointments TA on
T.TestAppointmentID = TA.TestAppointmentID join LocalDrivingLicenseApplications L
on L.LocalDrivingLicenseApplicationID = TA.LocalDrivingLicenseApplicationID where
TA.TestTypeID =@TestType and L.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestType", TestType);


                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    totalTrials = (Convert.ToByte(result));
                }
            }
            return totalTrials;

        }
        public static DataTable GetAllLocalDrivingLicensesApplicationsData()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string GetAllLocalApplicationsQuery = "select * from LocalDrivingLicenseApplications_View";

                SqlCommand command = new SqlCommand(GetAllLocalApplicationsQuery, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable;
        }



        public static bool CheckIfThisPersonAttendTestByTestType(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))   
            {
                
                string query = @"select top 1 1
from Tests T
join TestAppointments TA 
    on T.TestAppointmentID = TA.TestAppointmentID
where TA.TestTypeID = @TestTypeID
and TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                connection.Open();

                object result = command.ExecuteScalar();
                return (result != null);

            }

        }
        public static bool CheckIfThisPersonHaveActiveAndUncompletdApplication(int personId,
           int appTypeID, byte appStatue, byte licenseClassID)
        {

            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select 1 from LocalDrivingLicenseApplications L
join Applications A on L.ApplicationID = A.ApplicationID
where
A.ApplicantPersonID = @personId And 
A.ApplicationStatus = @appStatue And
L.LicenseClassID =  @licenseClassID and
A.ApplicationTypeID = @appTypeID ";

              

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@personId", personId);
                command.Parameters.AddWithValue("@appTypeID", appTypeID);
                command.Parameters.AddWithValue("@appStatue", appStatue);
                command.Parameters.AddWithValue("@licenseClassID", licenseClassID);

                connection.Open();

                object result = command.ExecuteScalar();



                isFound = result != null;

            }
            return isFound;


        }


        public static bool GetLocalDrivingLicenseApplicationInfoByID(
         int LocalDrivingLicenseApplicationID, ref int ApplicationID,
         ref int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);


            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];



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


        public static bool IsThereAnActiveScheduledTest(int AppID, int TestTypeID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"SELECT TOP 1 1
                 FROM TestAppointments
                 WHERE LocalDrivingLicenseApplicationID = @AppID
                 AND TestTypeID = @TestTypeID
                 AND IsLocked = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppID", SqlDbType.Int).Value = AppID;
                    command.Parameters.AddWithValue("@TestTypeID", SqlDbType.Int).Value = TestTypeID;

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        isFound = true;
                    }


                }

                return isFound;
            }
        }




        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
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


            return LicenseID;
        }


    }
}
