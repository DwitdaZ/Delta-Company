using CompanyD.Models.Domain;
using CompanyD.Models.Requests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CompanyD.Services
{ 
    public class EmployeeService
    {

        public List<Employee> AllEmployees()
        {
            using (var conn = Getconn())
            {
                List<Employee> employeeList = null;
                if (conn.State == ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("Employee_SelectAll", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        employeeList = new List<Employee>();

                        while (reader.Read())
                        {
                            employeeList.Add(new Employee() {
                                Employee_Id = (int)reader["employee_id"],
                                First_Name = (string)reader["first_name"],
                                Last_Name = (string)reader["last_name"],
                                Gender = (string)reader["gender"],
                                Salary = (decimal)reader["salary"],
                                Position = (string)reader["position"],
                                Department_Id = (int)reader["department_id"]
                            });
                        }
                    }
                }
                else
                {
                    BadConn();
                }
                return employeeList;
            }
        }

        public Employee EmployeeById(int id)
        {
            using (var conn = Getconn())
            {
                Employee employee = null;
                if (conn.State == ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("Employee_SelectById", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@employee_id", id);
                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        
                        if (reader.Read())
                        {
                            employee = new Employee()
                            {
                                Employee_Id = (int)reader["employee_id"],
                                First_Name = (string)reader["first_name"],
                                Last_Name = (string)reader["last_name"],
                                Gender = (string)reader["gender"],
                                Salary  = (decimal)reader["salary"],
                                Position = (string)reader["position"],
                                Department_Id = (int)reader["department_id"]
                            };
                        }
                    }
                }
                else
                {
                    BadConn();
                }
                return employee;
            }
        }

        public int AddEmployee(EmployeeAddRequest model)
        {
            using (var conn = Getconn())
            {
                int id = 0;
                if (conn.State == ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("Employee_Create", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@first_name", model.First_Name);
                        cmd.Parameters.AddWithValue("@last_name", model.Last_Name);
                        cmd.Parameters.AddWithValue("@gender", model.Gender);
                        cmd.Parameters.AddWithValue("@salary", model.Salary);
                        cmd.Parameters.AddWithValue("@position", model.Position);
                        cmd.Parameters.AddWithValue("@department_id", model.Department_Id);
                        cmd.Parameters.AddWithValue("@employee_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        id = (int)cmd.Parameters["@employee_id"].Value;
                    }
                }
                else 
                {
                    BadConn();
                }
                return id;
            }
        }

        public int EditEmployee(EmployeeEditRequest model)
        {
            using (var conn = Getconn())
            {
                int id = 0;
                if (conn.State == ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("Employee_Update", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@employee_id", model.Employee_Id);

                        cmd.Parameters.AddWithValue("@first_name", model.First_Name);
                        cmd.Parameters.AddWithValue("@last_name", model.Last_Name);
                        cmd.Parameters.AddWithValue("@gender", model.Gender);
                        cmd.Parameters.AddWithValue("@salary", model.Salary);
                        cmd.Parameters.AddWithValue("@position", model.Position);
                        cmd.Parameters.AddWithValue("@department_id", model.Department_Id);

                        cmd.ExecuteNonQuery();
                        id = (int)cmd.Parameters["@employee_id"].Value;
                    }
                }
                else
                {
                    BadConn();
                }
                return id;
            }
        }

        public int RemoveEmployee(int id)
        {
            using (var conn = Getconn())
            {
                if (conn.State == ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("Employee_Delete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@employee_id", id);
                        id = (int)cmd.Parameters["@employee_id"].Value;
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    BadConn();
                }
                return id;
            }
        }

        private string BadConn()
        {
            return "Error, Db connection failed!!"; 
        }
        
        private SqlConnection Getconn()
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString);
            conn.Open();
            return conn;
        }
    }
}