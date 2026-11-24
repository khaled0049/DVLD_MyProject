using DVLD.Classes;
using DVLD.Controls;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.LocalDrivingLicenseApplication
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {



        private void _LoadDataInCbLicenseClass()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClasses();

            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";
            cbLicenseClass.DataSource = dt;

            cbLicenseClass.SelectedIndex = 0;


        }



        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();

        }

        public frmAddUpdateLocalDrivingLicesnseApplication(int n)
        {
            InitializeComponent();

        }


        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _LoadDataInCbLicenseClass();
            tabPage2.Enabled = false;
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblApplicationFees.Text = 15.00.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;



        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("Please select a person before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                tabPage2.Enabled = true;
                tcPersonalInfo.SelectedTab = tabPage2;
            }

            // show tabPage2 and hide tabPage1 


        }

        private void ctrlPersonCardWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.Find(ctrlPersonCardWithFilter1.PersonID);

            int LicenseClassID = clsLicenseClass.Find((int)cbLicenseClass.SelectedValue).LicenseClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(person.PersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("This person already has an active application for the selected license class.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


        }
    }
}