
using System.Collections.Generic;
using Models;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class UsersService
    {
        private static List<User> _users = new List<User>
        {
            new User
            { UserName = "rihanna", DisplayName = "Rihanna", Password = "SingWithMe8", Image = "https://pbs.twimg.com/profile_images/1133109643734130688/BwioAwkz.jpg" }
            ,new User
            { UserName = "elon", DisplayName= "Elon Musk", Password= "ImRich10", Image= "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTc5OTk2ODUyMTMxNzM0ODcy/gettyimages-1229892983-square.jpg" }
            ,new User
            { UserName = "ryan", DisplayName = "Ryan Reynolds", Password = "FunnyMe5", Image = "https://pbs.twimg.com/profile_images/1299844050208555008/7wMQaJRA_400x400.jpg" }
            ,new User
            { UserName = "shir", DisplayName = "Shir", Password = "Shir1998", Image = "default_picture.jpg" }
            ,new User
            { UserName = "rotem", DisplayName = "Rotem", Password = "Rotem100", Image = "default_picture.jpg" }
            ,new User
            { UserName = "dwayne johnson", DisplayName = "The Rock", Password = "Strong9", Image = "https://www.biography.com/.image/t_share/MTgwOTI0NDYwNjQ2Mjc4MjMy/gettyimages-1061959920.jpg" }
            ,new User
            { UserName = "michael", DisplayName = "Michael Jackson", Password = "TheKIng3", Image = "https://geo-media.beatport.com/image_size/590x404/080c6217-0efa-4323-8b7e-2ad3546a1def.jpg" }
            ,new User
            { UserName = "obama", DisplayName = "Barak Obama", Password = "Prsident7", Image = "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTE4MDAzNDEwNzg5ODI4MTEw/barack-obama-12782369-1-402.jpg" }

        };

        public UsersService() {

            
        }

        public List<User> GetAll()
        {
            return _users;
        }
        public void Add(User user)
        {
            _users.Add(user);
        }

        public void AddImg(string userName, string imgSrc)
        {
            User curr = GetUserByUsername(userName);
            curr.Image = imgSrc;
        }

        public string GetImgByUserName(string userName)
        {
            User curr = GetUserByUsername(userName);
            return curr.Image;
        }

        public bool UserIsExists(string userName)
        {
            User curr = GetUserByUsername(userName);
            if (curr != null)
            {
                return true;
            }
            return false;
        }

        public string GetDisNameByUsername(string userName)
        {
            User curr = GetUserByUsername(userName);
            return curr.DisplayName;
        }
        
        public string GetUserPassword(string userName)
        {
            User curr = GetUserByUsername(userName);
            return curr.Password;
        }
        public void RemoveUser(string userName)
        {
            User curr = GetUserByUsername(userName);
            _users.Remove(curr);
        }

        public User GetUserByUsername(string userName)
        {
            return _users.Find((curr) => { return curr.UserName == userName; });
        }
        

    }
}
