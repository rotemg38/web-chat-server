using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
	public class UserContext : DbContext
	{
        public DbSet<User> Users { get; set; }
        private string connectionString;// =
        //	"Data Source=newIcqDB.db";
        //"FileName=/Users/rotemgh/Documents/programingCourses/yearB/AdvancedProgramming2/AdvancedProgrammingWebServerSide/AdvancedProgrammingWebServer/newIcqDB.db";
        //"FileName=newIcqDB.db";

        public UserContext(string connectionString)
			//(string connectionString)
			//(DbContextOptions<UserContext> options):base(options)
		{
            this.connectionString = connectionString;
            Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			//optionsBuilder.UseSqlServer("Server=localhost,1443; Database=NewIcqDB; User=sa; Password=Strong.Pwd-123; " +
              //  "Trusted_Connection=True;MultipleActiveResultSets=true");
			optionsBuilder.UseSqlite(connectionString, options =>
            {
				options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

			base.OnConfiguring(optionsBuilder);
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(k => k.Id);

            });
            base.OnModelCreating(modelBuilder);
        }

        public List<User> getAllUsers()
        {
			return Users.ToList<User>();
			
        }

		public User? getUser(String username)
        {
			return Users.Where(user => user.Id == username)
                .Select(user => user).SingleOrDefault();

        }

		public void insertUser(User user)
        {
			Users.Add(user);
			SaveChanges();
        }

        public void removeUser(User user)
        {
            Users.Remove(user);
            SaveChanges();
        }

        public void updateImageUser(String username, String img)
		{
			var user = new User { Id = username, Image = img };
			Users.Attach(user).Property(x => x.Image).IsModified = true;
			SaveChanges();
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

