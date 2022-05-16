using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    internal interface IUserService
    {
        public List<User> GetAll();

        public void Add(User user);

        public void AddImg(string userName, string imgSrc);

        public string GetImgByUserName(string userName);

        public bool UserIsExists(string userName);

        public string GetDisNameByUsername(string userName);

        public string GetUserPassword(string userName);
        public void RemoveUser(string userName);
    }
}
