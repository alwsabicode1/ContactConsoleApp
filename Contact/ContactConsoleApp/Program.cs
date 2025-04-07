using System;
using ContactBusinessLayer;
using System.Data;

namespace ContactConsoleApp
{
    internal class Program
    {
        static void TestFindContact(int ID)
        {
            clsContact ContactFind = clsContact.Find(ID);
            if (ContactFind != null)
            {
                Console.WriteLine($"Name : {ContactFind.FirstName + " " + ContactFind.LastName}");
                Console.WriteLine($"Email : {ContactFind.Email}");
                Console.WriteLine($"Phone : {ContactFind.Phone}");
                Console.WriteLine($"Address : {ContactFind.Address}");
                Console.WriteLine($"DateOfBirth : {ContactFind.DateOfBirth}");
                Console.WriteLine($"Country Id : {ContactFind.CountryID}");
                Console.WriteLine($"Emage Path : {ContactFind.ImagePath}");
            }
            else
            {
                Console.WriteLine("Contact [" + ID + "] Not Found!");

            }


        }
        static void TestAddNewContact()
        {

            clsContact ContactAddNew = new clsContact();

            ContactAddNew.FirstName = "Ahamed";

            ContactAddNew.LastName = "AL-Wsabi";

            ContactAddNew.Email = "Ahamed@gmail.com";

            ContactAddNew.Phone = "775595524";

            ContactAddNew.Address = "16 Street";

            ContactAddNew.DateOfBirth = new DateTime(2000, 11, 1, 1, 20, 0);

            ContactAddNew.CountryID = 3;

            ContactAddNew.ImagePath = "";

            if (ContactAddNew.Save())
            {
                Console.WriteLine("Contact Added Successfuly With ID=" + ContactAddNew.ID);
            }
            else
            {
                Console.WriteLine("Contact Added Fiald!");
            }
        }
        static void TestUpdateContact(int Id)
        {
            clsContact ContactUpdate = clsContact.Find(Id);

            ContactUpdate.FirstName = "Belal";

            ContactUpdate.LastName = "AL-Wsabi";

            ContactUpdate.Email = "Ahamed@gmail.com";

            ContactUpdate.Phone = "775595524";

            ContactUpdate.Address = "16 Street";

            ContactUpdate.DateOfBirth = new DateTime(2000, 11, 1, 1, 20, 0);

            ContactUpdate.CountryID = 3;

            ContactUpdate.ImagePath = "";

            if (ContactUpdate.Save())
            {
                Console.WriteLine("Contact Updated Successfuly With ID=" + ContactUpdate.ID);
            }
            else
            {
                Console.WriteLine("Contact Updated Fiald!");
            }
        }
        static void TestUpdateCountry(int id)
        {
            clsCountry CountryUpdate = clsCountry.FindCountries(id);

            CountryUpdate.CountryName = "Germany";
            CountryUpdate.CountryCode = "112";
            CountryUpdate.PhoneCode = "777";

            if (CountryUpdate.Save())
            {
                Console.WriteLine("Country Updated Successfuly With ID=" + CountryUpdate.CountryID);
            }
            else
            {
                Console.WriteLine("Country Updated Fiald!");
            }
        }
        static void TestDeleteContact(int Id)
        {
            if (clsContact.isContactExist(Id))
                if (clsContact.DeleteContact(Id))
                    Console.WriteLine("Contact Deleted Successfuly.");
                else
                    Console.WriteLine("Contact Deleted Faild.");
            else
                Console.WriteLine(" The Contact With ID = " + Id + "  Is Not Found.");

        }
        static void TestDeleteCountry(string CountryName)
        {
            if (clsCountry.isCountryExist(CountryName))
                if (clsCountry.DeleteCountry(CountryName))
                    Console.WriteLine("Country Deleted Successfuly.");
                else
                    Console.WriteLine("Country Deleted Faild.");
            else
                Console.WriteLine(" The Country With ID = " + CountryName + "  Is Not Found.");

        }
        static void ListContact()
        {
            DataTable dataTable = clsContact.GetAllContact();
            Console.WriteLine("Contact Data : ");
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ContactID"]} / {row["FirstName"]} / {row["LastName"]}");
            }
        }
        static void ListCountries()
        {
            DataTable dataTable = clsCountry.GetAllCountry();
            Console.WriteLine("Country Data : ");
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["CountryID"]} / {row["CountryName"]} / {row["CountryCode"]} / {row["PhoneCode"]}");
            }
        }
        static void TestIsContactExist(int Id)
        {
            if (clsContact.isContactExist(Id))

                Console.WriteLine("Yes , Contact Found.");
            else
                Console.WriteLine("No ,Contact Is Not Found.");

        }
        static void TestIsCountryExist(string CountryName)
        {
            if (clsCountry.isCountryExist(CountryName))

                Console.WriteLine("Yes , Country Found.");
            else
                Console.WriteLine("No ,Country Is Not Found.");

        }

        static void TestFindCountries(string CountryName)
        {
            clsCountry FindCountry = clsCountry.FindCountriesByName(CountryName);
            if (FindCountry != null)
            {
                Console.WriteLine($"Country ID : {FindCountry.CountryID}");
                Console.WriteLine($"Country Code : {FindCountry.CountryCode}");
                Console.WriteLine($"Phone Code : {FindCountry.PhoneCode}");

            }
            else
            {

                Console.WriteLine("Country [" + CountryName + "] Not Found!");

            }
        }
        static void TestAddNewCountry()
        {
            clsCountry country = new clsCountry();
            country.CountryName = "China";
            country.CountryCode = "1";
            country.PhoneCode = "221";
            if (country.Save())
            {
                Console.WriteLine("Country Added Successfuly With ID=" + country.CountryID);
            }
            else
            {
                Console.WriteLine("Country Added Fiald!");
            }
        }
        static int ReadNumber(string Message)
        {
            int Number;
            Console.WriteLine(Message);
            Number = int.Parse(Console.ReadLine());
            return Number;

        }

        static void Main(string[] args)
        {

            TestFindContact(1);

            Console.ReadKey();
        }
            
        
    }
}
