using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
	public class ChatContext : DbContext
	{
		public DbSet<Chat> Chats { get; set; }

		public ChatContext()
		{
			//Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseSqlite("Data Source=newIcqDB.db");
			optionsBuilder.UseSqlite("FileName=newIcqDB.db", options =>
			{
				options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
			});

			base.OnConfiguring(optionsBuilder);
		}

		public List<Chat> getAll()
		{
			return Chats.ToList<Chat>();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Chat>().ToTable("Chats");
			modelBuilder.Entity<Chat>(entity =>
			{
				entity.HasKey(k => k.ChatId);

			});
			base.OnModelCreating(modelBuilder);
		}
	}
}

