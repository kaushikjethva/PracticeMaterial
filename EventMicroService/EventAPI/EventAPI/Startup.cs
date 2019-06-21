using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAPI.CustomFormatter;
using EventAPI.Infrastructure;
using EventAPI.Models;
using EventAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace EventAPI
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
            services.AddDbContext<EventDBContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
            });

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info()
                {
                    Title = "Event Management API",
                    Version = "1.0",
                    Contact = new Contact { Name = "Kaushik Jethva", Email = "jethvakaushik@gmail.com" },
                    Description = "This api gives the functions for adding, querying, updating and deleting events"
                });
            });

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(config =>
                {
                    config.AllowAnyOrigin()
                        .WithMethods("GET")
                        .WithHeaders();
                });
                option.AddPolicy("AllowAll", config =>
                {

                    config.AllowAnyOrigin() //Allow All Domain (*)
                    .AllowAnyMethod()       //Allow all HTTP methods
                    .AllowAnyHeader();        //All HTTP headers})
                });
                option.AddPolicy("AllowPartner", config =>
                {

                    config.WithOrigins("*.synergetics.com", "*.microsoft.com")
                .WithMethods("GET")
                .WithHeaders("Content-Type", "Authrization", "Accept");
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetValue<string>("jwt:Issuer"),
                        ValidAudience = Configuration.GetValue<string>("jwt:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("jwt:Secret")))
                    };
                });

            services.AddScoped<IEventRepository<EventData>, EventRepository<EventData>>();
            services.AddScoped<IEventRepository<EventRegister>, EventRepository<EventRegister>>();

            services.AddMvc(option => {
                option.OutputFormatters.Insert(0, new CsvOutputFormatter());
            })
                //.AddXmlDataContractSerializerFormatters()   //XmlDataContractSerializer
                //.AddXmlSerializerFormatters()     //
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Server side exception - 500 - Server exception occurs.
                //app.ConfigurationExceptionHandler();
            }

            app.UseAuthentication();

            app.UseCors("AllowAll");
            //Custom poicy
            //app.UseCors(config =>
            //{

            //    config.AllowAnyOrigin() //Allow All Domain (*)
            //    .AllowAnyMethod()       //Allow all HTTP methods
            //    .AllowAnyHeader();        //All HTTP headers

            //    config.WithOrigins("*.synergetics.com", "*.microsoft.com")
            //    .WithMethods("GET")
            //    .WithHeaders("Content-Type", "Authrization", "Accept");
            //    config.WithOrigins("*.hexawarriors.com")
            //    .AllowAnyMethod()
            //    .AllowAnyMethod();

            //});
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Management API");
                option.RoutePrefix = "";
            });

            //Client side Exceptions - 404
            //app.UseStatusCodePages();
            //app.UseStatusCodePages("application/json", "'Message':'{0}, Client side error'");

            app.UseMvc();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var database = serviceScope.ServiceProvider.GetService<EventDBContext>().Database;
                database.Migrate();
            }        
        }
    }
}
