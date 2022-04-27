using System;
using System.Collections.Generic;
using SalaryDataConsoleApp.GetDataFromDb;
using SalaryDataConsoleApp.Models;


namespace SalaryDataConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetSalaryByDepartment();
            Console.WriteLine("---------------------");

            GetSalaryDepartmentHeads();
            Console.WriteLine("---------------------");

            GetDepartmentWithMaxSalary();

        }

        public static void GetSalaryByDepartment()
        {
            var salaryByDepartment = new SalaryByDepartment();

            var employeesList_SalaryByDepartment_WithCheff = new List<Employee>();
            employeesList_SalaryByDepartment_WithCheff = salaryByDepartment.SalaryByDepartment_withCheff();

            Console.WriteLine("Salary by department (with cheff):");
            Console.WriteLine($"{"Salary",-10} {"Chief_Id",-10} {"Department",-10}");
            foreach (Employee employee in employeesList_SalaryByDepartment_WithCheff)
                Console.WriteLine($"{employee.Salary, -10} {employee.Chief_Id, -10} {employee.Department.Name, -10}");


            var employeesList_SalaryByDepartment_WithoutCheff = new List<Employee>();
            employeesList_SalaryByDepartment_WithoutCheff = salaryByDepartment.SalaryByDepartment_withoutCheff();

            Console.WriteLine();
            Console.WriteLine("Salary by department (without cheff):");
            Console.WriteLine($"{"Salary",-10} {"Department",-10}");
            foreach (Employee employee in employeesList_SalaryByDepartment_WithoutCheff)
                Console.WriteLine($"{employee.Salary, -10} {employee.Department.Name, -10}");
        }

        public static void GetSalaryDepartmentHeads()
        {
            var salaryDepartmentHeads = new SalaryDepartmentHeads();

            var employeesList_SalaryDepartmentHeads = new List<Employee>();
            employeesList_SalaryDepartmentHeads = salaryDepartmentHeads.SalaryDepartmentHeads_Desc();

            Console.WriteLine("Salary of department heads (desc):");
            Console.WriteLine($"{"Salary",-10} {"Chief_Id",-10} {"Department",-10}");
            foreach (Employee employee in employeesList_SalaryDepartmentHeads)
                Console.WriteLine($"{employee.Salary,-10} {employee.Chief_Id,-10} {employee.Department.Name,-10}");
        }

        public static void GetDepartmentWithMaxSalary()
        {
            var departmentWithMaxSalary = new DepartmentWithMaxSalary();

            var employeesList_DepartmentWithMaxSalary = new List<Employee>();
            employeesList_DepartmentWithMaxSalary = departmentWithMaxSalary.DepartmentWithHighestSalary();

            Console.WriteLine("Department with highest salary:");
            Console.WriteLine($"{"Department",-10} {"Salary",-10} {"Name",-10}");
            foreach (Employee employee in employeesList_DepartmentWithMaxSalary)
                Console.WriteLine($"{employee.Department.Name, -10} {employee.Salary,-10} {employee.Name,-10}");
        }        
    }
}
