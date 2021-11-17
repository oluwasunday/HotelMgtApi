using FluentValidation.AspNetCore;
using HotelMgt.API.Extensions;
using HotelMgt.Core;
using HotelMgt.Core.interfaces;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.Services.implementations;
using HotelMgt.Core.Utilities;
using HotelMgt.Data;
using HotelMgt.Data.Seeder;
using HotelMgt.Models;
using HotelMgt.Utilities.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace HotelMgt.API
{
    public class Startup
    {
        public static IConfiguration StaticConfig { get; private set; }
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            StaticConfig = configuration;
            Environment = environment;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            
            // configure entityframeworkcore with PostgreSQL database connection
            services.AddDbContext<HotelMgtDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConfiguration"))
                );

            // configure mail service
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();

            services.AddControllers(setupAction => {
                setupAction.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters(); // to support XML media type

            /*services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );*/


            // configure CORS for mail service
            services.AddCors(cors =>
            {
                cors.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            // configure swagger
            services.AddSwagger();


            // Configure Identity
            services.ConfigureIdentity();

            // Add JWT Authentication and Authorization
            services.ConfigureAuthentication();

            services.AddMvc();

            // Configure AutoMapper
            services.AddAutoMapper(typeof(ModelMaps));

            // Configure dependency injection
            services.AddDependencyInjection();

            // Configure cloudinary
            services.AddCloudinary(CloudinaryServiceExtension.GetAccount(Configuration));

            services.AddRazorPages();

            services.AddMvc().AddFluentValidation(fv => {
                fv.DisableDataAnnotationsValidation = false;
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                fv.ImplicitlyValidateChildProperties = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            HotelMgtDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelMgt.API v1"));
            

            HotelMgtSeeder.SeedData(dbContext, userManager, roleManager).Wait();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
