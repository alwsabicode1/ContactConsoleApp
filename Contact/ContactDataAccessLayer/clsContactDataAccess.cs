using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;

namespace ContactDataAccessLayer
{
    public class clsContactDataAccess
    {
        public static bool GetContactInfoById(int Id, ref string FirstName, ref string LastName,
        ref string Email, ref string Phone, ref string Address,
        ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = "select *from Contacts where ContactID=@ContactID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ContactID", Id);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        public static int AddNewContact(string FirstName, string LastName, string Email,
            string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            int ContactID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = @"insert into Contacts(FirstName,LastName,Email,Phone,Address,
                           DateOfBirth,CountryID,ImagePath)
                            Values(@FirstName,@LastName,@Email,@Phone,@Address,
                           @DateOfBirth,@CountryID,@ImagePath);
                            select Scope_identity();";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@CountryID", CountryID);
            if (ImagePath != "")
            {
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }
            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertID))
                {
                    ContactID = InsertID;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return ContactID;
        }
        public static bool UpdateContact(int Id, string FirstName, string LastName, string Email,
            string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = @"Update Contacts
                set FirstName=@FirstName,
                    LastName=@LastName,
                    Email=@Email,
                    Phone=@Phone,
                    Address=@Address,
                    DateOfBirth=@DateOfBirth,
                    CountryID=@CountryID,
                    ImagePath=@ImagePath
                    where ContactID=@ContactID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ContactID", Id);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImagePath != "")
            {
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }
        public static bool DeleteContact(int ContactID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = @"Delete Contacts where ContactID=@ContactID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@contactID", ContactID);
            try
            {
                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }
        public static DataTable GetAllContact()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = @"select *from contacts ";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);

                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;


        }
        public static bool isContactExist(int Id)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = "select Found=1 from Contacts where ContactID = @ContactID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ContactID", Id);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
    }

}

