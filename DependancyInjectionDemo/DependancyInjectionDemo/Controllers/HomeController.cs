using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DependancyInjectionDemo.Models;
using DependencyInjectionDemo.Services;
using DependancyInjectionDemo;

namespace DependancyInjectionDemo.Controllers
{
    public class HomeController : Controller
    {
        IDataManager dm;
        public HomeController(IDataManager manager)
        {
            this.dm = manager;
        }
        public IActionResult Index()
        {
            ViewBag.Message = dm.GetMessage();
            return View();
        }

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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
