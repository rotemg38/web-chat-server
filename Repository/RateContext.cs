using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
	public class RateContext : DbContext
	{
		public DbSet<Rate> Rates { get; set; }
        private string connectionString =
        //	"Data Source=newIcqDB.db";
        "FileName=/Users/rotemgh/Documents/programingCourses/yearB/AdvancedProgramming2/AdvancedProgrammingWebServerSide/AdvancedProgrammingWebServer/newIcqDB.db";
        //"FileName=newIcqDB.db";
        public RateContext()
			//(string connectionString)
			//(DbContextOptions<RateContext> options): base(options)
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
			modelBuilder.Entity<Rate>().ToTable("Rate");
			modelBuilder.Entity<Rate>(entity =>
			{
				entity.HasKey(k => k.Id);

			});
			base.OnModelCreating(modelBuilder);
		}

		public List<Rate> getAllRate()
		{
			return Rates.ToList<Rate>();
		}

        public Rate? getRate(int? id)
        {
            return Rates.Where(rate => rate.Id == id)
                .Select(rate => rate).SingleOrDefault();
        }

        public void insertRate(Rate rate)
        {
            Rates.Add(rate);
            SaveChanges();
        }

        public void removeRate(Rate rate)
        {
            Rates.Remove(rate);
            SaveChanges();
        }

        public void updateRate(Rate rate)
        {
            Rates.Attach(rate).Property(x => x.Feedback).IsModified = true;
            Rates.Attach(rate).Property(x => x.Name).IsModified = true;
            SaveChanges();
        }

    }
}

