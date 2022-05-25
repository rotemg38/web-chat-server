using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/setup")]
    [ApiController]
    public class SetUpController : Controller
    {

        private readonly IUsersService _contextUser;

        public SetUpController(IUsersService context)
        {
            _contextUser = context;
        }

        // Get api/setup/{userName}
        // get for login
        [HttpGet("{userName}")]
        //this function save the logged user from login page
        public IActionResult Get(string userName)
        {
            User user = _contextUser.GetUserByUsername(userName);
            if (user == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("username", user.Id);
            return Ok();

        }

        // POST api/setup/register
        [HttpPost("register")]
        //this function save the new user from register page and set him as logged in
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return NotFound();
            }
            //if user allreadt exists we cant enroll him
            if (_contextUser.UserIsExists(user.Id))
            {
                return NotFound();
            }
            _contextUser.Add(user);
            HttpContext.Session.SetString("username", user.Id);
            return Ok();
        }

        [HttpGet("connectedUser")]
        //get the connected user
        public IActionResult Get()
        {
            var user = HttpContext.Session.GetString("username");
            return Content(user);
        }

        [HttpGet("disConnectUser")]
        public IActionResult DisConnectUser()
        {
            HttpContext.Session.Clear();
            return Ok();
        }

    }
}

