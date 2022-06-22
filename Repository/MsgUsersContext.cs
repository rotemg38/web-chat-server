using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;
/*
namespace Repository
{
	public class MsgUsersContext :DbContext
	{
		public DbSet<MsgUsers> MsgUsers { get; set; }
        private string connectionString =
        //"Data Source=newIcqDB.db";
        "FileName=/Users/rotemgh/Documents/programingCourses/yearB/AdvancedProgramming2/AdvancedProgrammingWebServerSide/AdvancedProgrammingWebServer/newIcqDB.db";

        public MsgUsersContext()
			//(string connectionString)
			//(DbContextOptions<MsgUsersContext> options): base(options)
		{
			Database.EnsureCreated();
			//this.connectionString = connectionString;
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
			modelBuilder.Entity<MsgUsers>().ToTable("MsgUsers");
			modelBuilder.Entity<MsgUsers>(entity =>
			{
				entity.HasNoKey();
				//entity.HasKey(entity => entity.idMsgUsers);
				

			});

            base.OnModelCreating(modelBuilder);
		}

		public List<MsgUsers> getAll()
		{
			return MsgUsers.ToList<MsgUsers>();
		}
      
    }
}

*/