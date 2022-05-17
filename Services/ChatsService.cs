
using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class ChatsService
    {
        private static List<Chat> _chats = new List<Chat>
        {
                new Chat { ChatId = 1, Participants = new Tuple<User, User>(
                new User { UserName = "shir", DisplayName = "Shir", Password = "Shir1998", Image = "default_picture.jpg" },
                new User { UserName = "dwayne johnson", DisplayName = "The Rock", Password = "Strong9", Image = "https://www.biography.com/.image/t_share/MTgwOTI0NDYwNjQ2Mjc4MjMy/gettyimages-1061959920.jpg" }
                )
            },

            new Chat
            {
                ChatId = 2,
                Participants = new Tuple<User, User>(
                new User { UserName = "rotem", DisplayName = "Rotem", Password = "Rotem100", Image = "default_picture.jpg" },
                new User { UserName = "obama", DisplayName = "Barak Obama", Password = "Prsident7", Image = "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTE4MDAzNDEwNzg5ODI4MTEw/barack-obama-12782369-1-402.jpg" }
                )
            }
        };
        private static int _chatsId = 2;
    
        public ChatsService() {

            
        }

        public int GenerateChatId()
        {
            _chatsId++;
            return _chatsId;
        }

        //todo: check if needed?
        public List<Chat> GetChatsByUsername(string username)
        {
            List<Chat> chats = new List<Chat>();
            foreach (Chat chat in _chats)
            {
                if (chat.Participants.Item1.UserName == username)
                {
                    chats.Add(chat);
                }
                else if (chat.Participants.Item2.UserName == username)
                {
                    chats.Add(chat);
                }
            }
            return chats;
        }

        public List<Tuple<int, User>> GetOtherUser (User user)
        {
            List< Tuple < int,User >> chatIdAndUser = new List<Tuple<int, User>>();
            foreach (Chat chat in _chats)
            {
                if (chat.Participants.Item1 == user)
                {
                    Tuple<int, User> currentTup = Tuple.Create(chat.ChatId, chat.Participants.Item2);
                    chatIdAndUser.Add(currentTup);
                }
                else if (chat.Participants.Item2 == user)
                {
                    Tuple<int, User> currentTup = Tuple.Create(chat.ChatId, chat.Participants.Item1);
                    chatIdAndUser.Add(currentTup);
                }
            }
            return chatIdAndUser;
        }

        /* Get the other user of a specific chat */
        public User GetOtherUserByChatId(int idChat, User user)
        {
            Chat chat = _chats.Find((chat) => { return chat.ChatId == idChat; });
            if(chat == null){
                return null;
            }
            Tuple<User,User> users = chat.Participants;
            if (users.Item1.UserName == user.UserName)
                return users.Item2;
            return users.Item1;
        }

        public List<Chat> GetUserChats(string userName)
        {
            return _chats.FindAll((chat) => {
                return chat.Participants.Item1.UserName == userName ||
                chat.Participants.Item2.UserName == userName; });
        }

        public Chat GetChatByUsers(string user1, string user2)
        {
            return _chats.Find((chat) => { return
                (chat.Participants.Item1.UserName == user1 &&
                chat.Participants.Item2.UserName == user2) ||
                (chat.Participants.Item1.UserName == user2 &&
                chat.Participants.Item2.UserName == user1);
            });
        }

        public Chat AddChat(User user1, User user2)
        {
            Chat newChat = new Chat()
            {
                ChatId = GenerateChatId(),
                Participants = new Tuple<User, User>(user1, user2)
            };
            _chats.Add(newChat);
            return newChat;
        }



    }
    
}
