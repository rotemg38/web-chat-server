using System;
namespace Models
{
	public class MsgUsers
	{
		public MsgUsers(Message msg, User from, User to)
        {
			Message = msg;
			From = from;
			To = to;
        }
		public Message Message { get; set; }
		public User From { get; set; }
		public User To { get; set; }
	}
}

