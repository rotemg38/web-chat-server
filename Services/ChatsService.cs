
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
       
    }
    
}
