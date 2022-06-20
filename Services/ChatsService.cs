
using System;
using System.Collections.Generic;
using Repository;
using Models;

namespace Services
{
    
    public class ChatsService :IChatsService
    {
        private static List<Chat> _chats = new List<Chat>();

        private ChatContext _context;
        
        private static int _chatsId = 0;

        public ChatsService() {
           // _context = new ChatContext();
        }

        public int GenerateChatId()
        {
            _chatsId++;
            return _chatsId;
        }

        public List<Chat> GetChatsByUsername(string username)
        {
            List<Chat> chats = new List<Chat>();
            foreach (Chat chat in _chats)
            {
                if (chat.user1.Id == username)
                {
                    chats.Add(chat);
                }
                else if (chat.user2.Id == username)
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
                if (chat.user1.Id == user.Id)
                {
                    Tuple<int, User> currentTup = Tuple.Create(chat.ChatId, chat.user2);
                    chatIdAndUser.Add(currentTup);
                }
                else if (chat.user2.Id == user.Id)
                {
                    Tuple<int, User> currentTup = Tuple.Create(chat.ChatId, chat.user1);
                    chatIdAndUser.Add(currentTup);
                }
            }
            return chatIdAndUser;
        }

        public User GetOtherUserByChatId(int idChat, string username)
        {
            Chat chat = _chats.Find((chat) => { return chat.ChatId == idChat; });
            if (chat == null)
            {
                return null;
            }
            //Tuple<User, User> users = chat.Participants;
            if (chat.user1.Id == username)
                return chat.user2;
            return chat.user1;
        }


        /* Get the other user of a specific chat */
        public User GetOtherUserByChatId(int idChat, User user)
        {
            if(user == null)
            {
                return null;
            }
            else
            {
                return GetOtherUserByChatId(idChat, user.Id);
            }
        }

        public List<Tuple<int, string>> ExtractIdAndOtherUser(List<Chat> chats, string username)
        {
            List<Tuple<int, string>> lstdict = new List<Tuple<int, string>>();
            foreach (Chat chat in chats)
            {
                string user2 = GetOtherUserByChatId(chat.ChatId, username).Id;
                lstdict.Add(new Tuple<int, string>(chat.ChatId, user2));
            }
            return lstdict;
        }

        public List<Chat> GetUserChats(string userName)
        {
            return _chats.FindAll((chat) => {
                return chat.user1.Id == userName ||
                chat.user2.Id == userName; });
        }

        public Chat GetChatByUsers(string user1, string user2)
        {
            return _chats.Find((chat) => { return
                (chat.user1.Id == user1 &&
                chat.user2.Id == user2) ||
                (chat.user1.Id == user2 &&
                chat.user2.Id == user1);
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
                //Participants = new Tuple<User, User>(user1, user2)
                user1 = user1,
                user2 = user2
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
