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
    [Route("api/contacts")]
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
        public string Get(string userName)
        {
            User user = _context.GetAll().Find(x => x.id == userName);
            if (user == null)
            {
                //return NotFound();
            }
            return JsonSerializer.Serialize(user); 
        }

        // POST api/<ContactsController>
        [HttpPost]
        public void Post([FromBody] User user) // [Bind("id", "name", "Password", "Image")]?
        {
            User curr = _context.GetAll().Find((curr) => { return curr.id == user.id; });
            curr.last = null;
            curr.lastdate = null;
            if(curr != null)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return; // need to be error
            }
            _context.Add(user);
        }

        // PUT api/<ContactsController>/user1 //todo: fix the last and last msg
        [HttpPut("{userName}")]
        public void Put(string userName, [FromBody] User user)
        {
            User curr = _context.GetAll().Find(x => x.id == userName);
            if (curr == null)
            {
                return;
            }
            curr.id = user.id;
            curr.Password = user.Password;
            curr.name =  user.name;
            curr.Image = user.Image;
            Message message = _context.GetLastMsg(userName);
            curr.last = message.Content;
            curr.lastdate = message.Created;
        }

        // DELETE api/<ContactsController>/user1
        [HttpDelete("{userName}")]
        public void Delete(string userName)
        {
            _context.RemoveUser(userName);
        }
    }
}
