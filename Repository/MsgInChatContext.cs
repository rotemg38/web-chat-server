using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
	public class MsgInChatContext : DbContext
	{
		public DbSet<MsgInChat> MsgInChat { get; set; }

		public MsgInChatContext()
			//(DbContextOptions<MsgInChatContext> options): base(options)
		{
			//Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseSqlServer("Server=localhost,1443; Database=NewIcqDB; User=sa; Password=Strong.Pwd-123; " +
			//  "Trusted_Connection=True;MultipleActiveResultSets=true");
			//optionsBuilder.UseSqlite("Data Source=newIcqDB.db");
			optionsBuilder.UseSqlite("FileName=newIcqDB.db", options =>
			{
				options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
			});

			base.OnConfiguring(optionsBuilder);
		}

		public List<MsgInChat> getAll()
		{
			return MsgInChat.ToList<MsgInChat>();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MsgInChat>().ToTable("MsgInChat");
			base.OnModelCreating(modelBuilder);
		}
	}
}

