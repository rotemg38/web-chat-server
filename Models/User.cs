using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        //todo define the DataAnnotations

        [Required]//unique this is key-> username
        public string id { get; set; }

        //[Required]
        public string name { get; set; }
        
        //[Required]
        //[RegularExpression("")]
        public string Password { get; set; }

        public string Image { get; set; }
        
        public string last { get; set; }
        public string lastdate { get; set; }
        public string server = "localhostShirRotem";

    }
}
