
using System.Collections.Generic;
using Models;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class UsersService
    {
        private static List<User> _users = new List<User>();

        public UsersService() { }

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
            User curr = _users.Find((curr) => { return curr.UserName == userName; });
            curr.Image = imgSrc;
        }

        public string GetImgByUserName(string userName)
        {
            User curr = _users.Find((curr) => { return curr.UserName == userName; });
            return curr.Image;
        }

        public bool UserIsExists(string userName)
        {
            User curr = _users.Find((curr) => { return curr.UserName == userName; });
            if (curr != null)
            {
                return true;
            }
            return false;
        }

        public string GetDisNameByUsername(string userName)
        {
            User curr = _users.Find((curr) => { return curr.UserName == userName; });
            return curr.DisplayName;
        }
        
        public string GetUserPassword(string userName)
        {
            User curr = _users.Find((curr) => { return curr.UserName == userName; });
            return curr.Password;
        }
        public void RemoveUser(string userName)
        {
            User curr = _users.Find((curr) => { return curr.UserName == userName; });
            _users.Remove(curr);
        }

    }
}
