using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PetShop.Core.ApplicationService;
using PetShop.Core.ApplicationService.Implementation;
using PetShop.Core.DomainService;
using PetShop.Core.ErrorHandling;
using PetShop.Infrastructure.SQL;
using PetShop.RestAPI.Initializer;

namespace PetShop.RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Dependency Injection
            services.AddScoped<IErrorFactory, ErrorFactory>();
            services.AddScoped<IPetRepository, Infrastructure.SQL.Repositories.PetRepository>();

            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerRepository, Infrastructure.SQL.Repositories.OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.MaxDepth = 3;
            });

            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            /*
            //Authentication
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper(secretBytes));*/

            //Database setup
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<PetShopContext>(
                optionsAction: opt => opt.UseSqlite(
                    connectionString: "Data Source = PetShop.db"));
            }
            else
            {
                services.AddDbContext<PetShopContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString
                ("defaultConnection")));
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseCors("AllowSpecificOrigin");

            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider
                        .GetRequiredService<PetShopContext>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    DBInitializer.Seed(context);
                }
                app.UseDeveloperExceptionPage();
            }
            else
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider
                        .GetRequiredService<PetShopContext>();
                    if(!context.Pets.Any())
                    {
                        context.Database.EnsureCreated();
                        DBInitializer.Seed(context);
                    }
                }
                app.UseHsts();
            }

            app.UseAuthentication();

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
