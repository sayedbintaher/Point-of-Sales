using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PosAPI.Data;
using PosAPI.Repository.Implementation;
using PosAPI.Repository.Interfaces;
using PosAPI.Validator;
using PosAPI.ViewModels;

namespace PosAPI
{
    public static class Services
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            //Dapper Dependency Injection
            services.AddSingleton<DapperContext>();

            //Validator Dependecy Injection 

            //Service Registration for interfaces and classes 
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
        }
    }
}