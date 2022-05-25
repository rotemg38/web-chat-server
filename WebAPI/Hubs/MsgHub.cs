using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs
{
    public class MsgHub : Hub
    {
        private static Dictionary<string, string> _connections = new Dictionary<string, string>();

        /// <summary>
        /// This function get message which is sent to the given userName.
        /// The function send the message to the username if connected
        /// </summary>
        /// <param name="msg">the message</param>
        /// <param name="userName">the user the message ment for</param>
        /// <returns></returns>
        public async Task SentMessage(string msg, string userName)
        {
            string keyRes = null;
            //send to specific client the message
            if(_connections.TryGetValue(userName, out keyRes))
            {
                await Clients.Client(keyRes).SendAsync("ReciveMessage", msg);
            }
            
        }

        /// <summary>
        /// This function add user to the connection list
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task AddUserConnection(string userName)
        {
            string keyRes = null;
            //add the user to the connections only if not exists
            if (!_connections.TryGetValue(userName, out keyRes))
            {
                _connections.Add(userName, Context.ConnectionId);
            }
            else
            {
                //if exists update his connection string
                _connections[userName] = Context.ConnectionId;
            }

        }

    }
}
