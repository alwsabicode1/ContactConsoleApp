using ContactDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBusinessLayer
{
    public class clsCountry
    {

        public enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string PhoneCode     { get; set; }

        public clsCountry()
        {
            this.CountryID = -1;
            this.CountryName = "";
            this.CountryCode = "";
            this.PhoneCode = "";

            Mode = enMode.AddNew;

        }
        private clsCountry(int ID, string CountryName,string CountryCode,string PhoneCode)
        {
            this.CountryID = ID;
            this.CountryName = CountryName;
            this.CountryCode = CountryCode;
            this.PhoneCode = PhoneCode;
            Mode = enMode.Update;
        }
        public static clsCountry FindCountriesByName(string CountryName)
        {
            int CountryID = -1;
            string CountryCode = "";
            string PhoneCode = "";
            if (clsCountryDataAccess.GetCountryByName(ref CountryID, CountryName,ref CountryCode,ref PhoneCode))
            {
                return new clsCountry(CountryID, CountryName,CountryCode,PhoneCode);

            }
            else
            {

                return null;
            }
        }
        public static clsCountry FindCountries(int CountryID)
        {
            string CountryName = "";         
            string CountryCode = "";
            string PhoneCode = "";
            if (clsCountryDataAccess.GetCountryById(CountryID, ref CountryName , ref CountryCode, ref PhoneCode))
            {
                return new clsCountry(CountryID, CountryName, CountryCode, PhoneCode);

            }
            else
            {

                return null;
            }
        }
        private bool _AddNewCountry()
        {
            this.CountryID = clsCountryDataAccess.AddNewCountry(this.CountryName,this.CountryCode,this.PhoneCode);
            return (this.CountryID != -1);
        }
        private bool _UpdateCountry()
        {
            return clsCountryDataAccess.UpdateCountry(this.CountryID, this.CountryName,this.CountryCode,this.PhoneCode);

        }
        public static bool DeleteCountry(string CountryName)
        {
            return clsCountryDataAccess.DeleteCountry(CountryName);
        }
        public static DataTable GetAllCountry()
        {
            return clsCountryDataAccess.GetAllCountry();
        }
        public static bool isCountryExist(string CountryName)
        {
            return clsCountryDataAccess.isCountryExist(CountryName);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCountry();


            }
            return false;
        }

    }
}

