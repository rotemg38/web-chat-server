using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System.Text.Json;

namespace WebAPI.Controllers
{
    public class UserForApi
    {
        public string id { get; set; }
        public string name { get; set; }
        public string server { get; set; }
    }
    public class UserForPUTApi
    {
        public string name { get; set; }
        public string server { get; set; }
    }

    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IUsersService _userContext;
        private readonly IChatsService _chatsContxt;

        public ContactsController(IUsersService userContext, IChatsService chatContext)
        {
            _userContext = userContext;
            _chatsContxt = chatContext;
        }

        /* return all contacts of coneected user */
        // GET: api/<ContactsController>
        [HttpGet]
        public string Get()
        {
            List<User> usersList = new List<User>();
            string username = HttpContext.Session.GetString("username");
            if(username == null)
            {
                return null;//return null as error that no user is connected
            }
            User connectedUser = _userContext.GetUserByUsername(username);
            List<Tuple<int, User>> chatsANdUsers = _chatsContxt.GetOtherUsers(connectedUser);
            foreach (Tuple<int, User> user in chatsANdUsers)
            {
                usersList.Add(user.Item2);
            }
            return JsonSerializer.Serialize(usersList);
        }

        /* return the details of the user with the Id "userName" */
        // GET api/<ContactsController>/user1
        [HttpGet("{userName}")]
        public IActionResult Get(string userName)
        {
            User user = _userContext.GetUserByUsername(userName);
            if (user == null)
            {
                return NotFound();
            }
            return Content(JsonSerializer.Serialize(user));
        }

        /* create new contact to connected user - add new connection between them (empty) */
        // POST api/<ContactsController>
        [HttpPost]
        public IActionResult Post([FromBody] UserForApi user)
        {
            string username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                //if no user is connected return error
                return NotFound();
            }
            User connectedUser = _userContext.GetUserByUsername(username);
            User curr = _userContext.GetUserByUsername(user.id);

            //todo: check maybe need to create the user because its says add new contact?
            if (curr == null)
            {
                return NotFound();
            }
            
            _chatsContxt.AddChat(curr, connectedUser);

            return Created("Post", user);
        }

        /* update the details of user with id "userName */
        // PUT api/<ContactsController>/user1 
        [HttpPut("{userName}")]
        public IActionResult Put(string userName, [FromBody] UserForPUTApi user)
        {
            User curr = _userContext.GetUserByUsername(userName);
            if (curr == null)
            {
                return NotFound();
            }
            curr.Name = user.name;
            curr.Server = user.server;

            //todo: check if needed to return also the user object? with code 204
            return NoContent();
        }

        /* delete the user with id "userName" from contacts list for every user connected */
        // DELETE api/<ContactsController>/user1
        [HttpDelete("{userName}")]
        public IActionResult Delete(string userName)
        {
            // delete from chats list:
            List<Tuple<int, User>> contactsChats = _chatsContxt.GetOtherUsers(_userContext.GetUserByUsername(userName));
            if (contactsChats != null)
            {
                foreach (Tuple<int, User> contact in contactsChats)
                {
                    _chatsContxt.RemoveChat(contact.Item2.Id, userName);
                }
            }

            _userContext.RemoveUser(userName); // delete from users list
            return NoContent();
        }
    }
}
