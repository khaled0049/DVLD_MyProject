using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicenseClassesData
    {

        public static bool GetLicenseClassInfoByID(int LicenseClassID, ref string ClassName , 
            ref string classDescription, ref int MinimumAllowedAge , ref int DefaultValidaityLength,
            ref float ClassFees)
        {
            bool IsFound = false;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                conn.Open();
                string query = "SELECT  LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge," +
                    " DefaultValidityLength, ClassFees" +
                    " WHERE LicenseClassID = @LicenseClassID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            IsFound = true;
                            ClassName = reader["ClassName"].ToString();
                            classDescription = reader["classDescription"].ToString();
                            MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                            DefaultValidaityLength = Convert.ToInt32(reader["DefaultValidaityLength"]);
                            ClassFees = Convert.ToSingle(reader["ClassFees"]);
                        }
                    }
                }
            }
            return IsFound;
        }


        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                conn.Open();
                string query = "SELECT LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge," +
                    " DefaultValidityLength, ClassFees FROM LicenseClasses";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.HasRows)
                    {
                        dt.Load(reader);
                    }

                }
            }
            return dt;
        }



    }
}
