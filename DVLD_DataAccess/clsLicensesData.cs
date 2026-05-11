
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicensesData
    {
        
        public static bool DeativateLicense(int licenseId)
        {
            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))

            {

                string query = @"update Licenses set IsActive = 0 where LicenseID = @licenseId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@licenseId", licenseId);
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();


            }
            return RowsAffected > 0;
        }

        public static int GetLicenseIDByPersonIdAndLicenseClassID(int PersonID, int LicenseClassID)
        {
            int LicenseId = -1;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {

                string query = @"SELECT        L.LicenseID
                            FROM Licenses L INNER JOIN
                                                     Drivers D ON L.DriverID = D.DriverID
                            WHERE  
                             
                             L.LicenseClass = @LicenseClassID 
                              AND D.PersonID = @PersonID
                              And L.IsActive=1;";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

                connection.Open();

                object obj = command.ExecuteScalar();
                if (obj != null)
                {
                    LicenseId = Convert.ToInt32(obj);
                }

            }
            return LicenseId;
        }

        public static bool GetLicenseInfoByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,
            ref float PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LicenseID", LicenseID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] == DBNull.Value)
                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }                else                {                    isFound = false;
                }           }            return isFound;        }


        public static bool CheckIfLicenseExistsByID(int licenseID)
        {
            using (SqlConnection connection =
                new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"
            SELECT 1
            WHERE EXISTS
            (
                SELECT 1
                FROM Licenses
                WHERE LicenseID = @LicenseID
            )";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LicenseID", licenseID);

                connection.Open();

                object result = command.ExecuteScalar();

                return result != null;
            }
        }

        public static int AddNewLicense(int applicationID, int DriverID
            , int licenseClassID, DateTime issueDate, DateTime expirationDate,
            string notes, float paidFees, bool isActive, byte issueReason, int createdByUserID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                int LicenseID = -1;
                string query = @"
INSERT INTO Licenses
           (ApplicationID
           ,DriverID
           ,LicenseClass
           ,IssueDate
           ,ExpirationDate
           ,Notes
           ,PaidFees
           ,IsActive
           ,IssueReason
           ,CreatedByUserID)
     VALUES
           (@applicationID
           ,@DriverID
           ,@licenseClassID
           ,@issueDate
           ,@expirationDate
           ,@notes
           ,@paidFees
           ,@isActive
           ,@issueReason
           ,@createdByUserID)
           select SCOPE_IDENTITY();
";


                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue(@"applicationID", applicationID);
                sqlCommand.Parameters.AddWithValue(@"DriverID", DriverID);
                sqlCommand.Parameters.AddWithValue(@"licenseClassID", licenseClassID);
                sqlCommand.Parameters.AddWithValue(@"issueDate", issueDate);
                sqlCommand.Parameters.AddWithValue(@"expirationDate", expirationDate);
                if (notes == "")
                {
                    sqlCommand.Parameters.AddWithValue(@"notes", DBNull.Value);

                }
                else
                {

                    sqlCommand.Parameters.AddWithValue(@"notes", notes);
                }
                sqlCommand.Parameters.AddWithValue(@"paidFees", paidFees);
                sqlCommand.Parameters.AddWithValue(@"isActive", isActive);
                sqlCommand.Parameters.AddWithValue(@"issueReason", issueReason);
                sqlCommand.Parameters.AddWithValue(@"createdByUserID", createdByUserID);

                connection.Open();

                object result = sqlCommand.ExecuteScalar();

                if (result != null)
                {
                    LicenseID = Convert.ToInt32(result);
                }
                connection.Close();


                return LicenseID;

            }

        }


        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
             DateTime IssueDate, DateTime ExpirationDate, string Notes,
             float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))

            {

                string query = @"UPDATE Licenses
                           SET ApplicationID=@ApplicationID, DriverID = @DriverID,
                              LicenseClass = @LicenseClass,
                              IssueDate = @IssueDate,
                              ExpirationDate = @ExpirationDate,
                              Notes = @Notes,
                              PaidFees = @PaidFees,
                              IsActive = @IsActive,IssueReason=@IssueReason,
                              CreatedByUserID = @CreatedByUserID
                         WHERE LicenseID=@LicenseID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

                if (Notes == "")
                {
                    command.Parameters.AddWithValue(@"Notes", DBNull.Value);
                }
                else
                {

                    command.Parameters.AddWithValue(@"Notes", DBNull.Value);
                }
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueReason", IssueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                connection.Open();
                RowsAffected = command.ExecuteNonQuery();



            }
            return RowsAffected != 0;

        }

        public static DataTable GetAllLicenses(int DriverID)
        {
            DataTable DriverLicensesTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "select * from Licenses where DriverID = @DriverID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue(@"DriverID", DriverID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DriverLicensesTable.Load(reader);
                }
                return DriverLicensesTable;
            }
        }
        public static DataTable GetDriverLocalLicenses(int DriverID)
        {
            DataTable DriverLicensesTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"
SELECT     
                           Licenses.LicenseID,
                           ApplicationID,
		                   LicenseClasses.ClassName, Licenses.IssueDate, 
		                   Licenses.ExpirationDate, Licenses.IsActive
                           FROM Licenses INNER JOIN
                                LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                            where DriverID= @DriverID
                            Order By IsActive Desc, ExpirationDate Desc

";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue(@"DriverID", DriverID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DriverLicensesTable.Load(reader);
                }
                return DriverLicensesTable;
            }
        }

        public static DataTable GetAllLicenses()
        {

            DataTable LicensesTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"SELECT * FROM Licenses";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    LicensesTable.Load(reader);
                }

            }

            return LicensesTable;

        }

        public static bool IsThisPersonHasThisLicesnseByID(int PersonID, int LicenseID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select 1 from Licenses L join Drivers D on L.DriverID = D.DriverID
where D.PersonID = @PersonID and L.LicenseClass = @LicenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@personID", PersonID);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                connection.Open();

                object result = command.ExecuteScalar();
                return (result != null);

            }
        }


        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses JOIN
                                Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";


                SqlCommand command = new SqlCommand(query, connection);


                command.Parameters.AddWithValue("@PersonID", PersonID);

                command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    LicenseID = (int)result;
                }

            }
            return LicenseID;
        }


        public static bool GetLicenseInfosByLicenseID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,
            ref float PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))

            {
                string query = @"SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] == DBNull.Value)
                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["DriverID"];

                }
                return isFound;
            }






        }


        public static bool DeactivLicenseByLicenseID(int LicenseID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Licenses
                           SET 
                              IsActive = 0
                             
                         WHERE LicenseID=@LicenseID";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                connection.Open();
                int result = command.ExecuteNonQuery();

                return result > 0;

            }

        }
    }
}
