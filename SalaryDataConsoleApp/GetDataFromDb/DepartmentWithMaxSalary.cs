using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SalaryDataConsoleApp.Models;

namespace SalaryDataConsoleApp.GetDataFromDb
{
    public class DepartmentWithMaxSalary
    {
        private String GetDatabaseConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

            return configuration.GetConnectionString("DefaultConnection");
        }

        public List<Employee> DepartmentWithHighestSalary()
        {
            List<Employee> employees = new List<Employee>();

            string storedProcedureNameInSql = "DepartmentWithHighestSalary";

            using (SqlConnection connection = new SqlConnection(GetDatabaseConnectionString()))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureNameInSql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee();
                                Department department = new Department();

                                department.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                department.Name = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                employee.Salary = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                employee.Name = reader.IsDBNull(3) ? "" : reader.GetString(3);

                                employee.Department = department;

                                employees.Add(employee);
                            }
                            reader.Close();
                        }
                        return employees;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return employees;
                }
            }
        }
    }
}
