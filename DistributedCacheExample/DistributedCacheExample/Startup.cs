using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Caching.;

namespace DistributedCacheExample
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.AddDistributedMemoryCache()

            //services.AddDistributedSqlServerCache(config =>
            //{
            //    config.ConnectionString = "Data Source=PULT-TR04-03;Initial Catalog=cachedb;Integrated Security=True";
            //    config.SchemaName = "dbo";
            //    config.TableName = "CachedTable";
            //});

            //services.AddStackExchangeRedisCache(config = => {
            //    config.Configuration = Configuration.GetValue<string>("Redis:host");
            //    config.InstanceName = Configuration.GetValue<string>("Redis:name");
            //})

           
            services.AddScoped〈IEventManager, EventManager〉();

            services.AddSession(options =>
            {
                options.Cookie.Name = "ASPDOTCOOKIESESSION";
                options.Cookie.MaxAge = TimeSpan.FromDays(5);
                options.IdleTimeout = TimeSpan.FromMinutes(15);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                context.Items["flag"] = $"Verify by Middleware {DateTime.Now}";
                await next.Invoke();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
