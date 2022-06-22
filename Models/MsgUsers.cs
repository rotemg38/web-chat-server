using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
	public class MsgUsers
	{
        [Key]
        public int Id { get; set; }
        //[ForeignKey("ChatId")]
        //public virtual Chat Chat { get; set; }
    //    [Key]
      //  public int Id { get; set; }
        [ForeignKey("MsgId")]
        public virtual Message Message { get; set; }
        [ForeignKey("FromId")]
        public virtual User From { get; set; }
        [ForeignKey("ToId")]
        public virtual User To { get; set; }

		public MsgUsers()
        {

        }
		public MsgUsers(Message msg, User from, User to)
        {
			Message = msg;
			From = from;
			To = to;
        }
	}
}

