using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Models;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WbeAPIAdvencedProgramming.Controllers
{
    //class to match the requirment of the api
    public class TmpMsg{
        public string Content { get; set; }
    }

    [Route("api/contacts/{userName}/messages")]
    public class MessagesController : Controller
    {
        private readonly UsersService _contextUsers;
        private readonly ChatsService _contextChats;
        private readonly MessagesService _contextMsg;
        private readonly MsgInChatService _contextMsgInChat;

        public MessagesController(MessagesService contextMsg, MsgInChatService contextMsgInChat, ChatsService contextChats, UsersService usersService)
        {
            _contextMsg = contextMsg;
            _contextMsgInChat = contextMsgInChat;
            _contextChats = contextChats;
            _contextUsers = usersService;
        }

        // GET: api/Contacts/:userName/Messages
        [HttpGet]
        //get all mesages of user
        public string Get(string userName)
        {
            List<Chat> chats = _contextChats.GetUserChats(userName);
            List<MsgUsers> messages = _contextMsgInChat.GetAllMessages(chats);
            List<Message> msgs = _contextMsgInChat.ExtractMessages(messages);
            List<Message> fixedMsgs = _contextMsgInChat.GetCopyWithFixedSent(userName, msgs);

            return JsonSerializer.Serialize(fixedMsgs);
        }

        // GET api/Contacts/:userName/Messages/:id
        [HttpGet("{idMsg}")]
        //get the message with the given id of the given usrname
        public string Get(string userName, int idMsg)
        {
            Message msg = _contextMsg.GetMsgById(idMsg);
            if(msg == null)
            {
                return JsonSerializer.Serialize(msg);
            }
            //todo: check if needed to check this, and if not for what we need userName?
            if(_contextMsgInChat.IsSender(userName, msg.Id)){
                return JsonSerializer.Serialize(msg);
            }

            return null;
        }

        // PUT api/Contacts/:userName/Messages/:id
        [HttpPut("{idMsg}")]
        //update the message where the id = idMsg
        //todo: ask for what we need username
        public IActionResult Put(string userName, int idMsg, [FromBody] TmpMsg content)
        {
            if (_contextMsg.UpdateMsg(idMsg, content.Content))
            {
                return Created("Put", new { content = content.Content });
            }
            return NotFound();
        }

        // DELETE api/Contacts/:userName/Messages/:id
        [HttpDelete("{idMsg}")]
        //delete the message of the user with the given id
        public IActionResult Delete(string userName, int idMsg)
        {
            List<Chat> chats = _contextChats.GetUserChats(userName);
            if(chats.Count != 0)
            {
                _contextMsgInChat.DeleteMsg(chats, idMsg);
                _contextMsg.DeleteMsg(idMsg);
            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST api/Contacts/:userName/Messages
        [HttpPost]
        //create new msg between users- currentUserName send to userName
        public IActionResult Post(string userName, [FromBody] TmpMsg content)
        {
            string currentUserName = HttpContext.Session.GetString("username");
            if (currentUserName == null)
            {
                //todo: maybe redireect signin?
                return NotFound();
            }

            User userTo = _contextUsers.GetUserByUsername(userName);
            User userFrom = _contextUsers.GetUserByUsername(currentUserName);
            if (userFrom == null || userTo == null)
            {
                return NotFound();
            }
            
            Message msg = _contextMsg.AddMsg(content.Content, true);
            Chat chat = _contextChats.GetChatByUsers(userName, currentUserName);
            
            //if this is new messag need to create chat
            if (chat == null)
            {
                chat = _contextChats.AddChat(userTo, userFrom);
            }
            MsgUsers msgUsers = _contextMsgInChat.CreatMsgUsers(userFrom, userTo, msg);
            _contextMsgInChat.AddMsgInChat(chat, msgUsers);

            return Created("Post",new { Content= msg.Content});
        }
    }
}

