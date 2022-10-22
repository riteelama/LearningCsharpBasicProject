using DataLayer;
using System.Data;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            BindDataGrid();
        }

        public void BindDataGrid()
        {
            EmployeeDataProvider objData = new EmployeeDataProvider();
            DataTable dt = objData.GetAllEmp();
            dataGridView1.DataSource = dt;
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            Employee obj = new Employee();
            MessageBox.Show(obj.FirstName + " " + obj.LastName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveEmployee();
            ClearForm();
            BindDataGrid();
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
            if(userId == 0)
            {
                objData.AddEmpBySP(objEmp);
            }
            else
            {
                objEmp.UserId = userId;
                objData.UpdateEmpBySP(objEmp);
                userId = 0;
            }
            //objData.AddEmployee(objEmp);
            //FileManagement objFileManager = new FileManagement();
            //objFileManager.SaveFile(strFileName, strDataForFile);

            MessageBox.Show("File stored successfully in the database");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtFirstName.Text = "";
            txtLastName.Text = String.Empty;
        }

        int userId = 0;

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            userId = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            if (userId != 0)
            {
                LoadUserDataByID();
            }
        }

        public void LoadUserDataByID()
        {
            EmployeeDataProvider objEmpData = new EmployeeDataProvider();
            DataTable dt = objEmpData.GetEmpByUserId(userId);

            if(dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtFirstName.Text = dr[1].ToString();
                    txtLastName.Text = dr[2].ToString();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteEmployee();
        }

        private void DeleteEmployee()
        {
            Employee objEmp = new Employee();

            EmployeeDataProvider objData = new EmployeeDataProvider();
            objData.DeleteEmpBySP(objEmp);
            objEmp.UserId = userId;
            objData.DeleteEmpBySP(objEmp);
            userId = 0;

            MessageBox.Show("Data Deleted from database successfully!!!");

            BindDataGrid();
         }
            
        }
    }