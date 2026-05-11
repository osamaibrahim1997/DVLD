using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {

        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {

                 string query = @"Update  Applications  
                                 set 
                                     ApplicationStatus = @NewStatus, 
                                     LastStatusDate = @LastStatusDate
                                 where ApplicationID=@ApplicationID;";

                 SqlCommand command = new SqlCommand(query, connection);

                 command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                 command.Parameters.AddWithValue("@NewStatus", NewStatus);
                 command.Parameters.AddWithValue("LastStatusDate", DateTime.Now);
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }

            return (rowsAffected > 0);

        }


        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
           byte ApplicationStatus, DateTime LastStatusDate,
           float PaidFees, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"Update  Applications  
                            set ApplicantPersonID = @ApplicantPersonID,
                                ApplicationDate = @ApplicationDate,
                                ApplicationTypeID = @ApplicationTypeID,
                                ApplicationStatus = @ApplicationStatus, 
                                LastStatusDate = @LastStatusDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID=@CreatedByUserID
                            where ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("ApplicantPersonID", @ApplicantPersonID);
            command.Parameters.AddWithValue("ApplicationDate", @ApplicationDate);
            command.Parameters.AddWithValue("ApplicationTypeID", @ApplicationTypeID);
            command.Parameters.AddWithValue("ApplicationStatus", @ApplicationStatus);
            command.Parameters.AddWithValue("LastStatusDate", @LastStatusDate);
            command.Parameters.AddWithValue("PaidFees", @PaidFees);
            command.Parameters.AddWithValue("CreatedByUserID", @CreatedByUserID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }





        public static bool DeleteApplication(int ApplicationID)
        {
            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"delete Applications where ApplicationID = @ApplicationID ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }

            return RowsAffected > 0;

        }
        public static bool IsAppExistsByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "Select 1 from Applications where ApplicationID = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                object result = command.ExecuteScalar();
                return (result != null);

            }
        }

        public static float AppTypeAndFees(byte AppTypeID)
        {
            float appFees = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select ApplicationTypes.ApplicationFees from
ApplicationTypes where ApplicationTypes.ApplicationTypeID = @AppTypeID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@AppTypeID", AppTypeID);

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    appFees = (Convert.ToSingle(result));
                }

            }
            return appFees;
        }

        public static int AddNewApp(int appPersonId, DateTime appDate, int appTypeId,
            byte appStatues, DateTime appLastStatueDate, float appPaidFees, int appCreatedByUserId)
        {
            int appId = -1;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO Applications
           (ApplicantPersonID ,ApplicationDate ,ApplicationTypeID ,ApplicationStatus ,LastStatusDate ,PaidFees,CreatedByUserID)
     VALUES
           (@appPersonId,@appDate, @appTypeId , @appStatues , @appLastStatueDate,@appPaidFees ,
            @appCreatedByUserId   )  
        SELECT SCOPE_IDENTITY(); ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@appPersonId", appPersonId);
                command.Parameters.AddWithValue("@appDate", appDate);
                command.Parameters.AddWithValue("@appTypeId", appTypeId);
                command.Parameters.AddWithValue("@appStatues", appStatues);
                command.Parameters.AddWithValue("@appLastStatueDate", appLastStatueDate);
                command.Parameters.AddWithValue("@appPaidFees", appPaidFees);
                command.Parameters.AddWithValue("@appCreatedByUserId", appCreatedByUserId);


                connection .Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    appId = (Convert.ToInt32(result));
                }                
                
            }
            return appId;

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

//                string q2 = @"//IF EXISTS (//    SELECT 1//    FROM Applications A//INNER JOIN LocalDrivingLicenseApplications L
//        ON A.ApplicationID = L.ApplicationID//    WHERE//        A.ApplicantPersonID = @personId//        AND A.ApplicationTypeID = @appTypeID
//        AND A.ApplicationStatus = @appStatue//        AND L.LicenseClassID = @licenseClassID//)    SELECT 1 ELSE    SELECT null//";

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

        public static int GetActiveApplicationIDForLicenseClassData(int PersonId , int LicenseClassID, int ApplicationTypeID)
        {
            
                int ActiveApplicationID = -1;
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ApplicantPersonID", PersonId);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int ID))
                    {
                        ActiveApplicationID = ID;
                    }
                    return ActiveApplicationID;
                }
            }
        

        public static bool CancelAnApplicatioinByID(int ApplicationID)
        {

            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"update Applications set ApplicationStatus = 2 where ApplicationID = @ApplicationID";
            
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
              
                connection.Open();

                int result = -1; 

                result = command.ExecuteNonQuery();

                isFound = result != -1;

            }
            return isFound;
        }


        public static int GetApplicationIDByLocalDrivingApplication(int LDLID)
        {

            int ActiveApplicationID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select A.ApplicationID from Applications A join LocalDrivingLicenseApplications L on L.ApplicationID = A.ApplicationID
where L.LocalDrivingLicenseApplicationID = @LDLID ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LDLID", LDLID);
               

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    ActiveApplicationID = ID;
                }
                return ActiveApplicationID;
            }
        }

        public static bool GetApplicationInfoByID(int ApplicationID,
         ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
         ref byte ApplicationStatus, ref DateTime LastStatusDate,
         ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

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

                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];


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
