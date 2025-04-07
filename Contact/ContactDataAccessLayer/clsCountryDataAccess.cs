using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDataAccessLayer
{
    public class clsCountryDataAccess
    {

        public static bool GetCountryByName(ref int CountryID, string CountryName, ref string CountryCode, ref string PhoneCode)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = "select * from Countries where CountryName=@CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    CountryID = (int)reader["CountryID"];
                    CountryCode = (string)reader["CountryCode"];
                    PhoneCode = (string)reader["PhoneCode"];

                }
                else
                {
                    IsFound = false;
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
            return IsFound;
        }
        public static bool GetCountryById(int CountryID, ref string CountryName, ref string CountryCode, ref string PhoneCode)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = "select * from Countries where CountryID=@CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    CountryName = Convert.ToString(reader["CountryName"]);
                    CountryCode = Convert.ToString(reader["CountryCode"]);
                    PhoneCode = Convert.ToString(reader["PhoneCode"]);
                }
                else
                {
                    IsFound = false;
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
            return IsFound;
        }
        public static int AddNewCountry(string CountryName, string CountryCode, string PhoneCode)
        {
            int CountryID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = @"insert into Countries(CountryName,CountryCode,PhoneCode)
                            Values(@CountryName,@CountryCode,@PhoneCode);
                            select Scope_identity();";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CountryName", CountryName);
            cmd.Parameters.AddWithValue("@CountryCode", CountryCode);
            cmd.Parameters.AddWithValue("@PhoneCode", PhoneCode);

            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertID))
                {
                    CountryID = InsertID;
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

            return CountryID;
        }
        public static bool UpdateCountry(int Id, string CountryName, string CountryCode, string PhoneCode)
        {
            int rowsAffected = 0;
            string query = @"UPDATE Countries
                     SET CountryName = @CountryName,
                         CountryCode = @CountryCode,
                         PhoneCode = @PhoneCode
                     WHERE CountryID = @CountryID";

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            SqlCommand cmd = new SqlCommand(query, connection);

            // إضافة المعلمات
            cmd.Parameters.AddWithValue("@CountryID", Id);
            cmd.Parameters.AddWithValue("@CountryName", CountryName);

            // التحقق من CountryCode
            if (string.IsNullOrEmpty(CountryCode))
            {
                cmd.Parameters.AddWithValue("@CountryCode", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CountryCode", CountryCode);
            }

            // التحقق من PhoneCode
            if (string.IsNullOrEmpty(PhoneCode))
            {
                cmd.Parameters.AddWithValue("@PhoneCode", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            }

            try
            {
                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {

                connection.Close();
            }
            return (rowsAffected > 0);

        }



        public static bool DeleteCountry(string CountryName)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = @"Delete Countries where CountryName=@CountryName";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CountryName", CountryName);
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
        public static DataTable GetAllCountry()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = @"select *from Countries ";
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
        public static bool isCountryExist(string CountryName )
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionString);
            string query = "select Found=1 from Countries where CountryName = @CountryName";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CountryName", CountryName);
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
