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
    [Route("api/contacts/{userName}/messages")]
    public class MessagesController : Controller
    {
        //todo: handle errors- msg not found\ user not exists? check if need?
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
            return JsonSerializer.Serialize(messages);
        }

        // GET api/Contacts/:userName/Messages/:id
        [HttpGet("{idMsg}")]
        //todo: ask username what is the connection??
        public string Get(string userName, int idMsg)
        {
            Message msg = _contextMsg.GetMsgById(idMsg);
            return JsonSerializer.Serialize(msg);
        }

        // PUT api/Contacts/:userName/Messages/:id
        [HttpPut("{idMsg}")]
        //update the message with the id = idMsg
        public void Put(string userName, int idMsg, [FromBody] Message message)
        {
            message.Id = idMsg;
            _contextMsg.UpdateMsg(message);
        }

        // DELETE api/Contacts/:userName/Messages/:id
        [HttpDelete("{idMsg}")]
        //delete the message of the user with the given id
        public void Delete(string userName, int idMsg)
        {
            List<Chat> chats = _contextChats.GetUserChats(userName);
            _contextMsgInChat.DeleteMsg(chats, idMsg);
            _contextMsg.DeleteMsg(idMsg);
        }

        // POST api/Contacts/:userName/Messages/:currentUser 
        [HttpPost("{currentUserName}")] 
        //create new msg between users- current send to userName
        public void Post(string userName, string currentUserName, [FromBody] Message msg)
        {
            int idMsg = _contextMsg.AddMsg(msg);
            msg.Id = idMsg;
            Chat chat = _contextChats.GetChatByUsers(userName, currentUserName);
            User userTo = null;
            User userFrom = null;
            //if this is new messag need to create chat
            if (chat != null)
            {
                userTo = _contextUsers.GetUserByUsername(userName);
                userFrom = _contextUsers.GetUserByUsername(currentUserName);
                chat = _contextChats.AddChat(userTo, userFrom);
            }
            MsgUsers msgUsers = _contextMsgInChat.CreatMsgUsers(userFrom, userTo, msg);
            _contextMsgInChat.AddMsgInChat(chat, msgUsers);
            
        }

    }
}

