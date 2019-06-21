using Autofac;
using Alexinea.Autofac.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Http;

namespace DependancyInjectionDemo
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
            //services.AddSingleton<IDataManager,SqlDataManager>();
            services.AddScoped<IDataManager, MongoDataManager>();
            //services.AddTransient<IDataManager,SqlDataManager>();
            services.AddMvc();
        }

        // public IServiceCollection ConfigureServices(IServiceCollection services)
        //{
        //    //services.Configure<CookiePolicyOptions>(option=>
        //    //{
        //    //    option.CheckConsentNeeded = context => true;
        //    //    option.MinimumSameSitePolicy = SameSiteMode.None;
        //    //});            
        //    services.AddMvc()
        //        //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        //    var ContainerBuilder = new ContainerBuilder();
        //    ContainerBuilder.RegistrationModule<AutofactModule>();
        //    ContainerBuilder.Populate<services>();
        //    var container = ContainerBuilder.Build();
        //    return new AutofacServiceProvider(container);
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
