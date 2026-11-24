using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsTestData
    {
        // Read By ID
        public static bool GetTestInfoByID(
            int TestID,
            ref int TestAppointmentID,
            ref bool TestResult,
            ref string Notes,
            ref int CreatedByUserID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT * FROM Tests
                                 WHERE TestID = @TestID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TestID", TestID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        IsFound = true;

                        TestAppointmentID =
                            (int)reader["TestAppointmentID"];

                        TestResult = (bool)reader["TestResult"];

                        Notes = reader["Notes"] == DBNull.Value
                            ? string.Empty
                            : (string)reader["Notes"];

                        CreatedByUserID =
                            (int)reader["CreatedByUserID"];
                    }
                }
            }

            return IsFound;
        }


        // Create
        public static int AddNewTest(
            int TestAppointmentID,
            bool TestResult,
            string Notes,
            int CreatedByUserID)
        {
            int TestID = -1;

            using (SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    INSERT INTO Tests
                    (
                        TestAppointmentID,
                        TestResult,
                        Notes,
                        CreatedByUserID
                    )
                    VALUES
                    (
                        @TestAppointmentID,
                        @TestResult,
                        @Notes,
                        @CreatedByUserID
                    );

                    SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@TestAppointmentID", TestAppointmentID);

                command.Parameters.AddWithValue(
                    "@TestResult", TestResult);

                command.Parameters.AddWithValue(
                    "@Notes",
                    string.IsNullOrEmpty(Notes)
                    ? (object)DBNull.Value
                    : Notes);

                command.Parameters.AddWithValue(
                    "@CreatedByUserID", CreatedByUserID);

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    TestID = Convert.ToInt32(result);
                }
            }

            return TestID;
        }


        // Update
        public static bool UpdateTest(
            int TestID,
            int TestAppointmentID,
            bool TestResult,
            string Notes,
            int CreatedByUserID)
        {
            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    UPDATE Tests
                    SET
                        TestAppointmentID = @TestAppointmentID,
                        TestResult = @TestResult,
                        Notes = @Notes,
                        CreatedByUserID = @CreatedByUserID
                    WHERE TestID = @TestID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@TestID", TestID);

                command.Parameters.AddWithValue(
                    "@TestAppointmentID", TestAppointmentID);

                command.Parameters.AddWithValue(
                    "@TestResult", TestResult);

                command.Parameters.AddWithValue(
                    "@Notes",
                    string.IsNullOrEmpty(Notes)
                    ? (object)DBNull.Value
                    : Notes);

                command.Parameters.AddWithValue(
                    "@CreatedByUserID", CreatedByUserID);

                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }

            return RowsAffected > 0;
        }


        // Delete
        public static bool DeleteTest(int TestID)
        {
            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString))
            {
                string query = @"DELETE FROM Tests
                                 WHERE TestID = @TestID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TestID", TestID);

                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }

            return RowsAffected > 0;
        }


        // Read All
        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Tests";

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