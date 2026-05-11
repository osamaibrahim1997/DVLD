using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_DataAccess
{
    public class clsTestTypesData
    {

        public static DataTable GetAllTestsTypes()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string quer = @"select TestTypeID, TestTypeTitle , TestTypeDescription , TestTypeFees  from TestTypes";

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

        public static bool IsTestTypeExists(int testTypeID, ref string title,
            ref string discription, ref float fees)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select TestTypeID, TestTypeTitle , TestTypeDescription ,
    TestTypeFees  from TestTypes where TestTypeID = @testTypeID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@testTypeID", testTypeID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    title = (string)reader["TestTypeTitle"];
                    discription = (string)reader["TestTypeDescription"];
                    
                    fees = Convert.ToSingle(reader["TestTypeFees"]);

                }

            }
            return isFound;

        }



        public static bool UpdateTestType(int testTypeID, string title,
             string discription,  float fees)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"update TestTypes set TestTypeTitle = @title , TestTypeDescription = @discription ,
TestTypeFees =@fees where TestTypeID = @testTypeID ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@testTypeID", testTypeID);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@discription", discription);
                command.Parameters.AddWithValue("@fees", fees);

                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            return rowsAffected > 0;
        }




    }
}
