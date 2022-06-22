using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }
        
        public virtual User user1 { get; set; }
        
        public virtual User user2 { get; set; }

        //public Tuple<User,User> Participants { get; set; }

        public Chat() {
          //  Participants = new Tuple<User, User>(new User(), new User());
        }
        /*
        public Chat(Tuple<User,User> participants)
        {
            Participants = participants;
        }*/
    }
}
