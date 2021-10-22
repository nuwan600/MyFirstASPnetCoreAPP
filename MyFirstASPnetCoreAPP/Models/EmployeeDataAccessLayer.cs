using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace MyFirstASPnetCoreAPP.Models
{
    public class EmployeeDataAccessLayer : Controller
    {
        
        

        private readonly IConfiguration _configuration;
        private string _sqlDataSoruce;

        public EmployeeDataAccessLayer(IConfiguration configuration)
        {
            //_sqlDataSoruce = System.Configuration.ConfigurationManager.AppSettings["EmployeeAppCon"].ToString();
            _configuration = configuration;

            _sqlDataSoruce = _configuration.GetConnectionString("EmployeeAppCon");
        }

        public EmployeeDataAccessLayer()
        {
            _sqlDataSoruce = "Data Source=CML-NUWANIN\\SQL2019;Initial Catalog=EmployeeDetialsDB;Integrated Security=False;Trusted_Connection=true;User ID=sa;Password=Passw0rd@123;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        }


        /// <summary>
        /// To view all emplyees detials 
        /// </summary>
        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> listEmployees = new List<Employee>();

            using (SqlConnection con = new SqlConnection(_sqlDataSoruce))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();


                while(rdr.Read())
                {
                    Employee employee = new Employee();

                    employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                    employee.FirstName = rdr["FirstName"].ToString();
                    employee.LastName = rdr["LastName"].ToString();
                    employee.City = rdr["City"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.IsDeleted = Convert.ToBoolean(rdr["IsDeleted"]);

                    listEmployees.Add(employee);
                }

            }
                return listEmployees;
        }

        public Boolean AddEmployee(Employee employee)
        {
            using(SqlConnection con = new SqlConnection(_sqlDataSoruce))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@IsDeleted", employee.IsDeleted);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }

            return false;
        }

        public Boolean UpdateEmployee(Employee employee)
        {

            using (SqlConnection con = new SqlConnection(_sqlDataSoruce))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpId", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@IsDeleted", employee.IsDeleted);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }

            return false;
        }

        public Employee GetEmployeeData(int? id)
        {
            Employee employee = new Employee();

            using (SqlConnection con = new SqlConnection(_sqlDataSoruce))
            {
                string sqlQuery = "select * from Employee where EmployeeID=" + id;

                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                    employee.FirstName = rdr["FirstName"].ToString();
                    employee.LastName = rdr["LastName"].ToString();
                    employee.City = rdr["City"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.IsDeleted = Convert.ToBoolean(rdr["IsDeleted"]);

                }
            }

            return employee;

        }
    }
}
