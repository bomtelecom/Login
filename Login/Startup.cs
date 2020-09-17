using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Login.Helpers;
using Login.Services;
using Login.Entities;
using Microsoft.EntityFrameworkCore;
using Login.Migrations;
using AutoMapper;
using System.Reflection;

namespace Login
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<UserContext>(options =>
         options.UseSqlServer(Configuration.GetConnectionString("Db")));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddCors();
            services.AddControllers();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //DatabaseInitializer.Initialize(app);
            
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
