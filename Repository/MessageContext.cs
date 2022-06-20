using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;


namespace Repository
{
	public class MessageContext : DbContext
	{
		public DbSet<Message> Messages { get; set; }

		public MessageContext()
			//(DbContextOptions<MessageContext> options)
			//: base(options)
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

		public List<Message> getAll()
		{
			return Messages.ToList<Message>();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Message>().ToTable("Messages");
			modelBuilder.Entity<Message>(entity =>
			{
				entity.HasKey(k => k.Id);

			});
			base.OnModelCreating(modelBuilder);
		}
	}
}

