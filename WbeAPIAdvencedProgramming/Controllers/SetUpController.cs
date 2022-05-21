using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WbeAPIAdvencedProgramming.Controllers
{
    [Route("api/setup")]
    [ApiController]
    public class SetUpController : Controller
    {

        private readonly UsersService _context;

        public SetUpController(UsersService context)
        {
            _context = context;
        }

        // POST api/setup/login
        [HttpPost("login")]
        public IActionResult PostLogin([FromBody] User user)
        {
            if (user == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("username", user.Id);
            return Ok();

        }

        // POST api/setup/register
        [HttpPost("register")]
        public IActionResult PostRegister([FromBody] User user)
        {
            if(user == null)
            {
                return NotFound();
            }
            
            HttpContext.Session.SetString("username", user.id);
            _context.Add(user);
            return Ok();
        }

    }
}

