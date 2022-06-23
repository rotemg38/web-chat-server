
using System;
using System.Collections.Generic;
using Models;
using Repository;

namespace Services
{
    public class UsersService : IUsersService
    {
        private readonly ChatsService _chatsService;
        private readonly MsgInChatService _msgInChatService;
        private ServerDbContext _context;

        public UsersService() {
            _context = new ServerDbContext();
        }

        public List<User> GetAll()
        {
            return _context.getAllUsers();
        }
        public void Add(User user)
        {
            _context.insertUser(user);
        }

        public void AddImg(string userName, string imgSrc)
        {
            _context.updateImageUser(userName, imgSrc);
        }

        public string GetImgByUserName(string userName) 
        {
            User curr = GetUserByUsername(userName);
            return curr.Image;
        }

        public bool UserIsExists(string userName)
        {
            User curr = GetUserByUsername(userName);
            if (curr != null)
            {
                return true;
            }
            return false;
        }

        public string GetDisNameByUsername(string userName)
        {
            User curr = GetUserByUsername(userName);
            return curr.Name;
        }
        
        public string GetUserPassword(string userName)
        {
            User curr = GetUserByUsername(userName);
            return curr.Password;
        }
        public void RemoveUser(string userName)
        {
            User curr = GetUserByUsername(userName);
            _context.removeUser(curr);
        }

        public User GetUserByUsername(string userName)
        {
            return _context.getUser(userName);
        }

        public Message GetLastMsg(string userName)
        {
            List<MsgUsers> messages = new List<MsgUsers>();
            List<Chat> chats = _chatsService.GetUserChats(userName);
            foreach (Chat chat in chats)
            {
                List<MsgUsers> current = _msgInChatService.GetMessagesInChat(chat);
                if (current != null)
                {
                    foreach (MsgUsers msg in current)
                    {
                        messages.Add(msg);
                    }
                }
            }
            string date = "0000-00-00 00:00";
            Message finalMsg = new Message(0, "", "", true);
            foreach (MsgUsers msg in messages)
            {
                if (String.Compare(msg.Message.Created, date) > 0) // the left string is bigger (i think-nedd to check)
                {
                    date = msg.Message.Created;
                    finalMsg = msg.Message;
                }
            }
            return finalMsg;
        }

        public void updateUserNameAndServer(User user)
        {
            _context.updateUserNameAndServer(user);
        }

        public void updateUserLastMsg(User user)
        {
            _context.updateUserLastMsg(user);
        }
    }
}
