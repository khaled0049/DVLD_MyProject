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
            DataTable dt = clsLicenseClasses.GetAllLicenseClasses();

            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";
            cbLicenseClass.DataSource = dt;

            cbLicenseClass.SelectedIndex = 0;
            

        }



        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
             
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _LoadDataInCbLicenseClass();
            tabPage2.Enabled = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // show tabPage2 and hide tabPage1 
            tabPage2.Enabled = true;
            tcPersonalInfo.SelectedTab = tabPage2;
        }
    }
}
