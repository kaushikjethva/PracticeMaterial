using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DistributedCacheExample.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DistributedCacheExample.Controllers
{
    public class HomeController : Controller
    {
        IDistributedCache cache;
        public HomeController(IDistributedCache cache)
        {
            this.cache = cache;
        }
        public IActionResult Index()
        {
            var session = HttpContext.Items["flag"];
            ViewBag.Flag = session;
            var data = cache.Get("data");
            if (data != null)
            {
                var message = Encoding.UTF8.GetString(data);
                ViewBag.Message = data;
            }
            else
            {
                ViewBag.Message = "Nothing in Cashe";
                var text = "This is Sample text data";
                var value = Encoding.UTF8.GetBytes(text);
                cache.Set("data", value);
            }

            var msg = Encoding.UTF8.GetBytes("This is Session Data");
            HttpContext.Session.Set("message", msg);
            HttpContext.Session.SetInt32("count", 100);
            HttpContext.Session.SetString("name", "Apple");
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

        public IActionResult Privacy()
        {
            var data = HttpContext.Session.Get("message");
            ViewBag.Message = Encoding.UTF8.GetString(data);

            var count = HttpContext.Session.Get("count");
            ViewBag.count = Convert.ToInt32(Encoding.UTF8.GetString(count));

            var name = HttpContext.Session.Get("name");
            ViewBag.name = Convert.ToInt32(Encoding.UTF8.GetString(name));

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
