using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using MVCApp.Repositories;
using MVCApp.Services;

namespace MVCApp.Controllers
{
    [Route("events")]
    public class EventInfoController : Controller
    {
        private IEventRepository<EventData> _Repo;
        private IEventRepository<EventRegister> _RepoEventRegister;
        public EventInfoController(IEventRepository<EventData> Repo,
            IEventRepository<EventRegister> RepoEventRegister)
        {
            _Repo = Repo;
            _RepoEventRegister = RepoEventRegister;
        }
        [HttpGet("",Name ="EventList")]
        public IActionResult Index()
        {
            var events = _Repo.GetAll();
            return View(events);
        }

        [HttpGet("new", Name = "NewEvent")]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost("new", Name = "NewEvent")]
        public async Task<IActionResult> Create(EventData eventData)
        {
            if (ModelState.IsValid)
            {
                await _Repo.Add(eventData);
                return RedirectToRoute("EventList");
            }
            else
            {
                return View();
            }
        }

        [HttpGet("eventregister", Name = "EventRegister")]
        public IActionResult EventRegister(int id, string title)
        {
            EventRegister eventRegister = new EventRegister()
            {
                EventId = id
            };

            ViewBag.EventName = title;

            return View(eventRegister);
        }

        [HttpPost("eventregister", Name = "")]
        public async Task<IActionResult> EventRegister(EventRegister eventRegister)
        {
            if (ModelState.IsValid)
            {
                await _RepoEventRegister.Add(eventRegister);
                return RedirectToRoute("RegisterList");
            }
            else
            {
                return View();
            }
        }

        [HttpGet("registerlist", Name = "RegisterList")]
        public IActionResult RegisterList()
        {
            var events = _RepoEventRegister.GetAll();
            return View(events);
        }
    }
}