using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsLicenseClass
    {

        public int LicenseClassID { get; set; }

        public string ClassName { get; set; }

        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }

        public int DefaultValidityLength { get; set; }

        public float ClassFees { get; set; } = 0;

        enum enmode { AddNew = 0, Update = 1 }

        enmode Mode = enmode.AddNew;

        public clsLicenseClass()
        {
            LicenseClassID = -1;
            ClassName = "";
            ClassDescription = "";
            MinimumAllowedAge = -1;
            DefaultValidityLength = -1;
            ClassFees = 0.0f;
        }

        private clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription,
            int MinimumAllowedAge, int DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            Mode = enmode.Update;
        }


        public static clsLicenseClass Find(int LicenseClassID)
        {
            bool isFound = false;
            string ClassName = "";
            string ClassDescription = "";
            int MinimumAllowedAge = -1;
            int DefaultValidityLength = -1;
            float ClassFees = 0.0f;
            isFound = clsLicenseClassesData.GetLicenseClassInfoByID(LicenseClassID, ref ClassName, ref ClassDescription,
                ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees);
            if (isFound)
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }


        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClasses();
        }







    }
}
