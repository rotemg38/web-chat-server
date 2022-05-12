using System;
namespace Models
{
    public class Chat
    {
        public int ChatId { get; set; }

        public Tuple<User,User> Participants { get; set; }
    }
}
