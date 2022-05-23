using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        //todo define the DataAnnotations

        [Required]//unique this is key-> username
        public string Id { get; set; }

        //[Required]
        public string Name { get; set; }
        
        public string Password { get; set; }

        public string Image { get; set; }
        
        public string last { get; set; }
        public string lastdate { get; set; }
        public string Server { get; set; }

        public User(string id, string name, string server)
        {
            Id = id;
            Name = name;
            Server = server;
            Password = null;
            Image = null;
            last = null;
            lastdate = null;

        }

        public User(string id, string name, string password, string image, string server)
        {
            Id=id;
            Name = name;
            Password = password;
            Image = image;  
            Server = server;
            last = null;
            lastdate = null;
        }

        public User()
        {

        }

    }
}
