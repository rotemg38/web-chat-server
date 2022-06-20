using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
	public class UserContext : DbContext
	{
        public DbSet<User> Users { get; set; }

		public UserContext()
			//(DbContextOptions<UserContext> options):base(options)
		{
			//Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			//optionsBuilder.UseSqlServer("Server=localhost,1443; Database=NewIcqDB; User=sa; Password=Strong.Pwd-123; " +
              //  "Trusted_Connection=True;MultipleActiveResultSets=true");
			optionsBuilder.UseSqlite("FileName=newIcqDB.db", options =>
            {
				options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

			base.OnConfiguring(optionsBuilder);
		}

		public List<User> getAll()
        {
			return Users.ToList<User>();
			
        }
		

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<User>(entity =>
			{
				entity.HasKey(k => k.Id);

			});
			base.OnModelCreating(modelBuilder);
        }

		/*
		public UserContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
			//
			optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=NewIcqDB;Trusted_Connection=True;MultipleActiveResultSets=true");

			return new UserContext(optionsBuilder.Options);
		}*/


	}

}

