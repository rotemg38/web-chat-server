
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
                new User("shir", "Shir", "Shir1998", "default_picture.jpg", "localhost:5000"),
                new User("dwayne johnson", "The Rock","Strong9","https://www.biography.com/.image/t_share/MTgwOTI0NDYwNjQ2Mjc4MjMy/gettyimages-1061959920.jpg", "localhost:5000")
                )
            },

            new Chat
            {
                ChatId = 2,
                Participants = new Tuple<User, User>(
                new User("rotem", "Rotem", "Rotem100", "default_picture.jpg", "localhost:5000"),
                new User("obama", "Barak Obama", "Prsident7", "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTE4MDAzNDEwNzg5ODI4MTEw/barack-obama-12782369-1-402.jpg", "localhost:5000")
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


        //todo: check if needed?
        public List<Chat> GetChatsByUsername(string username)
        {
            List<Chat> chats = new List<Chat>();
            foreach (Chat chat in _chats)
            {
                if (chat.Participants.Item1.Id == username)
                {
                    chats.Add(chat);
                }
                else if (chat.Participants.Item2.Id == username)
                {
                    chats.Add(chat);
                }
            }
            return chats;
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
            if (users.Item1.Id == user.Id)
                return users.Item2;
            return users.Item1;
        }

        public List<Chat> GetUserChats(string userName)
        {
            return _chats.FindAll((chat) => {
                return chat.Participants.Item1.Id == userName ||
                chat.Participants.Item2.Id == userName; });
        }

        public Chat GetChatByUsers(string user1, string user2)
        {
            return _chats.Find((chat) => { return
                (chat.Participants.Item1.Id == user1 &&
                chat.Participants.Item2.Id == user2) ||
                (chat.Participants.Item1.Id == user2 &&
                chat.Participants.Item2.Id == user1);
            });
        }

        // return the chat who was created, or the chat that was already in the list.
        public Chat AddChat(User user1, User user2) 
        {
            Chat findChat = GetChatByUsers(user1.Id, user2.Id);
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

        public void RemoveChat(string user1, string user2)
        {
            Chat chat = GetChatByUsers(user1, user2);
            _chats.Remove(chat);
        } 

    }
}
