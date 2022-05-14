
using System.Collections.Generic;
using Models;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class Messages
    {
        private static List<Message> _msgs = new List<Message>();
        private int _msgId = 0;

        public Messages() { }

        public int GenerateMsgId()
        {
            _msgId++;
            return _msgId;
        }

        public int AddMsg(string msgText)
        {
            int id = GenerateMsgId();
            Message msg = new Message();
            msg.Text = msgText;
            msg.Id = id;
            return id;
        }
        




    }
}
