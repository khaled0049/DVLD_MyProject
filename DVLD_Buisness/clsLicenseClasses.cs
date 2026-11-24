using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsLicenseClasses
    {
        public enum enMode { AddNew = 0, Update = 1 };

        enMode Mode = enMode.AddNew;
        public int LicenseClassID { get; set; }

        public string ClassName { get; set; }

        public string ClassDescription { get; set; }

        public int MinimumAllowedAge { get; set; }

        public int DefaultValidatyLength { get; set; }

        public float ClassFees { get; set; }



        public clsLicenseClasses()
        {
            LicenseClassID = 0;
            ClassName = string.Empty;
            ClassDescription = string.Empty;
            MinimumAllowedAge = 0;
            DefaultValidatyLength = 0;
            ClassFees = 0;
        }

        private clsLicenseClasses(int LicenseClassID, string ClassName, string ClassDescription, int MinimumAge,
            int DefaultValidatyLength, int ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAge;
            this.DefaultValidatyLength = DefaultValidatyLength;
            this.ClassFees = ClassFees;
            Mode = enMode.Update;
        }


        public static DataTable GetAllLicenseClasses()
        {
            return LicenseClasses.GetAllLicenseClasses();
        }










    }
}
