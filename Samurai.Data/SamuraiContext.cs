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
			modelBuilder.Entity<Domain.Samurai>().
				OwnsOne(L => L.Name);
			modelBuilder.Entity<Domain.Samurai>()
				.OwnsOne(m => m.Salary);

			//Shadow properties
			//modelBuilder.Entity<Domain.Samurai>().Property<DateTime>("Created");
			//foreach (var entity in modelBuilder.Model.GetEntityTypes())
			//{
			//	modelBuilder.Entity(entity.Name).Property<DateTime>("Created");
			//	modelBuilder.Entity(entity.Name).Property<DateTime>("LastModified");
			//	modelBuilder.Entity(entity.Name).Property<string>("CreatedBy");
			//}
		}
		public override int SaveChanges()
		{
			//MyOverride();
			return base.SaveChanges();
		}
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			//MyOverride();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
		public void MyOverride()
		{
			//Your Requirements will determine this
			ChangeTracker.DetectChanges();
			var time = DateTime.UtcNow;
			foreach (var entry in ChangeTracker.Entries()
				.Where(c => (c.State == EntityState.Added || c.State == EntityState.Modified)
				&& !c.Metadata.IsOwned()))
			{
				entry.Property("LastModified").CurrentValue = time;
				if (entry.State == EntityState.Added)
				{
					entry.Property("Created").CurrentValue = time;
					entry.Property("CreatedBy").CurrentValue = null;
				}
			}
		}
	}
	
}
