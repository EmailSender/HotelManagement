using DataAccess.Implementation;
using DataAccess.Interface;
using HJotelManagement.DataAccess.Implementation;
using HJotelManagement.DataAccess.Interface;
using HJotelManagement.DataLayer;
using HJotelManagement.Infrastructure.EmailService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJotelManagement
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


            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBookRoomRepository, BookRoomRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>(); 
            services.AddScoped<IEmailService, EmailService>(); 


            services.AddDbContextPool<HotelContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
                 , options => options.EnableRetryOnFailure(
                   maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
