using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using CustomPortalV2.DataAccessLayer;
using CustomPortalV2.Business;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.DataAccessLayer.Repository;
using CustomPortalV2.Business.Service;
using CustomPortalV2.Business.Concrete;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using MySqlConnector;

namespace CustomPortalV2.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();
            //services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            // {           builder.WithOrigins("http://localhost:3000/", "https://localhost:7232")
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader();
            // }));


 
            /*
            services.AddDbContextPool<DBContext>(options =>
             options.UseMySql(ServerVersion.AutoDetect(connection), options => options.EnableRetryOnFailure()));
            */
            //5.7.27
            services.AddDbContextPool<DBContext>(
                options => options.UseMySql(Configuration.GetConnectionString("DBConnection"), ServerVersion.Parse("5.7.27", ServerType.MySql), null));


            services.AddScoped<ISalePackageService, SalePackageService>();
            services.AddScoped<IAppLangService, AppLangService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IParamService, ParamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IFormDefinationService, FormDefinationService>();
            services.AddScoped<IFormMetaDataService, FormMetaDataService>();
            services.AddScoped<ICompanyDefinationService, CompanyDefinationService>();
            services.AddScoped<IFormDefinationAttachmentService, FormDefinationAttachmentService>();




            services.AddScoped<ICompanyDefinationRepository, CompanyRepository>();
            services.AddScoped<IAppLangRepository, AppLangRepository>();

            services.AddScoped<IParamRepository, ParamRepository>();
            services.AddScoped<ILoginrequestLogRepository, LoginrequestLogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IFormDefinationRepository, FormDefinationRepository>();
            services.AddScoped<ICompanyAdresDefinationRepository, CompanyDefinationRepository>();
            services.AddScoped<IFormMetaDataRepository, FormMetaDataRepository>();


            

            services.AddMemoryCache();

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCors(options => options.AllowAnyOrigin());

        }
    }
}
