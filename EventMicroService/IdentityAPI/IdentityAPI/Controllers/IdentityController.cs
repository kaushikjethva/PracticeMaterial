using IdentityAPI.Models;
using IdentityAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace IdentityAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        private IIdentityManger idManager;        

        public IdentityController(IIdentityManger _IIdentityManger,
            IConfiguration configuration )
        {
            idManager = _IIdentityManger;            
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //Register User
        public async Task<ActionResult> Register([FromBody] User user) {

            if (!ModelState.IsValid) {
                return BadRequest();
            }
            var result = await idManager.AddUserAsync(user);
            return Created("", result);
        }

        [HttpPost("token")]
        //Lodin/Token Generation
        public ActionResult<string> Token([FromBody] Login login)
        {
            var token = idManager.ValidateUser(login);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}