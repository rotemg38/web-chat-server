
using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class MessagesService
    {
        private static List<Message> _msgs = new List<Message>();
        private static int _msgId = 0;

        public MessagesService() { }

        public int GenerateMsgId()
        {
            _msgId++;
            return _msgId;
        }

        public Message AddMsg(string content, bool sent)
        {
            var msg = new Message(GenerateMsgId(), content, DateTime.Now.ToString(), sent);
            _msgs.Add(msg);
            return msg;
        }

        public int AddMsg(Message msg)
        {
            int id = GenerateMsgId();
            msg.Id = id;
            _msgs.Add(msg);
            return id;
        }

        /// <summary>
        /// This function find the message with the given Id
        /// </summary>
        /// <param Name="id">message Id</param>
        /// <returns>The message if found, otherwise null</returns>
        public Message GetMsgById(int id)
        {
            return _msgs.Find((msg) => { return msg.Id == id; });
        }

        /// <summary>
        /// This function delete a message with the given Id
        /// </summary>
        /// <param Name="id">message Id</param>
        /// <returns>true for success and false for failur</returns>
        public bool DeleteMsg(int id)
        {
            var msg = GetMsgById(id);
            if (msg != null)
            {
                _msgs.Remove(msg);
                return true;
            }
            return false;
        }

        /// <summary>
        /// This function updates the content of the given message
        /// </summary>
        /// <param Name="content"> the new content</param>
        /// <param Name="idMsg"> the msg we want to update</param>
        /// <returns>true for success and false for failur</returns>
        public bool UpdateMsg(int idMsg, string content)
        {
            var oldMsg = GetMsgById(idMsg);
            if (oldMsg != null) {
                oldMsg.Content = content;
                return true;
            }
            return false;
        }

       
        

    }
}
