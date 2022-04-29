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
    public class SalaryByDepartment
    {
        private readonly ILogger logger;
        public SalaryByDepartment(ILogger<SalaryByDepartment> log)
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

        private List<Employee> SalaryByDepartment_withCheff()
        {
            List<Employee> employees = new List<Employee>();

            string storedProcedureNameInSql = "SalaryByDepartment_withCheff";

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
                    logger.LogError(ex, "Method 'SalaryByDepartment_withCheff'");
                    return employees;
                }
            }
        }

        private List<Employee> SalaryByDepartment_withoutCheff()
        {
            List<Employee> employees = new List<Employee>();

            string storedProcedureNameInSql = "SalaryByDepartment_withoutCheff";

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
                                department.Id = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                                department.Name = reader.IsDBNull(2) ? "" : reader.GetString(2);

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
                    logger.LogError(ex, "Method 'SalaryByDepartment_withoutCheff'");
                    return employees;
                }
            }
        }

        public void GetSalaryByDepartment()
        {
            logger.LogInformation("Call method 'SalaryByDepartment_withCheff'");

            var employeesList_SalaryByDepartment_WithCheff = new List<Employee>();
            employeesList_SalaryByDepartment_WithCheff = SalaryByDepartment_withCheff();

            Console.WriteLine("Salary by department (with cheff):");
            Console.WriteLine($"{"Salary",-10} {"Chief_Id",-10} {"Department",-10}");

            foreach (Employee employee in employeesList_SalaryByDepartment_WithCheff)
                Console.WriteLine($"{employee.Salary,-10} {employee.Chief_Id,-10} {employee.Department.Name,-10}");


            logger.LogInformation("Call method 'SalaryByDepartment_withoutCheff'");

            var employeesList_SalaryByDepartment_WithoutCheff = new List<Employee>();
            employeesList_SalaryByDepartment_WithoutCheff = SalaryByDepartment_withoutCheff();

            Console.WriteLine("Salary by department (without cheff):");
            Console.WriteLine($"{"Salary",-10} {"Department",-10}");

            foreach (Employee employee in employeesList_SalaryByDepartment_WithoutCheff)
                Console.WriteLine($"{employee.Salary,-10} {employee.Department.Name,-10}");
        }
    }
}
