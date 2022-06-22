using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
	public class ChatContext : DbContext
	{
		public DbSet<Chat> Chats { get; set; }
        private string connectionString;// =
        //	"Data Source=newIcqDB.db";
        //"FileName=/Users/rotemgh/Documents/programingCourses/yearB/AdvancedProgramming2/AdvancedProgrammingWebServerSide/AdvancedProgrammingWebServer/newIcqDB.db";
        //"FileName=newIcqDB.db";

        public ChatContext(string connectionString)
			//(string connectionString)
		{
            this.connectionString = connectionString;
            Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseSqlite("Data Source=newIcqDB.db");
			optionsBuilder.UseSqlite(connectionString, options =>
			{
				options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
			});

			base.OnConfiguring(optionsBuilder);
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Chat>().ToTable("Chat");
			modelBuilder.Entity<Chat>(entity =>
			{
				entity.HasKey(k => k.ChatId);

			});
			base.OnModelCreating(modelBuilder);
		}

        public List<Chat> getAllChats()
        {
            return Chats.ToList<Chat>();
        }
		
        public void insertChat(Chat chat, DataContext dataContext)
        {
            Chats.Add(chat);
			//dataContext.userContext.Users.Attach(chat.user1);
            //dataContext.userContext.Users.Attach(chat.user2);
            SaveChanges();

        }

        public void removeChat(Chat chat)
        {
			Chats.Remove(chat);
            SaveChanges();
        }

    }
}

