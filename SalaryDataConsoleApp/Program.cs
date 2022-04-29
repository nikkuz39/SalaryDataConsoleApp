using System;
using System.Collections.Generic;
using SalaryDataConsoleApp.GetDataFromDb;
using SalaryDataConsoleApp.Models;
using SalaryDataConsoleApp.Services;

namespace SalaryDataConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logger = new AppLog();
            logger.Logger();
        }        
    }
}
