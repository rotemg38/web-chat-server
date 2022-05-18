
using System;
using System.Collections.Generic;
using Models;
using Services;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class UsersService
    {
        private readonly ChatsService _chatsService;
        private readonly MsgInChatService _msgInChatService;
        private static List<User> _users = new List<User>
        {
            new User
            { id = "rihanna", name = "Rihanna", Password = "SingWithMe8", Image = "https://pbs.twimg.com/profile_images/1133109643734130688/BwioAwkz.jpg" }
            ,new User
            { id = "elon", name= "Elon Musk", Password= "ImRich10", Image= "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTc5OTk2ODUyMTMxNzM0ODcy/gettyimages-1229892983-square.jpg" }
            ,new User
            { id = "ryan", name = "Ryan Reynolds", Password = "FunnyMe5", Image = "https://pbs.twimg.com/profile_images/1299844050208555008/7wMQaJRA_400x400.jpg" }
            ,new User
            { id = "shir", name = "Shir", Password = "Shir1998", Image = "default_picture.jpg" }
            ,new User
            { id = "rotem", name = "Rotem", Password = "Rotem100", Image = "default_picture.jpg" }
            ,new User
            { id = "dwayne johnson", name = "The Rock", Password = "Strong9", Image = "https://www.biography.com/.image/t_share/MTgwOTI0NDYwNjQ2Mjc4MjMy/gettyimages-1061959920.jpg" }
            ,new User
            { id = "michael", name = "Michael Jackson", Password = "TheKIng3", Image = "https://geo-media.beatport.com/image_size/590x404/080c6217-0efa-4323-8b7e-2ad3546a1def.jpg" }
            ,new User
            { id = "obama", name = "Barak Obama", Password = "Prsident7", Image = "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTE4MDAzNDEwNzg5ODI4MTEw/barack-obama-12782369-1-402.jpg" }

        };

        public UsersService() { }

        public List<User> GetAll()
        {
            return _users;
        }
        public void Add(User user)
        {
            _users.Add(user);
        }

        public void AddImg(string userName, string imgSrc)
        {
            User curr = GetUserByUsername(userName);
            curr.Image = imgSrc;
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
            return curr.name;
        }
        
        public string GetUserPassword(string userName)
        {
            User curr = GetUserByUsername(userName);
            return curr.Password;
        }
        public void RemoveUser(string userName)
        {
            User curr = GetUserByUsername(userName);
            _users.Remove(curr);
        }

        public User GetUserByUsername(string userName)
        {
            return _users.Find((curr) => { return curr.id == userName; });
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
                //if (msg.Message.Date > date)
                {
                    date = msg.Message.Created;
                    finalMsg = msg.Message;
                }
            }
            return finalMsg;
        }
        

    }
}
