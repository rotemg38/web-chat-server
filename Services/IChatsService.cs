using System;
using System.Collections.Generic;
using Models;

namespace Services
{
	public interface IChatsService
	{
        //public int GenerateChatId();

        public List<Chat> GetChatsByUsername(string username);

        public List<Tuple<int, User>> GetOtherUsers(User user);

        public User GetOtherUserByChatId(int idChat, string username);

        public User GetOtherUserByChatId(int idChat, User user);

        public List<Tuple<int, string>> ExtractIdAndOtherUser(List<Chat> chats, string username);

        public List<Chat> GetUserChats(string userName);

        public Chat GetChatByUsers(string user1, string user2);

        
        public Chat AddChat(User user1, User user2);

        public void RemoveChat(string user1, string user2);

    }
}

