
using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class ChatsService
    {
        private static List<Chat> _chats = new List<Chat>();
        private int _chatsId = 0;
    
        public ChatsService() { }

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
        //return -1 if convertion doesnt exists, or chat id if so.
        public int GetConversationBy2Users(User user1, User user2)
        {
            foreach (Chat chat in _chats)
            {
                if ((chat.Participants.Item1 == user1 && chat.Participants.Item2 == user2) || 
                    (chat.Participants.Item1 == user2 && chat.Participants.Item2 == user1)) {
                    return chat.ChatId;
                }
            }
            return -1;
        }

        //return -1 if convertion doesnt exists, or chat id if so.
        public int AddConction(User user1, User user2) 
        {
            if (GetConversationBy2Users(user1, user2) == -1)
            {
                Chat chat = new Chat() { ChatId = _chatsId + 1, Participants = Tuple.Create(user1, user2) };
                _chats.Add(chat);
                return chat.ChatId;
            }
            else
            {
                return -1;
            }
        }
        

    }
    
}
