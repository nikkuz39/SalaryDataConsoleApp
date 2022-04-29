using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using SalaryDataConsoleApp.GetDataFromDb;

namespace SalaryDataConsoleApp.Services
{
    public class AppLog
    {
        public void Logger()
        {
            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                var config = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();


                using var servicesProvider_DepWithMaxSalary = new ServiceCollection()
                    .AddTransient<DepartmentWithMaxSalary>()
                    .AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
                        loggingBuilder.AddNLog(config);
                    }).BuildServiceProvider();

                var depWithMaxSalary = servicesProvider_DepWithMaxSalary.GetRequiredService<DepartmentWithMaxSalary>();
                depWithMaxSalary.GetDepartmentWithMaxSalary();


                using var servicesProvider_SalaryByDep = new ServiceCollection()
                    .AddTransient<SalaryByDepartment>()
                    .AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
                        loggingBuilder.AddNLog(config);
                    }).BuildServiceProvider();

                var salaryByDep = servicesProvider_SalaryByDep.GetRequiredService<SalaryByDepartment>();
                salaryByDep.GetSalaryByDepartment();


                using var servicesProvider_SalaryDepHeads = new ServiceCollection()
                    .AddTransient<SalaryDepartmentHeads>()
                    .AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
                        loggingBuilder.AddNLog(config);
                    }).BuildServiceProvider();

                var salaryDepHeads = servicesProvider_SalaryDepHeads.GetRequiredService<SalaryDepartmentHeads>();
                salaryDepHeads.GetSalaryDepartmentHeads();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}