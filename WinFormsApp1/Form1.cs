using DataLayer;
using System.IO;

using UserManagement;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            Employee obj = new Employee("Ritee", "Lama");
            MessageBox.Show(obj.FirstName + " " + obj.LastName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveEmployee();
        }

        private void SaveEmployee()
        {
            Employee objEmp = new Employee();
            objEmp.FirstName = txtFirstName.Text;
            objEmp.LastName = txtLastName.Text;

            //string strFileName = "OurEmployeeDataFromClass.txt";
            //string strDataForFile = "First Name = " +
            //    objEmp.FirstName + " " + "Last Name = " + objEmp.LastName;

            EmployeeDataProvider objData = new EmployeeDataProvider();
            objData.AddEmpBySP(objEmp);
            //objData.AddEmployee(objEmp);
            //FileManagement objFileManager = new FileManagement();
            //objFileManager.SaveFile(strFileName, strDataForFile);

            MessageBox.Show("File stored successfully in the database");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = String.Empty;
        }
    }
}