using System.Collections.Generic;

namespace Services
{
    public class TokenList
    {
        private static Dictionary<string, string> _users = new Dictionary<string, string>();

        public void addToken(string username, string token)
        {
            if (!_users.ContainsKey(username))
            {
                _users.Add(username, token);
            }
        }

        public string getTokenByUser(string user)
        {
            if (_users.ContainsKey(user))
            {
                return _users[user];
            } else
            {
                return null;
            }
            
        }
    }
}
