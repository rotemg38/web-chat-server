using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
	public class DataContext2 : DbContext
	{
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rate> Rates { get; set; }
        //public DbSet<MsgInChat> MsgInChat { get; set; }
        public DbSet<MsgUsers> MsgUsers { get; set; }
        

        private string connectionString = "FileName=serverDB.db";
        //	"Data Source=newIcqDB.db";
        //"FileName=/Users/rotemgh/Documents/programingCourses/yearB/AdvancedProgramming2/AdvancedProgrammingWebServerSide/AdvancedProgrammingWebServer/newIcqDB.db";

        public DataContext2()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=localhost,1443; Database=NewIcqDB; User=sa; Password=Strong.Pwd-123; " +
              //"Trusted_Connection=True;MultipleActiveResultSets=true");

            //optionsBuilder.UseSqlite("Data Source=serverDB.db");
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

            modelBuilder.Entity<Chat>().ToTable("Chat");
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(k => k.ChatId);

            });

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(k => k.Id);

            });

            modelBuilder.Entity<Rate>().ToTable("Rate");
            modelBuilder.Entity<Rate>(entity =>
            {
                entity.HasKey(k => k.Id);

            });

            /*modelBuilder.Entity<MsgInChat>().ToTable("MsgInChat");
            modelBuilder.Entity<MsgInChat>(entity =>
            {
                entity.HasNoKey();
            });*/

            modelBuilder.Entity<MsgUsers>().ToTable("MsgUsers");
            modelBuilder.Entity<MsgUsers>(entity =>
            {
                entity.HasNoKey();
            });


            //modelBuilder.Entity<MsgInChat>()
              //      .HasMany<MsgUsers>(e => e.Messages);

            base.OnModelCreating(modelBuilder);


        }
    }
}

