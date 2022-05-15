using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Services;
using Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly UsersService _context;

        public ContactsController (UsersService context)
        {
            _context = context;
        }

        // GET: api/<ContactsController>
        [HttpGet]
        public string Get()
        {
            List<User> users = _context.GetAll();
            return JsonSerializer.Serialize(users);



        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public string Get(string userName) // maybe id?
        { 
            User user = _context.GetAll().Find((user) => { return user.UserName == userName; });
            return JsonSerializer.Serialize(user); 
        }

        // POST api/<ContactsController>
        [HttpPost]
        public void Post([FromBody] User user) // [Bind("UserName", "DisplayName", "Password", "Image")]?
        {
            User curr = _context.GetAll().Find((user) => { return user.UserName == user.UserName; });
            if(curr != null)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return; // need to be error
            }
            _context.Add(user);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
