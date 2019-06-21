using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventAPI.Models;
using EventAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers
{
    //Below attribute required when we need data as per thr requestd at client location
    [FormatFilter]
    //Provide the below tag for content negotition at controller levle
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("AllowAll")]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private IEventRepository<EventData> eventRepository;
        public EventsController(IEventRepository<EventData> eventRepo)
        {
            eventRepository = eventRepo;

        }
        //public string GetMessage()
        //{
        //    return "Hello WebAPI";
        //}

        //Get /api/events
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EventData>> GetEvents()
        {            
            var events = eventRepository.GetAll();
            return events.ToList();
        }

        //Provide the {format}? in order to get the response on request based format type in url like .xml or .json
        [HttpGet("{id}.{format?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EventData> GetById([FromRoute]int id)
        {
            //throw new InvalidOperationException("InvalidOperationException Occured");
            var item = eventRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventData>> AddAsync([FromBody]EventData ev)
        {
            //try
            //{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                //throw new Exception("Some error message");
                var result = await eventRepository.AddAsync(ev);
                return Created($"/api/events/{result.Id}", result);
                //return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            //}
            //catch (Exception ex)
            //{
            //    var error =
            //    new
            //    {
            //        Message = ex.Message
            //    };
            //    return StatusCode(StatusCodes.Status500InternalServerError, error);
            //}
        }
    }
}