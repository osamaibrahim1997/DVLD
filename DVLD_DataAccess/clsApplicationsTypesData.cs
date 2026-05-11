using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationsTypesData
    {

        public static DataTable GetAllAppsTypes()
        {
            DataTable table = new DataTable();

            using(SqlConnection  connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string quer = "select ApplicationTypeID, ApplicationTypeTitle, ApplicationFees from ApplicationTypes";

                SqlCommand command = new SqlCommand(quer, connection);
                


                connection.Open();


                
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    table.Load(reader);
                }
            }
            return table;
        }

        public static bool IsAppTypeExistsByID(int appTypeID, ref string appTitle,  ref float appFees)
        {
                bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {

                string query = @"select ApplicationTypeID, ApplicationTypeTitle ,  
        ApplicationFees from ApplicationTypes   where ApplicationTypeID = @appTypeID ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@appTypeID", appTypeID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    appTitle = (string)reader["ApplicationTypeTitle"];
                    //discrbtion = (string)reader["TestTypeDescription"];
                    //appFees = (float)reader["ApplicationFees"];

                    appFees = (float)reader["ApplicationFees"];

                }

            }

            return isFound;
        }
        public static bool IsAppTypeExistsByIdd(int appTypeID, ref string appTitle, ref float appFees)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {

                string query = @"select ApplicationTypeID, ApplicationTypeTitle ,  
        ApplicationFees from ApplicationTypes   where ApplicationTypeID = @appTypeID ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@appTypeID", appTypeID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    appTitle = (string)reader["ApplicationTypeTitle"];
                    //discrbtion = (string)reader["TestTypeDescription"];
                    //appFees = (float)reader["ApplicationFees"];

                    appFees = Convert.ToSingle(reader["ApplicationFees"]);

                }

            }

            return isFound;
        }


        public static bool UpdateAppType(int appID, string appTitle, float appFees )
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"update ApplicationTypes set ApplicationTypeTitle = @appTitle ,
        ApplicationFees = @appFees where ApplicationTypeID = @appID";

                SqlCommand command = new SqlCommand(query,connection);

                command.Parameters.AddWithValue("@appID", appID);
                command.Parameters.AddWithValue("@appTitle", appTitle);
                command.Parameters.AddWithValue("@appFees", appFees);

                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            return rowsAffected > 0;
        }







    }
}
