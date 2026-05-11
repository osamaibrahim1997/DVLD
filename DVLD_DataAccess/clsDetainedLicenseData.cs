using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDetainedLicenseData
    {



        public static bool GetDetainedLicenseInfoByID(int DetainID,
        ref int LicenseID, ref DateTime DetainDate,
        ref float FineFees, ref int CreatedByUserID,
        ref bool IsReleased, ref DateTime ReleaseDate,
        ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
           
            bool isFound = false;
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {

                    string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@DetainID", DetainID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                

                    if (reader.Read())
                    {

                        isFound = true;

                        LicenseID = (int)reader["LicenseID"];
                        DetainDate = (DateTime)reader["DetainDate"];
                        FineFees = Convert.ToSingle(reader["FineFees"]);
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        IsReleased = (bool)reader["IsReleased"];

                        if (reader["ReleaseDate"] == DBNull.Value)

                            ReleaseDate = DateTime.MaxValue;
                        else
                            ReleaseDate = (DateTime)reader["ReleaseDate"];


                        if (reader["ReleasedByUserID"] == DBNull.Value)

                            ReleasedByUserID = -1;
                        else
                            ReleasedByUserID = (int)reader["ReleasedByUserID"];

                        if (reader["ReleaseApplicationID"] == DBNull.Value)

                            ReleaseApplicationID = -1;
                        else
                            ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

                    }
                    else
                    {

                        isFound = false;
                    }
                }               


            }
            catch (Exception ex)
            {
               
                isFound = false;
            }
            

            return isFound;
        }


        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID,
      ref int DetainID, ref DateTime DetainDate,
      ref float FineFees, ref int CreatedByUserID,
      ref bool IsReleased, ref DateTime ReleaseDate,
      ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "SELECT top 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID order by DetainID desc";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                try
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {

                            isFound = true;

                            DetainID = (int)reader["DetainID"];
                            DetainDate = (DateTime)reader["DetainDate"];
                            FineFees = Convert.ToSingle(reader["FineFees"]);
                            CreatedByUserID = (int)reader["CreatedByUserID"];

                            IsReleased = (bool)reader["IsReleased"];

                            if (reader["ReleaseDate"] == DBNull.Value)

                                ReleaseDate = DateTime.MaxValue;
                            else
                                ReleaseDate = (DateTime)reader["ReleaseDate"];


                            if (reader["ReleasedByUserID"] == DBNull.Value)

                                ReleasedByUserID = -1;
                            else
                                ReleasedByUserID = (int)reader["ReleasedByUserID"];

                            if (reader["ReleaseApplicationID"] == DBNull.Value)

                                ReleaseApplicationID = -1;
                            else
                                ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

                        }
                        else
                        {
                            isFound = false;
                        }

                    

                    }

                }
                catch (Exception ex)
                {
                    isFound = false;
                }

            }
            return isFound;
        }


        public static DataTable GetAllDetainedLicenses()
        {

            DataTable dt = new DataTable();
            

            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {


                    string query = "select * from detainedLicenses_View order by IsReleased ,DetainID;";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)

                        {
                            dt.Load(reader);
                        }
                    }
                }



            }

            catch (Exception ex)
            {
            }
         

            return dt;

        }

        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate,float FineFees, int CreatedByUserID)
        {
            int DetainedLicenseID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    string query = @"INSERT INTO dbo.DetainedLicenses
                              (LicenseID, DetainDate, FineFees, CreatedByUserID,IsReleased)
                           VALUES
                              (@LicenseID, @DetainDate,  @FineFees,  @CreatedByUserID,0);
SELECT SCOPE_IDENTITY();";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@LicenseID", LicenseID);
                    command.Parameters.AddWithValue("@DetainDate", DetainDate);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@FineFees", FineFees);
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        DetainedLicenseID = Convert.ToInt32(result);
                    }
                }
            }
            catch 
            {
                return -1;
                //throw new Exception("Error while adding detained license.", ex);
            }
            return DetainedLicenseID;
        
        }

        public static bool UpdateDetainedLicense(int DetainID,
           int LicenseID, DateTime DetainDate,
           float FineFees, int CreatedByUserID)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE dbo.DetainedLicenses
                              SET LicenseID = @LicenseID, 
                              DetainDate = @DetainDate, 
                              FineFees = @FineFees,
                              CreatedByUserID = @CreatedByUserID,   
                              WHERE DetainID=@DetainID;";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@DetainedLicenseID", DetainID);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                command.Parameters.AddWithValue("@FineFees", FineFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                connection.Open();
                try
                {
                    rowsAffected = command.ExecuteNonQuery();

                }
                catch (Exception)
                {

                    return false;
                }
            }

            return (rowsAffected > 0);
        }


        public static bool IsLicenseDetained(int licenseID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select 1 where exists(select 1 from 
DetainedLicenses where LicenseID = @licenseID and IsReleased = 0)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LicenseID", licenseID);

                connection.Open();
                object result = command.ExecuteScalar();
                isFound = result != null;

            }
            return isFound;

        }


        public static bool ReleaseDetainedLicense(int DetainID,
             int ReleasedByUserID, int ReleaseApplicationID)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE dbo.DetainedLicenses
                                  SET IsReleased = 1, 
                                  ReleaseDate = @ReleaseDate, 
                                  ReleaseApplicationID = @ReleaseApplicationID   
                                  WHERE DetainID=@DetainID;";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@DetainID", DetainID);
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                
                    return false;
                }
          
            }

            return (rowsAffected > 0);
        }




    }
}
