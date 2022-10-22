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

        public void UpdateEmpBySP(Employee objEmp)
        {
            SqlConnection SQLConn = new SqlConnection(sqlConString);
            SqlCommand SQLCmd = new SqlCommand();
            SQLCmd.Connection = SQLConn;
            SQLCmd.CommandText = "[dbo].[uspEmpUpdate]";
            SQLCmd.CommandType = CommandType.StoredProcedure;

            SQLCmd.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = objEmp.UserId;
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

        public DataTable GetAllEmp()
        {
            string strSPName = "[dbo].[uspEmpGetAll]";
            SqlParameter[] parameters = null; // new SqlParameter[1]
            DataSet ds = ExecuteProcedureReturnDataSet(strSPName, parameters);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        public DataTable GetEmpByUserId(int UserId)
        {
            string strSPName = "[dbo].[uspEmpGetById]";
            SqlParameter[] parameters =
                {new SqlParameter("@userId", UserId)};
            DataSet ds = ExecuteProcedureReturnDataSet(strSPName, parameters);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        public DataSet ExecuteProcedureReturnDataSet(string procName, 
            params SqlParameter[] parameters)
        {
            DataSet result = null;
            using (var sqlConnection = new SqlConnection(sqlConString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = procName;
                        if(parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }
            return result;
        }
    }
}
