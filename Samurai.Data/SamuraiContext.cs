using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Samurai.Domain;

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
		}

	}
}
