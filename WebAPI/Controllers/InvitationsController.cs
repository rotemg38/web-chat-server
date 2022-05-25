using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using System.Text.Json;

namespace WebAPI.Controllers
{
    public class InvitaionApi
    {
        public string from { get; set; }
        public string to { get; set; }
        public string server { get; set; }
    }

    public class InvitationsController : Controller
    {
        private readonly ChatsService _chatContext;
        private readonly UsersService _userContext;
        private readonly string _myServerName = "localhost:5067";

        public InvitationsController(ChatsService chatContext, UsersService userContext)
        {
            _chatContext = chatContext;
            _userContext = userContext;
        }

        /* create an invitain (new chat) between 2 users */
        [HttpPost]
        [Route("api/invitations")]
        public ActionResult Invitations([FromBody] InvitaionApi invitaion) // todo: we need to check if useres exists?
        {
            User from = _userContext.GetUserByUsername(invitaion.from);
            User to = _userContext.GetUserByUsername(invitaion.to);
            if (_userContext.GetUserByUsername(invitaion.to) == null)
            {
                return NotFound();
            }
            if (_userContext.GetUserByUsername(invitaion.from) == null)
            {
                User newUser = new User(invitaion.from, invitaion.from, invitaion.server);
                _userContext.Add(newUser);
                from = newUser;
            }
            _chatContext.AddChat(from, to);
            return NoContent();
        }
    }
}
