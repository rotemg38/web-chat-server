using System;
namespace Models
{
    public class Message
    {
        public Message(int id, string content, string created, bool sent)
        {
            Id = id;
            Content = content;
            Created = created;
            Sent = sent;
        }
        public Message(Message msg)
        {
            Id = msg.Id;
            Content = msg.Content;
            Created = msg.Created;
            Sent = msg.Sent;
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public string Created { get; set; }
        public bool Sent { get; set; }

    }
}
