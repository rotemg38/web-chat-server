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

        public Chat() {
        }
    }
}
