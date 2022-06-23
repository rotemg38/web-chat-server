using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class MsgInChat
    {
        public Chat Chat { get; set; }

        public List<MsgUsers> Messages { get; set; }

        public MsgInChat()
        {

        }
        public MsgInChat(Chat chat, MsgUsers msgUsers)
        {
            Chat = chat;
            Messages = new List<MsgUsers>{msgUsers};
        }
        public MsgInChat(Chat chat, List<MsgUsers> msgs)
        {
            Chat = chat;
            Messages = msgs;
        }

       
    }
}
