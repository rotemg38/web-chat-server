using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
	public class RateContext : DbContext
	{
		public DbSet<Rate> Rates { get; set; }

		public RateContext()
			//(DbContextOptions<RateContext> options): base(options)
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

		public List<Rate> getAll()
		{
			return Rates.ToList<Rate>();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Rate>().ToTable("Rates");
			modelBuilder.Entity<Rate>(entity =>
			{
				entity.HasKey(k => k.Id);

			});
			base.OnModelCreating(modelBuilder);
		}

	}
}

