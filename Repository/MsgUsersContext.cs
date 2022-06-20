using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
	public class MsgUsersContext :DbContext
	{
		public DbSet<MsgUsers> MsgUsers { get; set; }

		public MsgUsersContext()
			//(DbContextOptions<MsgUsersContext> options): base(options)
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

		public List<MsgUsers> getAll()
		{
			return MsgUsers.ToList<MsgUsers>();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MsgUsers>().ToTable("MsgUsers");
			base.OnModelCreating(modelBuilder);
		}
	}
}

