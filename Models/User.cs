using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        //todo define the DataAnnotations

        //[Required]//unique this is key
        public string UserName { get; set; }

        //[Required]
        public string DisplayName { get; set; }
        
        //[Required]
        //[RegularExpression("")]
        public string Password { get; set; }

        public string Image { get; set; }

    }
}
