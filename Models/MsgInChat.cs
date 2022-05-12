using System;
namespace Models
{
    public class MsgInChat
    {
        public string Id { get; set; }
        public Message Message { get; set; }
        public User from { get; set; }
        public User to { get; set; }
    }
}
