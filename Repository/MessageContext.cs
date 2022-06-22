using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;


namespace Repository
{
	public class MessageContext : DbContext
	{
		public DbSet<Message> Messages { get; set; }
        private string connectionString;// =
        //	"Data Source=newIcqDB.db";
        //"FileName=/Users/rotemgh/Documents/programingCourses/yearB/AdvancedProgramming2/AdvancedProgrammingWebServerSide/AdvancedProgrammingWebServer/newIcqDB.db";
        //"FileName=newIcqDB.db";

        public MessageContext(string connectionString)
			//(string connectionString)
			//(DbContextOptions<MessageContext> options)
			//: base(options)
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
			modelBuilder.Entity<Message>().ToTable("Message");
			modelBuilder.Entity<Message>(entity =>
			{
				entity.HasKey(k => k.Id);

			});
			base.OnModelCreating(modelBuilder);
		}

        public List<Message> getAllMsgs()
        {
            return Messages.ToList<Message>();
        }

        public Message? getMsg(int id)
        {
            return Messages.Where(msg => msg.Id == id)
                .Select(msg => msg).SingleOrDefault();

        }

        public Message insertMsg(Message msg)
        {
            Messages.Add(msg);
            SaveChanges();

			//get the last message that just added
			return Messages.OrderByDescending(msg => msg.Id).FirstOrDefault();

        }

        public void updateContent(Message msg)
        {
            Messages.Attach(msg).Property(x => x.Content).IsModified = true;
            SaveChanges();
        }

    }
}

