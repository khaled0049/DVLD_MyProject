using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {
        // Read By ID
        public static bool GetTestAppointmentInfoByID(
            int TestAppointmentID,
            ref int TestTypeID,
            ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate,
            ref decimal PaidFees,
            ref int CreatedByUserID,
            ref bool IsLocked,
            ref int? RetakeTestApplicationID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT * FROM TestAppointments
                                 WHERE TestAppointmentID = @TestAppointmentID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@TestAppointmentID", TestAppointmentID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        IsFound = true;

                        TestTypeID = (int)reader["TestTypeID"];

                        LocalDrivingLicenseApplicationID =
                            (int)reader["LocalDrivingLicenseApplicationID"];

                        AppointmentDate = (DateTime)reader["AppointmentDate"];

                        PaidFees = (decimal)reader["PaidFees"];

                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        IsLocked = (bool)reader["IsLocked"];

                        RetakeTestApplicationID =
                            reader["RetakeTestApplicationID"] == DBNull.Value
                            ? (int?)null
                            : (int)reader["RetakeTestApplicationID"];
                    }
                }
            }

            return IsFound;
        }


        // Create
        public static int AddNewTestAppointment(
            int TestTypeID,
            int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate,
            decimal PaidFees,
            int CreatedByUserID,
            bool IsLocked,
            int? RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;

            using (SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    INSERT INTO TestAppointments
                    (
                        TestTypeID,
                        LocalDrivingLicenseApplicationID,
                        AppointmentDate,
                        PaidFees,
                        CreatedByUserID,
                        IsLocked,
                        RetakeTestApplicationID
                    )
                    VALUES
                    (
                        @TestTypeID,
                        @LocalDrivingLicenseApplicationID,
                        @AppointmentDate,
                        @PaidFees,
                        @CreatedByUserID,
                        @IsLocked,
                        @RetakeTestApplicationID
                    );

                    SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                command.Parameters.AddWithValue(
                    "@LocalDrivingLicenseApplicationID",
                    LocalDrivingLicenseApplicationID);

                command.Parameters.AddWithValue(
                    "@AppointmentDate", AppointmentDate);

                command.Parameters.Add("@PaidFees", SqlDbType.Money)
                    .Value = PaidFees;

                command.Parameters.AddWithValue(
                    "@CreatedByUserID", CreatedByUserID);

                command.Parameters.AddWithValue("@IsLocked", IsLocked);

                command.Parameters.Add(
                    "@RetakeTestApplicationID", SqlDbType.Int).Value =
                    (object)RetakeTestApplicationID ?? DBNull.Value;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    TestAppointmentID = Convert.ToInt32(result);
                }
            }

            return TestAppointmentID;
        }


        // Update
        public static bool UpdateTestAppointment(
            int TestAppointmentID,
            int TestTypeID,
            int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate,
            decimal PaidFees,
            int CreatedByUserID,
            bool IsLocked,
            int? RetakeTestApplicationID)
        {
            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    UPDATE TestAppointments
                    SET
                        TestTypeID = @TestTypeID,
                        LocalDrivingLicenseApplicationID =
                            @LocalDrivingLicenseApplicationID,
                        AppointmentDate = @AppointmentDate,
                        PaidFees = @PaidFees,
                        CreatedByUserID = @CreatedByUserID,
                        IsLocked = @IsLocked,
                        RetakeTestApplicationID = @RetakeTestApplicationID
                    WHERE TestAppointmentID = @TestAppointmentID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@TestAppointmentID", TestAppointmentID);

                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                command.Parameters.AddWithValue(
                    "@LocalDrivingLicenseApplicationID",
                    LocalDrivingLicenseApplicationID);

                command.Parameters.AddWithValue(
                    "@AppointmentDate", AppointmentDate);

                command.Parameters.Add("@PaidFees", SqlDbType.Money)
                    .Value = PaidFees;

                command.Parameters.AddWithValue(
                    "@CreatedByUserID", CreatedByUserID);

                command.Parameters.AddWithValue("@IsLocked", IsLocked);

                command.Parameters.Add(
                    "@RetakeTestApplicationID", SqlDbType.Int).Value =
                    (object)RetakeTestApplicationID ?? DBNull.Value;

                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }

            return RowsAffected > 0;
        }


        // Delete
        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString))
            {
                string query = @"DELETE FROM TestAppointments
                                 WHERE TestAppointmentID = @TestAppointmentID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@TestAppointmentID", TestAppointmentID);

                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }

            return RowsAffected > 0;
        }


        // Read All
        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM TestAppointments";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }
    }
}