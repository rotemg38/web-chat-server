using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace WebAPI.Controllers
{
    //class to match the requirment of the api
    public class TmpTransfer
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
    }

    [Route("api/transfer")]
    public class TransferController : Controller
    {
        private readonly IUsersService _contextUsers;
        private readonly IChatsService _contextChats;
        private readonly IMessagesService _contextMsg;
        private readonly IMsgInChatService _contextMsgInChat;

        public TransferController(IMessagesService contextMsg, IMsgInChatService contextMsgInChat, IChatsService contextChats, IUsersService usersService)
        {
            _contextMsg = contextMsg;
            _contextMsgInChat = contextMsgInChat;
            _contextChats = contextChats;
            _contextUsers = usersService;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] TmpTransfer info)
        {
            User userTo = _contextUsers.GetUserByUsername(info.To);
            User userFrom = _contextUsers.GetUserByUsername(info.From);
            //if users not existing send error not found
            if (userFrom == null || userTo == null)
            {
                return NotFound();
            }
            Message msg = _contextMsg.AddMsg(info.Content, false);
            Chat chat = _contextChats.GetChatByUsers(info.To, info.From);

            //if this is new messag need to create chat
            if (chat == null)
            {
                chat = _contextChats.AddChat(userTo, userFrom);
            }
            MsgUsers msgUsers = _contextMsgInChat.CreatMsgUsers(userFrom, userTo, msg);
            _contextMsgInChat.AddMsgInChat(chat, msgUsers);
            
            return Created("Post", info);
        }
    }
}

