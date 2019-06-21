using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebAppConfiguration1.Models;

namespace WebAppConfiguration1.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _config;
        DeveloperSettings settings;
             
        public HomeController(IConfiguration config,IOptions<DeveloperSettings> options)
        {
            this._config = config;
            this.settings = options.Value;
        }

        public IActionResult Index([FromServices]IConfiguration _configuration)
        {
            
            var configuration = HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            //ViewBag.Name = _config.GetValue<string>("Developer:Name");
            ViewBag.Name = settings.Address.City;
            return View();
        }
        //public IActionResult Index([FromServices]IConfiguration _configuration)
        //{
        //    //ViewBag.Name = _config.GetValue<string>("Developer:Name");
        //    var configuration = HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
        //    ViewBag.Name = _config.GetValue<string>("Developer:Name");
        //    ViewBag.Name1 = _configuration.GetValue<string>("Developer:Name");
        //    ViewBag.Name2 = configuration.GetValue<string>("Developer:Name");
        //    return View();
        //}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
