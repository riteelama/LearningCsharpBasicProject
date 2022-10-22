using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement;

namespace DataLayer
{
    public class EmployeeDataProvider
    {
        string sqlConString = ConfigurationManager.ConnectionStrings["SQLConn"].ToString();
        public void AddEmployee(Employee objEmp)
        {           
            
            //"data source=DESKTOP-3KLQDN8\\SQLEXPRESS; database=UserManagement; integrated security=SSPI";
            try
            {
                using SqlConnection connection = new SqlConnection(sqlConString);
                SqlCommand cmd = new SqlCommand("insert into Employee values (' "+objEmp.FirstName+" ', ' "+ objEmp.LastName + " ')", connection);
                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void AddEmpBySP(Employee objEmp)
        {
            SqlConnection SQLConn = new SqlConnection(sqlConString);
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.Connection = SQLConn;
            SQLCmd.CommandText = "[dbo].[usp_EmpAdd]";
            SQLCmd.CommandType = CommandType.StoredProcedure;

            SQLCmd.Parameters.AddWithValue("@firstName", SqlDbType.NVarChar).Value = objEmp.FirstName;
            SQLCmd.Parameters.AddWithValue("@lastName", SqlDbType.NVarChar).Value = objEmp.LastName;

            try
            {
                SQLConn.Open();
                SQLCmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }
    }
}
