using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Services;
using Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;

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

        // GET api/<ContactsController>/user1
        [HttpGet("{userName}")]
        public string Get(string userName) // maybe id?
        {
            User user = _context.GetAll().Find(x => x.UserName == userName);
            if (user == null)
            {
                //return NotFound();
            }
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

        // PUT api/<ContactsController>/user1
        [HttpPut("{UserName}")]
        public void Put(string userName, [FromBody] User user)
        {
            User curr = _context.GetAll().Find((curr) => { return curr.UserName == userName; });
            if (curr == null)
            {
                return;
            }
            curr.UserName = user.UserName;
            curr.Password = user.Password;
            curr.DisplayName =  user.DisplayName;
            curr.Image = user.Image;
        }

        // DELETE api/<ContactsController>/user1
        [HttpDelete("{userName}")]
        public void Delete(string userName)
        {
            _context.RemoveUser(userName);
        }
    }
}
