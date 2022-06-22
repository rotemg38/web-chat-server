using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

/*
namespace Repository
{
	public class MsgInChatContext : DbContext
	{
        //public DbSet<MsgInChat> MsgInChat { get; set; }
        public DbSet<MsgUsers> MsgUsers { get; set; }
        private string connectionString;// =
        //	"Data Source=newIcqDB.db";
        //"FileName=/Users/rotemgh/Documents/programingCourses/yearB/AdvancedProgramming2/AdvancedProgrammingWebServerSide/AdvancedProgrammingWebServer/newIcqDB.db";
        //"FileName=newIcqDB.db";

        public MsgInChatContext(string connectionString)
			//(string connectionString)
			//(DbContextOptions<MsgInChatContext> options): base(options)
		{
            this.connectionString = connectionString;
            Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseSqlServer("Server=localhost,1443; Database=NewIcqDB; User=sa; Password=Strong.Pwd-123; " +
			//  "Trusted_Connection=True;MultipleActiveResultSets=true");
			//optionsBuilder.UseSqlite("Data Source=newIcqDB.db");
			optionsBuilder.UseSqlite(connectionString, options =>
			{
				options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
			});

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MsgUsers>().ToTable("MsgUsers");
			modelBuilder.Entity<MsgUsers>(entity =>
			{
                entity.HasNoKey();
            });


			//modelBuilder.Entity<MsgInChat>()
				//	.HasMany<MsgUsers>(e => e.Messages);


            base.OnModelCreating(modelBuilder);
        }

        public List<MsgInChat> getAllMsgInChat()
        {
            List<MsgInChat> msgInChat = new List<MsgInChat>();

			List<MsgUsers> msgUsers = MsgUsers.ToList();

			foreach(MsgUsers msgUser in msgUsers){
				MsgInChat? msgChat = msgInChat.Find(m => m.Chat.ChatId == msgUser.Chat.ChatId);

                if (msgChat != null)
                {
					msgChat.Messages.Add(msgUser);
                }
                else
                {
                    msgInChat.Add(new MsgInChat(msgUser.Chat, msgUser));
                }
            }

            return msgInChat;

        }
		
        public void insertMsgInChat(MsgInChat msg, DataContext dataContext)
        {
			foreach(MsgUsers msgUser in msg.Messages)
            {
				msgUser.Chat = msg.Chat;
				MsgUsers.Add(msgUser);

                dataContext.chatContext.Chats.Attach(msgUser.Chat);
                dataContext.messageContext.Messages.Attach(msgUser.Message);
                dataContext.userContext.Users.Attach(msgUser.To);
                dataContext.userContext.Users.Attach(msgUser.From);

                SaveChanges();
            }
            
            //MsgInChat.Add(msg);
            //SaveChanges();

        }

    }
}

*/