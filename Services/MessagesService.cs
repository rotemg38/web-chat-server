
using System;
using System.Collections.Generic;
using Models;
using Repository;

namespace Services
{
    public class MessagesService : IMessagesService
    {
        private ServerDbContext _context;

        public MessagesService() {
            _context = new ServerDbContext();
        }

        public Message AddMsg(string content, bool sent)
        {
            return _context.insertMsg(new Message(content, DateTime.Now.ToString(), sent));
        }

        public int AddMsg(Message msg)
        {
            return _context.insertMsg(msg).Id;
        }

       
        /// This function find the message with the given Id
        /// <param Name="id">message Id</param>
        /// <returns>The message if found, otherwise null</returns>
        public Message GetMsgById(int id)
        {
            return _context.getMsg(id);
        }

        /// This function delete a message with the given Id
        /// <param Name="id">message Id</param>
        /// <returns>true for success and false for failur</returns>
        public bool DeleteMsg(int id)
        {
            var msg = GetMsgById(id);
            if (msg != null)
            {
                _context.Remove(msg);
                return true;
            }
            return false;
        }

        /// This function updates the content of the given message
        /// <param Name="content"> the new content</param>
        /// <param Name="idMsg"> the msg we want to update</param>
        /// <returns>true for success and false for failur</returns>
        public bool UpdateMsg(int idMsg, string content)
        {
            Message oldMsg = GetMsgById(idMsg);
            if (oldMsg != null) {
                oldMsg.Content = content;
                _context.updateContent(oldMsg);
                return true;
            }
            return false;
        }

    }
}
