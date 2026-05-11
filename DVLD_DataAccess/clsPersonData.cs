using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace DVLD_DataAccess
{
    public class clsPersonData
    {

        public static int AddNewPerson(
            string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, byte Gendor,
            string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            int personID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"
INSERT INTO People
(
    NationalNo, FirstName, SecondName, ThirdName, LastName,
    DateOfBirth, Gendor, Address, Phone, Email,
    NationalityCountryID, ImagePath
)
VALUES
(
    @NationalNo, @FirstName, @SecondName, @ThirdName, @LastName,
    @DateOfBirth, @Gendor, @Address, @Phone, @Email,
    @NationalityCountryID, @ImagePath
);

SELECT SCOPE_IDENTITY();
";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@NationalNo", NationalNo);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@SecondName", SecondName);
                if (ImagePath != "")
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);
                else
                    command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                command.Parameters.AddWithValue("@Gendor", Gendor);
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@Phone", Phone);
                if (Email != "")
                    command.Parameters.AddWithValue(@"Email", Email);
                else
                    command.Parameters.AddWithValue(@"Email", System.DBNull.Value);
                
                command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                if (ImagePath != null)
                    command.Parameters.AddWithValue("@ImagePath", ImagePath);
                else
                    command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    personID = Convert.ToInt32(result);
            }

            return personID;
        }


        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            byte Gendor, string Address, string Phone, string Email, 
            int NationalityCountryID, string ImagePath)
        {
                int rowsAffected;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"
                              UPDATE People
                                 SET [NationalNo] = @NationalNo
                                    ,[FirstName] =  @FirstName
                                    ,[SecondName] =  @SecondName
                                    ,[ThirdName] =  @ThirdName
                                    ,[LastName] =  @LastName
                                    ,[DateOfBirth] = @DateOfBirth
                                    ,[Gendor] = @Gendor
                                    ,[Address] = @Address
                                    ,[Phone] = @Phone
                                    ,[Email] =  @Email
                                    ,[NationalityCountryID] = @NationalityCountryID
                                    ,[ImagePath] = @ImagePath
                               WHERE PersonID = @PersonID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@NationalNo", NationalNo);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@SecondName", SecondName);
                if (ThirdName != "")
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);
                else
                    command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
                
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = DateOfBirth;

                command.Parameters.Add("@Gendor", SqlDbType.TinyInt).Value = Gendor;

                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@Phone", Phone);
                if (Email != "")
                    command.Parameters.AddWithValue("@Email", Email);
                else
                    command.Parameters.AddWithValue("@Email", System.DBNull.Value);
                
                command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                if (ImagePath != null)
                    command.Parameters.AddWithValue("@ImagePath", ImagePath);
                else
                    command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

                connection.Open();
                
                rowsAffected = command.ExecuteNonQuery();               
                                        
            }
                return (rowsAffected > 0);
        }

        public static bool DeletePersonByID(int PersonID)
        {
            int rowsAffected;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"
                              DELETE FROM People
                                WHERE PersonID = @PersonID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", PersonID);               

                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            return (rowsAffected > 0);
        }

        public static DataTable GetAllPersonsData()
        {
            DataTable PersonsData = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string GetAllPersonsQuery = "select * from People";

                SqlCommand command = new SqlCommand(GetAllPersonsQuery, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        PersonsData.Load(reader);
                    }
                }
            }

            return PersonsData;
        }
        public static DataTable GetAllPersonsWithCountries()
        {
            DataTable Persons = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string GetAllPersonsQuery = @"SELECT People.PersonID, People.NationalNo,
People.FirstName, People.SecondName,People.ThirdName, People.LastName, case when People.Gendor = 0 then 'Male' else 'Female'
end as GendorCaption ,
People.Phone, People.Email  , People.DateOfBirth,
Countries.CountryName  FROM People INNER JOIN Countries
ON People.NationalityCountryID = Countries.CountryID";

                SqlCommand command = new SqlCommand(GetAllPersonsQuery, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Persons.Load(reader);
                    }
                }
            }
            return Persons;
        }
        public static DataTable GetAllPersonsWithCountriesForTheFilter(string FilterType, string Value)
        {
            DataTable Persons = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string GetAllPersonsQuery = $@"SELECT People.PersonID, People.NationalNo,
                    People.FirstName, People.SecondName,People.ThirdName, People.LastName, People.Gendor,
                    People.Phone, People.Email  , People.DateOfBirth,
                    Countries.CountryName as Nationality FROM People INNER JOIN Countries
                    ON People.NationalityCountryID = Countries.CountryID
                    where {FilterType} LIKE '%' + @Value + '%'";
                

                SqlCommand command = new SqlCommand(GetAllPersonsQuery, connection);
                command.Parameters.AddWithValue("@FilterType", FilterType);
                //command.Parameters.AddWithValue("@Value", "%" + Value + "%");
                command.Parameters.AddWithValue("@Value",   Value  );

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Persons.Load(reader);
                    }
                }
            }
            return Persons;
        }



        public static bool IsPersonExistsByNationalNoo(string NationalNo)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"Select 1 from people where NationalNo = @NationalNo";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NationalNo", NationalNo);
                connection.Open();

                object result = command.ExecuteScalar();
                isFound = (result != null);  

                
            }
                return isFound;
        }
        public static bool IsPersonExistsByID(int personID)
        {

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "Select 1 from people where personID = @personID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@personID", personID);
                connection.Open();

                object result = command.ExecuteScalar();
                return (result !=  null);

            }
        }



        public static bool GetPersonInfoByNationalNo(ref int PersonID, string NatoinalNo, ref string FirstName,
                ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte gender, ref string Adress,
                ref string Phone, ref string Email, ref int nationalityCountryID, ref string ImagePath, ref string CountryName)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                bool isFound = false;

                string query = @"select People.*, Countries.CountryName from People join 
        Countries on People.NationalityCountryID = Countries.CountryID where NationalNo =@NatoinalNo";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NatoinalNo", NatoinalNo);
                connection.Open();
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            PersonID = (int)reader["PersonID"];
                            FirstName = (string)reader["FirstName"];
                            SecondName = (string)reader["SecondName"];

                            if (reader["ThirdName"] != DBNull.Value)
                                ThirdName = (string)reader["ThirdName"];
                            else
                                ThirdName = "";

                            LastName = (string)reader["LastName"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            gender = (byte)reader["Gendor"];
                            Adress = (string)reader["Address"];
                            Phone = (string)reader["Phone"];

                            if (reader["Email"] != DBNull.Value)
                                Email = (string)reader["Email"];
                            else
                                Email = "";

                            nationalityCountryID = (int)reader["NationalityCountryID"];

                            if (reader["ImagePath"] != DBNull.Value)
                                ImagePath = (string)reader["ImagePath"];
                            else
                                ImagePath = null;

                            CountryName = (string)reader["CountryName"];

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
        }


        public static bool GetPersonInfoById(int PersonID, ref string NationalNo, ref string FirstName,
          ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
          ref byte Gender,
          ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID,
          ref string ImagePath, ref string CountryName)
        {

                bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {

                string query = @"select People.*, Countries.CountryName from People join 
        Countries on People.NationalityCountryID = Countries.CountryID where PersonID =@PersonID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", PersonID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    else
                        ThirdName = "";

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = "";

                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = null;

                    CountryName = (string)reader["CountryName"];

                }
                else
                {
                    isFound = false;
                }

                //try
                //{
                //    using (SqlDataReader reader = command.ExecuteReader())
                //    {
                //        if (reader.Read())
                //        {
                //            isFound = true;

                //            NationalNo = (string)reader["NationalNo"];
                //            FirstName = (string)reader["FirstName"];
                //            SecondName = (string)reader["SecondName"];

                //            if (reader["ThirdName"] != DBNull.Value)
                //                ThirdName = (string)reader["ThirdName"];
                //            else
                //                ThirdName = "";

                //            LastName = (string)reader["LastName"];
                //            DateOfBirth = (DateTime)reader["DateOfBirth"];
                //            Gender = (byte)reader["Gendor"];
                //            Address = (string)reader["Address"];
                //            Phone = (string)reader["Phone"];

                //            if (reader["Email"] != DBNull.Value)
                //                Email = (string)reader["Email"];
                //            else
                //                Email = "";

                //            NationalityCountryID = (int)reader["NationalityCountryID"];

                //            if (reader["ImagePath"] != DBNull.Value)
                //                ImagePath = (string)reader["ImagePath"];
                //            else
                //                ImagePath = null;

                //            CountryName = (string)reader["CountryName"];

                //        }
                //        else
                //        {
                //            isFound = false;
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    isFound = false;

                //}
            }
                return isFound;

        }



        public static bool GetPersonInfoByDriverId(int DriverID, ref int PersonId, ref string NationalNo, ref string FirstName,
          ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
          ref byte Gender,
          ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID,
          ref string ImagePath, ref string CountryName)
        {

            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {

                string query = @"select P.*, C.CountryName from People P join Drivers D on D.PersonID = P.PersonID 
join Countries C on C.CountryID = P.NationalityCountryID
where D.DriverID = @DriverID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                connection.Open();
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            PersonId = (int)reader["PersonID"];
                            NationalNo = (string)reader["NationalNo"];
                            FirstName = (string)reader["FirstName"];
                            SecondName = (string)reader["SecondName"];

                            if (reader["ThirdName"] != DBNull.Value)
                                ThirdName = (string)reader["ThirdName"];
                            else
                                ThirdName = "";

                            LastName = (string)reader["LastName"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Gender = (byte)reader["Gendor"];
                            Address = (string)reader["Address"];
                            Phone = (string)reader["Phone"];

                            if (reader["Email"] != DBNull.Value)
                                Email = (string)reader["Email"];
                            else
                                Email = "";

                            NationalityCountryID = (int)reader["NationalityCountryID"];

                            if (reader["ImagePath"] != DBNull.Value)
                                ImagePath = (string)reader["ImagePath"];
                            else
                                ImagePath = null;

                            CountryName = (string)reader["CountryName"];

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



        public static int GetPersonIDByNationalNo(string NationalNo)
        {
            int PersonID = -1;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {

                string query = "select PersonID from People where NationalNo=@NationalNo";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NationalNo", NationalNo);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result !=  null  && int.TryParse(result.ToString(), out int id))
                {
                    PersonID = id;
                }

            }

            return PersonID;
        }
        
        public static DataTable SearchPersonsByNationalNo(string NationalNo)
        {
            DataTable PersonsSearchResult = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string GetAllPersonsQuery = @"SELECT People.PersonID, People.NationalNo,
People.FirstName, People.SecondName,People.ThirdName, People.LastName, People.Gendor,
People.Phone, People.Email  , People.DateOfBirth,
Countries.CountryName as Nationality FROM People INNER JOIN Countries
ON People.NationalityCountryID = Countries.CountryID where NationalNo Like @NationalNo";

                SqlCommand command = new SqlCommand(GetAllPersonsQuery, connection);
                command.Parameters.AddWithValue("@NationalNo", "%" + NationalNo + "%");

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        PersonsSearchResult.Load(reader);
                    }
                }
                
            }
            return PersonsSearchResult;
        }

        public static DataTable SearchPersons(string keyword)
        {
            DataTable result = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @" SELECT 
            People.PersonID,
            People.NationalNo,
            People.FirstName,
            People.SecondName,
            People.ThirdName,
            People.LastName,
            People.Gendor,
            People.Phone,
            People.Email,
            People.DateOfBirth,
            Countries.CountryName AS Nationality
        FROM People
        INNER JOIN Countries
            ON People.NationalityCountryID = Countries.CountryID
        WHERE 
            People.NationalNo LIKE @keyword OR
            People.FirstName LIKE @keyword OR
            People.SecondName LIKE @keyword OR
            People.ThirdName LIKE @keyword OR
            People.LastName LIKE @keyword OR
            People.Phone LIKE @keyword OR
            People.Email LIKE @keyword";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                connection.Open();
                result.Load(command.ExecuteReader());
            }
            return result;
        }




        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID,
            ref string FirstName,
           ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
           ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            return false;
        }


        public static bool IsNationalNoUsedByAnotherPerson(string nationalNo, int personID)
        {
            using (SqlConnection connection =
                   new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"       
        SELECT 1 FROM People
        WHERE NationalNo = @NationalNo
          AND PersonID <> @PersonID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NationalNo", nationalNo);
                command.Parameters.AddWithValue("@PersonID", personID);

                connection.Open();
                return  ( command.ExecuteScalar() != null );
            }
        }



    }
}
