
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
        /*private static List<User> _users = new List<User>
        {
            new User
            ("rihanna", "Rihanna", "SingWithMe8", "https://pbs.twimg.com/profile_images/1133109643734130688/BwioAwkz.jpg", "localhost:5067")
            ,new User
            ("elon", "Elon Musk", "ImRich10", "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTc5OTk2ODUyMTMxNzM0ODcy/gettyimages-1229892983-square.jpg", "localhost:5067")
            ,new User
            ("ryan", "Ryan Reynolds", "FunnyMe5", "https://pbs.twimg.com/profile_images/1299844050208555008/7wMQaJRA_400x400.jpg", "localhost:5067")
            ,new User
            ("shir", "Shir", "Shir1998", "default_picture.jpg", "localhost:5067")
            ,new User
            ("rotem", "Rotem", "Rotem100", "default_picture.jpg","localhost:5067")
            ,new User
            ("dwayne johnson", "The Rock", "Strong9", "https://www.biography.com/.image/t_share/MTgwOTI0NDYwNjQ2Mjc4MjMy/gettyimages-1061959920.jpg", "localhost:5067")
            ,new User
            ("michael", "Michael Jackson", "TheKIng3", "https://geo-media.beatport.com/image_size/590x404/080c6217-0efa-4323-8b7e-2ad3546a1def.jpg", "localhost:5067")
            ,new User
            ("obama", "Barak Obama", "Prsident7", "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTE4MDAzNDEwNzg5ODI4MTEw/barack-obama-12782369-1-402.jpg", "localhost:5067")

        };*/

        //private UserContext _context;
        //private DataContext _dataContext;
        private ServerDbContext _context;

        public UsersService() {
            //_dataContext = new DataContext();
            //_context = _dataContext.userContext;
            _context = new ServerDbContext();
        }

        public List<User> GetAll()
        {
            //return _users;
            return _context.getAllUsers();
        }
        public void Add(User user)
        {
            //_users.Add(user);
            _context.insertUser(user);
        }

        public void AddImg(string userName, string imgSrc)
        {
            //User curr = GetUserByUsername(userName);
            //curr.Image = imgSrc;
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
            //_users.Remove(curr);
            _context.removeUser(curr);
        }

        public User GetUserByUsername(string userName)
        {
            //return _users.Find((curr) => { return curr.Id == userName; });
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
