
using DeskShareApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace DeskShareApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public IConfiguration _Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //inject MySql Connection for UserManager
            services.AddDbContext<DeskShareDbUserManager>(options =>
            options.UseMySql(_Configuration.GetConnectionString("DeskShareConnection"),
            ServerVersion.AutoDetect(_Configuration.GetConnectionString("DeskShareConnection"))));

            //inject MySql Connection for Datausage
            services.AddDbContext<DbContextDeskShare>(options =>
                options.UseMySql(_Configuration.GetConnectionString("DeskShareConnection"),
                    ServerVersion.AutoDetect(_Configuration.GetConnectionString("DeskShareConnection"))));


            // Inject Identity-Framework
            services.AddIdentity<UserIdentity, IdentityRole>()
            .AddEntityFrameworkStores<DeskShareDbUserManager>()
            .AddDefaultTokenProviders();

            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = "*",
                    ValidIssuer = "*",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Bf]R86kM+buEB3'K"))
                };
            });



            services.AddLogging();

            services.AddControllers();
    

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
           
            SeedDb.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
