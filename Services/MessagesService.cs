
using System.Collections.Generic;
using Models;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class MessagesService
    {
        private static List<Message> _msgs = new List<Message>
        {
            new Message() { Id = 1, Type = "text", Text = "content", Date = "10.10.10", MediaSrc = "uri" }
        };
        private static int _msgId = 1;
        

        public MessagesService() {
            
            
        }

        public int GenerateMsgId()
        {
            _msgId++;
            return _msgId;
        }

        public int AddMsg(Message msg)
        {
            int id = GenerateMsgId();
            msg.Id = id;
            _msgs.Add(msg);
            return id;
        }

        public Message GetMsgById(int id)
        {
            return _msgs.Find((msg) => { return msg.Id == id; });
        }
        
        public void DeleteMsg(int id)
        {
            _msgs.Remove(GetMsgById(id));
        }

        public void UpdateMsg(Message msg)
        {
            _msgs.Remove(GetMsgById(msg.Id));
            _msgs.Add(msg);
        }


    }
}
