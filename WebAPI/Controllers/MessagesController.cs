using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Models;
using Services;

namespace WebAPI.Controllers
{
    //class to match the requirment of the api
    public class TmpMsg
    {
        public string Content { get; set; }
    }

    [Route("api/contacts/{userName}/messages")]
    public class MessagesController : Controller
    {
        private readonly IUsersService _contextUsers;
        private readonly IChatsService _contextChats;
        private readonly IMessagesService _contextMsg;
        private readonly IMsgInChatService _contextMsgInChat;
        private readonly TokenList _tokenList;

        public MessagesController(IMessagesService contextMsg, IMsgInChatService contextMsgInChat, IChatsService contextChats, IUsersService usersService, TokenList tokenList)
        {
            _contextMsg = contextMsg;
            _contextMsgInChat = contextMsgInChat;
            _contextChats = contextChats;
            _contextUsers = usersService;
            _tokenList = tokenList;
        }

        // GET: api/Contacts/:userName/Messages
        [HttpGet]
        //get all mesages of user with current user
        public IActionResult Get(string userName)
        {
            string currentUserName = HttpContext.Session.GetString("username");
            if (currentUserName == null)
            {
                return NotFound();
            }

            Chat chat = _contextChats.GetChatByUsers(userName, currentUserName);
            List<MsgUsers> messages = _contextMsgInChat.GetMessagesInChat(chat);
            List<Message> msgs = _contextMsgInChat.ExtractMessages(messages);
            List<Message> fixedMsgs = _contextMsgInChat.GetCopyWithFixedSent(userName, msgs);

            return Content(JsonSerializer.Serialize(fixedMsgs));
        }

        // GET api/Contacts/:userName/Messages/:Id
        [HttpGet("{idMsg}")]
        //get the message with the given Id of the given usrname
        public IActionResult Get(string userName, int idMsg)
        {
            Message msg = _contextMsg.GetMsgById(idMsg);
            if (msg == null)
            {
                return NotFound();
            }
            Message fixedmsg = _contextMsgInChat.GetCopyWithFixedSent(userName, msg);

            return Content(JsonSerializer.Serialize(fixedmsg));
        }

        // PUT api/Contacts/:userName/Messages/:Id
        [HttpPut("{idMsg}")]
        //update the message where the Id = idMsg
        public IActionResult Put(string userName, int idMsg, [FromBody] TmpMsg content)
        {
            if (_contextMsg.UpdateMsg(idMsg, content.Content))
            {
                return Created("Put", new { content = content.Content });
            }
            return NotFound();
        }

        // DELETE api/Contacts/:userName/Messages/:Id
        [HttpDelete("{idMsg}")]
        //delete the message of the user with the given Id
        public IActionResult Delete(string userName, int idMsg)
        {
            List<Chat> chats = _contextChats.GetUserChats(userName);
            if (chats.Count != 0)
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
        public async Task<IActionResult>  Post(string userName, [FromBody] TmpMsg content)
        {
            string currentUserName = HttpContext.Session.GetString("username");
            if (currentUserName == null)
            {
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

            //update the last message in each user
            userTo.last = content.Content;
            userFrom.last = content.Content;

            userTo.lastdate = DateTime.Now.ToString();
            userFrom.lastdate = DateTime.Now.ToString();

            _contextUsers.updateUserLastMsg(userTo);
            _contextUsers.updateUserLastMsg(userFrom);

            // check if sent to android or react:
            string token = _tokenList.getTokenByUser(userTo.Id);
            if (token != null) // send  android
            {
                MobileMessagingClient moblie = new MobileMessagingClient();
                await moblie.SendNotification(userTo.Id, chat.ChatId.ToString(), token, "Got new message", content.Content);
            }
            //else to react- will get there alone


            return Created("Post", new { Content = msg.Content });
        }
    }
}

