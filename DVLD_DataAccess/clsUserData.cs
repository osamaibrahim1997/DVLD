using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD_DataAccess
{
    public class clsUserData
    {

        public static int AddNewUser(int personId, string userName, 
            string passWord, bool isActive  )
        {
            int UserId = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"
INSERT INTO Users ( PersonID ,UserName ,Password ,IsActive)
     VALUES  (@personId, @userName, @passWord,@isActive) select SCOPE_IDENTITY()  ";

                SqlCommand Command = new SqlCommand(query, connection);
                Command.Parameters.AddWithValue("@personId", personId);
                Command.Parameters.AddWithValue("@userName", userName);
                Command.Parameters.AddWithValue("@passWord", passWord);
                Command.Parameters.AddWithValue("@isActive", isActive);

                connection.Open();
                object obj = Command.ExecuteScalar();
                if (obj != null)
                {
                    UserId = Convert.ToInt32(obj);
                }
                return UserId;

            }
        }


        

        public static bool DeleteUserByUserID(int userID)
        {
            int rowsAffected;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"DELETE FROM Users WHERE Users.UserID = @userID ";

                SqlCommand Command = new SqlCommand(query, connection);
                Command.Parameters.AddWithValue("@userID", userID);
                

                connection.Open();

                try
                {
                    rowsAffected = Command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    return false;
                    throw;
                }
                
                                 

            }
            return( rowsAffected > 0);
        }

        public static bool UpdatePassword(int userID, string password)
        {
            int rowsAffected;

            using (SqlConnection connection = new 
                SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Users SET Password = @password
                                WHERE Users.UserID = @userID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }

            return (rowsAffected > 0);
        }

        public static bool UpdateUserByID(int userID, int personId, string userName,
            string passWord, bool isActive)
        {
            int rowsAffected;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"
UPDATE Users
   SET PersonID = @personId
      ,UserName = @userName
      ,Password = @passWord
      ,IsActive = @isActive
 WHERE UserID = @userID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@personId", personId);
                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@passWord", passWord);
                command.Parameters.AddWithValue("@isActive", isActive);


                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            return( rowsAffected > 0);
            
        }



        public static bool GetUserInfoByUserID(int userID,
            ref int personID, ref string userName,ref string passWord,ref bool isActive)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString) )  
            {
                string query = @"select * from Users where UserID = @userID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userID", userID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    userName = (string)reader["UserName"];
                    passWord = (string)reader["Password"];
                    isActive = (bool)reader["IsActive"];
                    personID = (int)reader["PersonID"];

                    isFound= true;
                }
                else
                {
                    isFound = false;
                }
            }

                return isFound;
        }



        public static bool GetUserInfoByUsername(ref int userID,
            ref int personID,  string userName,ref string passWord,ref bool isActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString) )  
            {
                string query = @"select * from Users where UserName = @userName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userName", userName);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    userID = (int)reader["UserID"];
                    passWord = (string)reader["Password"];
                    isActive = (bool)reader["IsActive"];
                    personID = (int)reader["PersonID"];

                    isFound= true;
                }
                else
                {
                    isFound = false;
                }
            }

                return isFound;
        }


        public static bool GetUserInfoByPersonID( int personID,
            ref int userID, ref string userName,ref string passWord,ref byte isActive)
        {


            bool isFound = false;


            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString) )  
            {
                string query = @"select * from Users where PersonID = @personID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userID", userID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    userName = (string)reader["UserName"];
                    passWord = (string)reader["Password"];
                    isActive = (byte)reader["IsActive"];
                    userID = (int)reader["UserID"];

                    isFound= true;
                }
                else
                {
                    isFound = false;
                }
            }

                return isFound;
        }

        public static bool isUserExistsByUserID(int userId)
        {
            bool isFound = false;


            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select 1 from Users where Users.UserID = @userId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                object result = command.ExecuteScalar();
                isFound = result !=null;
            }
            
            return isFound;

        }

        public static bool isUserExistsByPersonID(int personID)
        {
            bool isFound = false;


            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select 1 from Users where Users.PersonID = @personID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@personID", personID);
                connection.Open();
                
                object result = command.ExecuteScalar();
                 isFound = (result != null);
            }
            return isFound;
        }
        public static bool IsUsernameUsedByAnotherPerson(string username, int personID)
        {
            using (SqlConnection connection =
                  new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"       
        SELECT 1 FROM Users
        WHERE UserName = @username
          AND PersonID <> @PersonID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@PersonID", personID);

                connection.Open();
                return (command.ExecuteScalar() != null);
            }
        }
        public static bool FindByUserIDAndUsername(int userId, string username)
        {
            bool isFound = false;


            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select 1 from Users where Users.PersonID = @personID";
                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue("@personID", personID);
                connection.Open();

                object result = command.ExecuteScalar();
                isFound = (result != null);
            }
            return isFound;
        }
        public static bool IsUserExistsByUsername(string username)
        {
            bool isFound = false;


            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select 1 from Users where UserName = @username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                connection.Open();

                object result = command.ExecuteScalar();
                 isFound = (result != null);
            }
            return isFound;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dataTable = new DataTable();


            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"select Users.UserID, Users.PersonID, FullName = 
People.FirstName + ' ' +People.SecondName + ' ' + ISNULL( People.ThirdName,'') +
People.LastName , Users.UserName,  Users.IsActive from Users join People
on Users.PersonID = People.PersonID";

                SqlCommand Command       = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }

            }
            return dataTable;
        }


    }
}
