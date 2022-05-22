using Microsoft.AspNetCore.Mvc;
using Services;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WbeAPIAdvencedProgramming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private readonly UsersService _context;

        public RegisterController(UsersService context)
        {
            _context = context;
        }

        // GET api/<RegisterController>/5
        [HttpGet("{username}/{name}/{image}/{password}")]
        public IActionResult Get(string username, string name, string image, string password)
        {
            User user = new User(username, name, password, image, "localhost:5000");
            
            HttpContext.Session.SetString("username", user.Id);
            _context.Add(user);
            return Ok();
        }

        // POST api/<RegisterController>
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

        // PUT api/<RegisterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegisterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
