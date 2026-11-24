using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class LicenseClasses
    {

        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM LicenseClasses";

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

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
    }
}