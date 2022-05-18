using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Models;

namespace WbeAPIAdvencedProgramming.Controllers
{
    public class InvitationsController : Controller
    {
        private readonly ChatsService _chatContext;
        private readonly UsersService _userContext;

        public InvitationsController(ChatsService chatContext, UsersService userContext)
        {
            _chatContext = chatContext;
            _userContext = userContext;
        }



        
        [HttpPost]
        [Route("invitations")]
        public void Invitations([FromBody] string from, [FromBody] string to, [FromBody] string server) // todo: we need to check if useres exists?
        {
            // check if invitaion is from another server user. if so - we need to add it to our list.
            // if user "from" is exists (not null) then the "to" user is from another server, and the oppsite.
            if (server != "localhostShirRotem" && _userContext.GetUserByUsername(from) != null)
            {
                _userContext.Add(new Models.User {id = to , server = server });
            } else if(server != "localhostShirRotem" && _userContext.GetUserByUsername(to) != null)
            {
                _userContext.Add(new Models.User { id = from, server = server });
            }
            else if (_userContext.GetUserByUsername(from) == null && _userContext.GetUserByUsername(to) == null)
            {
                return; //// ? not found
            }
           Chat chat =  _chatContext.AddChat(new Models.User { id = from}, new Models.User { id= to});
        }

        

    }
}
