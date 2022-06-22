using System;
namespace Repository
{
	public class DataContext
	{
        private string connectionString =
                "FileName=/Users/rotemgh/Documents/programingCourses/yearB/AdvancedProgramming2/AdvancedProgrammingWebServerSide/AdvancedProgrammingWebServer/newIcqDB.db";
        public ChatContext chatContext { get; }
        public UserContext userContext { get; }
        public MessageContext messageContext { get; }
        //public MsgInChatContext msgInChatContext { get; }

        public DataContext()
		{
            chatContext = new ChatContext(connectionString);
            userContext = new UserContext(connectionString);
            messageContext = new MessageContext(connectionString);
            //msgInChatContext = new MsgInChatContext(connectionString);
        }
    }
}

