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
            List<MsgUsers> messages = _contextMsgInChat.GetMessagesInChat(chatId);
            List<Message> msgs = _contextMsgInChat.ExtractMessages(messages);
            return Content(JsonSerializer.Serialize(msgs));
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

    }
}

