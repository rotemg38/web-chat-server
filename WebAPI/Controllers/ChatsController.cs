using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{

    [Route("api/chats")]
    public class ChatsController : Controller
    {
        private readonly ChatsService _contextChats;
        private readonly MsgInChatService _contextMsgInChat;

        public ChatsController(MsgInChatService contextMsgInChat, ChatsService contextChats)
        {
            _contextMsgInChat = contextMsgInChat;
            _contextChats = contextChats;
        }


        // GET: api/chats/:chatId
        [HttpGet("{chatId}")]
        public IActionResult Get(int chatId)
        {
            string currentUserName = HttpContext.Session.GetString("username");
            if (currentUserName == null)
            {
                //todo: maybe redireect signin?
                return NotFound();
            }
            List<MsgUsers> messages = _contextMsgInChat.GetMessagesInChat(chatId);
            List<Message> msgs = _contextMsgInChat.ExtractMessages(messages);
            List<Message> fixedMsgs = _contextMsgInChat.GetCopyWithFixedSent(currentUserName, msgs);

            return Content(JsonSerializer.Serialize(fixedMsgs));
        }

        // GET: api/chats/:user/:chatId
        [HttpGet("{username}/{chatId}")]
        public IActionResult GetOtherUser(int chatId, string username)
        {
            User user = _contextChats.GetOtherUserByChatId(chatId, username);
            if(user != null)
            {
                return Content(user.Id);
            }
            return NotFound();
        }

        // GET: api/chats/user/:user
        [HttpGet("user/{username}")]
        public IActionResult GetUserChats(string username)
        {
            List<Chat> chats = _contextChats.GetChatsByUsername(username);
            return Content(JsonSerializer.Serialize(
                _contextChats.ExtractIdAndOtherUser(chats, username)));
        }

        [HttpGet("msgs/{chatid}")]
        public IActionResult GetMsgsOfChat(int chatId)
        {
            string clock = "";
            Message? lastOne = null;
            List<MsgUsers> msgsInChat = _contextMsgInChat.GetMessagesInChat(chatId);
            if (msgsInChat != null)
            {
                foreach (MsgUsers msgAndUser in msgsInChat)
                {
                    // get the msg that created last from all msgs in current chat:
                    //if (msgAndUser.Message.Created >= clock) 
                    if (String.Compare(msgAndUser.Message.Created, clock) >= 0) // created is biger then clock
                    {
                        lastOne = msgAndUser.Message;
                        clock = msgAndUser.Message.Created;
                    }
                }
            }
            return Content(JsonSerializer.Serialize(lastOne));
            
        }

        [HttpGet("getchat/{user1}/{user2}")]
        public IActionResult GetChatByUsers(string user1, string user2)
        {
            Chat chat = _contextChats.GetChatByUsers(user1, user2);
            if (chat == null)
            {
                return Content(null);
            }
            return Content(JsonSerializer.Serialize(chat));
        }

    }
}

