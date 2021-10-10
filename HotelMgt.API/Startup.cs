using HotelMgt.API.Extensions;
using HotelMgt.Core;
using HotelMgt.Core.interfaces;
using HotelMgt.Core.Utilities;
using HotelMgt.Data;
using HotelMgt.Data.Seeder;
using HotelMgt.Models;
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

            services.AddControllers(setupAction => {
                setupAction.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters(); // to support XML media type

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelMgt.API", Version = "v1" });
            });

            

            // Configure Identity
            services.ConfigureIdentity();

            // Add JWT Authentication and Authorization
            services.ConfigureAuthentication();

            services.AddMvc();

            // Configure AutoMapper
            services.AddAutoMapper(typeof(ModelMaps));

            // Configure dependency injection
            services.AddDependencyInjection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            HotelMgtDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelMgt.API v1"));
            }

            HotelMgtSeeder.SeedData(dbContext, userManager, roleManager).Wait();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
