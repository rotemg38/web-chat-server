using System;
using System.Collections.Generic;
using Models;

namespace Services
{
	public interface IMsgInChatService
	{
        public List<MsgUsers> GetMessagesInChat(int chatId);

        public List<MsgUsers> GetMessagesInChat(Chat chat);

        public List<Message> ExtractMessages(List<MsgUsers> msgsUsers);

        public List<MsgUsers> GetAllMessages(List<Chat> chats);

        public void AddMsgInChat(Chat chat, MsgUsers msg);

        public MsgUsers CreatMsgUsers(User from, User to, Message msg);

        public MsgUsers GetMsg(List<MsgUsers> msgsUsers, int idMsg);

        public List<MsgUsers> FindAllMsgs(Chat chat, int idMsg);

        public bool DeleteMsg(List<Chat> chats, int idMsg);
        
        public bool IsSender(string userName, int idMsg);

        public List<Message> GetCopyWithFixedSent(string userName, List<Message> msgs);

        public Message GetCopyWithFixedSent(string userName, Message msg);
    }
}

