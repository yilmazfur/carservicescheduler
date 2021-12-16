using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarServiceSchedule.Model;
using CarServiceSchedule.Repositories;
using CarServiceSchedule.Business;
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

namespace CarServiceSchedule
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
            services.AddControllers();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarFeatureRepository, CarFeatureRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDemandRepository, DemandRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ICarFeatureBusiness, CarFeatureBusiness>();
            services.AddScoped<IDemandBusiness, DemandBusiness>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarServiceSchedule", Version = "v1" });
            });
            services.AddDbContext<AppDbContext>(o =>
            //  o.UseSqlite(Configuration.GetConnectionString("CarServiceScheduleSqlite")));
            o.UseSqlServer(Configuration.GetConnectionString("DockerSQLServer")));


            services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarServiceScheduleAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
