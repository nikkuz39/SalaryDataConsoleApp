using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SalaryDataConsoleApp.Models;

namespace SalaryDataConsoleApp.GetDataFromDb
{
    public class SalaryDepartmentHeads
    {
        private readonly ILogger logger;
        public SalaryDepartmentHeads(ILogger<SalaryDepartmentHeads> log)
        {
            logger = log;
        }

        private String GetDatabaseConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

            return configuration.GetConnectionString("DefaultConnection");
        }

        private List<Employee> SalaryDepartmentHeads_Desc()
        {
            List<Employee> employees = new List<Employee>();

            string storedProcedureNameInSql = "SalaryDepartmentHeads_Desc";

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

                                employee.Salary = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                employee.Chief_Id = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                                department.Id = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                department.Name = reader.IsDBNull(3) ? "" : reader.GetString(3);

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
                    logger.LogError(ex, "Method 'SalaryDepartmentHeads_Desc'");
                    return employees;
                }
            }
        }

        public void GetSalaryDepartmentHeads()
        {
            logger.LogInformation("Call method 'SalaryDepartmentHeads_Desc'");

            var employeesList_SalaryDepartmentHeads = new List<Employee>();
            employeesList_SalaryDepartmentHeads = SalaryDepartmentHeads_Desc();

            Console.WriteLine("Salary of department heads (desc):");
            Console.WriteLine($"{"Salary",-10} {"Chief_Id",-10} {"Department",-10}");

            foreach (Employee employee in employeesList_SalaryDepartmentHeads)
                Console.WriteLine($"{employee.Salary,-10} {employee.Chief_Id,-10} {employee.Department.Name,-10}");
        }
    }
}
