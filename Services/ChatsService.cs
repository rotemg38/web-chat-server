
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
                new User { id = "shir", name = "Shir", Password = "Shir1998", Image = "default_picture.jpg" },
                new User { id = "dwayne johnson", name = "The Rock", Password = "Strong9", Image = "https://www.biography.com/.image/t_share/MTgwOTI0NDYwNjQ2Mjc4MjMy/gettyimages-1061959920.jpg" }
                )
            },

            new Chat
            {
                ChatId = 2,
                Participants = new Tuple<User, User>(
                new User { id = "rotem", name = "Rotem", Password = "Rotem100", Image = "default_picture.jpg" },
                new User { id = "obama", name = "Barak Obama", Password = "Prsident7", Image = "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTE4MDAzNDEwNzg5ODI4MTEw/barack-obama-12782369-1-402.jpg" }
                )
            }
        };
        private static int _chatsId = 2;

        public ChatsService() { }

        public int GenerateChatId()
        {
            _chatsId++;
            return _chatsId;
        }

        public List<Tuple<int, User>> GetOtherUsers (User user)
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
            if (users.Item1.id == user.id)
                return users.Item2;
            return users.Item1;
        }

        public List<Chat> GetUserChats(string userName)
        {
            return _chats.FindAll((chat) => {
                return chat.Participants.Item1.id == userName ||
                chat.Participants.Item2.id == userName; });
        }

        public Chat GetChatByUsers(string user1, string user2)
        {
            return _chats.Find((chat) => { return
                (chat.Participants.Item1.id == user1 &&
                chat.Participants.Item2.id == user2) ||
                (chat.Participants.Item1.id == user2 &&
                chat.Participants.Item2.id == user1);
            });
        }

        // return the chat who was created, or the chat that was already in the list.
        public Chat AddChat(User user1, User user2) 
        {
            Chat findChat = GetChatByUsers(user1.id, user2.id);
            if (findChat != null)
            {
                return findChat;
            }
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
