using System;
using Models;
namespace Services
{
	public interface IMessagesService
	{
        public int GenerateMsgId();

        public Message AddMsg(string content, bool sent);

        public int AddMsg(Message msg);

        public Message GetMsgById(int id);

        public bool DeleteMsg(int id);

        public bool UpdateMsg(int idMsg, string content);
    }
}

