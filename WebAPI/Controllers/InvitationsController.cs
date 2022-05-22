using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Models;

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

        public InvitationsController(ChatsService chatContext, UsersService userContext)
        {
            _chatContext = chatContext;
            _userContext = userContext;
        }

        /* create an invitain (new chat) between 2 users */
        [HttpPost]
        [Route("invitations")]
        public ActionResult Invitations([FromBody] InvitaionApi invitaion) // todo: we need to check if useres exists?
        {
            // check if invitaion is from another Server user. if so - we need to add it to our list.
            // if user "from" is exists (not null) then the "to" user is from another Server, and the oppsite.
            if (invitaion.server != "localhost: 5000" && _userContext.GetUserByUsername(invitaion.from) != null)
            {
                User newUser = new User(invitaion.to, "", invitaion.server);
                _userContext.Add(newUser);
                _chatContext.AddChat(newUser, _userContext.GetUserByUsername(invitaion.from));

            }
            else if (invitaion.server != "localhost: 5000" && _userContext.GetUserByUsername(invitaion.to) != null)
            {
                User newUser = new User(invitaion.from, "", invitaion.server);
                _userContext.Add(newUser);
                _chatContext.AddChat(newUser, _userContext.GetUserByUsername(invitaion.to));
            }
            else if (_userContext.GetUserByUsername(invitaion.from) == null && _userContext.GetUserByUsername(invitaion.to) == null)
            {
                return NotFound();
            }
            return Ok();
        }



    }
}
