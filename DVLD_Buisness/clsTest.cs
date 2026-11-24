using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };

        enMode Mode = enMode.AddNew;

        public int TestID { get; set; }

        public int TestAppointmentID { get; set; }

        public bool TestResult { get; set; }

        public string Notes { get; set; }

        public int CreatedByUserID { get; set; }


        // AddNew Constructor
        public clsTest()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = string.Empty;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }


        // Update Constructor
        private clsTest(
            int TestID,
            int TestAppointmentID,
            bool TestResult,
            string Notes,
            int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }


        // Create
        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(
                this.TestAppointmentID,
                this.TestResult,
                this.Notes,
                this.CreatedByUserID
            );

            return this.TestID != -1;
        }


        // Update
        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(
                this.TestID,
                this.TestAppointmentID,
                this.TestResult,
                this.Notes,
                this.CreatedByUserID
            );
        }


        // Read By ID
        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = string.Empty;
            int CreatedByUserID = -1;

            bool IsFound = clsTestData.GetTestInfoByID(
                TestID,
                ref TestAppointmentID,
                ref TestResult,
                ref Notes,
                ref CreatedByUserID
            );

            if (IsFound)
            {
                return new clsTest(
                    TestID,
                    TestAppointmentID,
                    TestResult,
                    Notes,
                    CreatedByUserID
                );
            }

            return null;
        }


        // Save
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:

                    return _UpdateTest();
            }

            return false;
        }


        // Delete
        public static bool DeleteTest(int TestID)
        {
            return clsTestData.DeleteTest(TestID);
        }


        // Read All
        public static DataTable GetAllTests()
        {
            return clsTestData.GetAllTests();
        }
    }
}