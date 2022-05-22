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

        // Get api/setup/{userName}
        // get for login
        [HttpGet("{userName}")]
        public IActionResult Get(string userName)
        {
            User user = _context.GetUserByUsername(userName);
            if (user == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("username", user.Id);
            return Ok();

        }

        // POST api/setup
        // post for register
        /*[HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("username", user.Id);
            _context.Add(user);
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
            
            HttpContext.Session.SetString("username", user.Id);
            _context.Add(user);
            return Ok();
        }
        
        [HttpGet("register/{userName}/{name}")]
        public IActionResult GetRegister(string userName, string name)
        {
            //User user = _context.GetUserByUsername(userName);
            if (userName == null)
            {
                return NotFound();
            }
            User user = new User(userName, name, "localhost:5000");
            HttpContext.Session.SetString("username", user.Id);
            return Ok();

        }*/
    }
}

