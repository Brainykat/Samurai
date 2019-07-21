using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Samurai.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Samurai.Data
{
	public class SamuraiContext :DbContext
	{
		public SamuraiContext(DbContextOptions<SamuraiContext> options)
			:base(options)
		{}
		public DbSet<Domain.Samurai> Samurais { get; set; }
		public DbSet<Quote>	 Quotes { get; set; }
		public DbSet<Battle> Battles { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SamuraiBattle>()
				.HasKey(k => new { k.SamuraiId, k.BattleId });
			//Shadow properties
			//modelBuilder.Entity<Domain.Samurai>().Property<DateTime>("Created");
			//modelBuilder.Entity<Domain.Samurai>().Property<DateTime>("LastModified");
			//modelBuilder.Entity<Domain.Samurai>().Property<string>("CreatedBy");
			foreach(var entiity in modelBuilder.Model.GetEntityTypes())
			{
				modelBuilder.Entity(entiity.Name).Property<DateTime>("Created");
				modelBuilder.Entity(entiity.Name).Property<DateTime>("LastModified");
				modelBuilder.Entity(entiity.Name).Property<string>("CreatedBy");
			}
		}
		public override int SaveChanges()
		{
			MyOverride();
			return base.SaveChanges();
		}
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			MyOverride();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
		public void MyOverride()
		{
			//Your Requirements will determine this
			ChangeTracker.DetectChanges();
			var time = DateTime.UtcNow;
			foreach (var entry in ChangeTracker.Entries()
				.Where(c => c.State == EntityState.Added || c.State == EntityState.Modified))
			{
				entry.Property("LastModified").CurrentValue = time;
				if (entry.State == EntityState.Added)
				{
					entry.Property("Created").CurrentValue = time;
					entry.Property("Created").CurrentValue = time;
					entry.Property("CreatedBy").CurrentValue = null;
				}
			}
		}
	}
	
}
