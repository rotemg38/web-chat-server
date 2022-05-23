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
        private readonly UsersService _userContext;
        private readonly ChatsService _chatsContxt;

        public ContactsController (UsersService userContext, ChatsService chatContext)
        {
            _userContext = userContext;
            _chatsContxt = chatContext;
        }

        /* return all contacts of coneected user */
        // GET: api/<ContactsController>
        [HttpGet] 
        public string Get() // todo: need to be checked
        {
            List<User> usersList = new List<User> ();
            string username = HttpContext.Session.GetString(key: "username");
            User connectedUser = _userContext.GetUserByUsername(username);
            List<Tuple<int, User>> chatsANdUsers = _chatsContxt.GetOtherUsers(connectedUser);
            foreach(Tuple<int, User> user in chatsANdUsers)
            {
                usersList.Add(user.Item2);
            }
            return JsonSerializer.Serialize(usersList);
        }

        /* return the details of the user with the Id "userName" */
        // GET api/<ContactsController>/user1
        [HttpGet("{userName}")]
        public ActionResult Get(string userName)
        {
            User emptyUser = new User("", "", "");
            //User user = _userContext.GetAll().Find(x => x.Id == userName);
            User user = _userContext.GetUserByUsername(userName);
            if (user == null)
            {
                return Content(JsonSerializer.Serialize(emptyUser));
            }
            //return JsonSerializer.Serialize(user); 
            return Content(JsonSerializer.Serialize(user));
        }

        /* create new contact to connected user - add new connection between them (empty) */ 
        // POST api/<ContactsController>
        [HttpPost]
        public ActionResult Post([FromBody] UserForApi user) 
        {
            //User curr = new User(user.id, user.name, user.server);
            User curr = _userContext.GetUserByUsername(user.id);
            if(curr == null)
            {
                return NotFound();
            }

            string username = HttpContext.Session.GetString("username");
            User connectedUser = _userContext.GetUserByUsername(username);
            _chatsContxt.AddChat(curr, connectedUser);
            return Ok();
            
        }
        /* update the details of user with id "userName */
        // PUT api/<ContactsController>/user1 
        [HttpPut("{userName}")]
        public ActionResult Put(string userName, [FromBody] UserForPUTApi user)
        {
            User curr = _userContext.GetUserByUsername(userName);
            if (curr == null)
            {
                return NotFound();
            }
            curr.Name = user.name;
            curr.Server = user.server;
            return Ok();
        }

        /* delete the user with id "userName" from contacts list for every user connected */
        // DELETE api/<ContactsController>/user1
        [HttpDelete("{userName}")]
        public ActionResult Delete(string userName)
        {
            _userContext.RemoveUser(userName); // delete from users list
            // delete from chats list:
            List<Tuple<int, User>> contactsChats = _chatsContxt.GetOtherUsers(_userContext.GetUserByUsername(userName));
            if (contactsChats == null)
            {
                return NotFound();
            }
            foreach (Tuple<int, User> contact in contactsChats)
            {
                _chatsContxt.RemoveChat(contact.Item2.Id, userName);
            }
            return Ok();
        }
    }
}
