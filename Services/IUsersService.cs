using System;
using System.Collections.Generic;
using Models;

namespace Services
{
	public interface IUsersService
	{
        public List<User> GetAll();

        public void Add(User user);

        public void AddImg(string userName, string imgSrc);

        public string GetImgByUserName(string userName);

        public bool UserIsExists(string userName);

        public string GetDisNameByUsername(string userName);

        public string GetUserPassword(string userName);

        public void RemoveUser(string userName);

        public User GetUserByUsername(string userName);

        public Message GetLastMsg(string userName);

        
    }
}

