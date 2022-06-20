using System;
namespace Models
{
    public class Chat
    {
        public int ChatId { get; set; }
        public User user1;
        public User user2;

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
