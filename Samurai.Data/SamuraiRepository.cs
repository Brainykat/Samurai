using Microsoft.EntityFrameworkCore;
using Samurai.Domain;
using Samurai.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samurai.Data
{
	public class SamuraiRepository : ISamuraiRepository
	{
		private readonly SamuraiContext _context;
		public SamuraiRepository(SamuraiContext context)
		{
			_context = context;
		}
		public async Task<int> AddSamurai(Domain.Samurai samurai, string user = null)
		{
			_ = await _context.Samurais.AddAsync(samurai);
			//_context.Entry(samurai).Property("Created").CurrentValue = DateTime.UtcNow;
			//_context.Entry(samurai).Property("LastModified").CurrentValue = DateTime.UtcNow;
			//_context.Entry(samurai).Property("CreatedBy").CurrentValue = user;
			return await _context.SaveChangesAsync();
		}
		public async Task<Domain.Samurai> GetSamurai(int id) => await _context.Samurais.FindAsync(id);
		public async Task<Domain.Samurai> GetSamuraiDetails(int id)
		{
			var m = _context.Samurais.Include(j => j.Name).ToList();
			var n = _context.Samurais.Include(j => j.Salary).ToList();
			return await _context.Samurais
			  .Include(q => q.Quotes)
			  .Include(r => r.SecretIdentity)
			  //.Include(s => s.SamuraiBattles)
			  .FirstOrDefaultAsync(k => k.Id == id);
		}

		public async Task<List<Domain.Samurai>> GetSamurais()
		{
			var m = _context.Samurais.Include(j => j.Name).ToList();
			var n = _context.Samurais.Include(j => j.Salary).ToList();
			var b = await _context.Samurais.Include(j => j.Quotes).ToListAsync();
			return b;
		}

		public async Task<int> AddQuote(Quote quote)
		{
			_context.Quotes.Add(quote);
			return await _context.SaveChangesAsync();
		}
		public async Task<List<Quote>> GetQuotes() => await _context.Quotes.Include(j => j.Samurai).ToListAsync();

		public async Task<List<Quote>> GetQuotes(int samuraiId) => await _context.Quotes.Where(h => h.SamuraiId == samuraiId).ToListAsync();

		public async Task<int> AddBattle(Battle battle)
		{
			_context.Battles.Add(battle);
			return await _context.SaveChangesAsync();
		}

		public async Task<List<Battle>> GetBattles() => await _context.Battles.ToListAsync();

		public async Task<Battle> GetBattle(int id) =>
			await _context.Battles.Include(h => h.SamuraiBattles).FirstOrDefaultAsync(g => g.Id == id);

		public async Task AddSamuraiBattle(SamuraiBattle samuraiBattle)
		{
			_context.Add(samuraiBattle);
			await _context.SaveChangesAsync();
		}

		public async Task<List<SamuraiBattle>> GetBattles(int samuraiId) => (await _context.Samurais.Include(g => g.SamuraiBattles)
				.ThenInclude(h => h.Battle).FirstOrDefaultAsync(s => s.Id == samuraiId)).SamuraiBattles;

		public void QueryShadowProperty()
		{
			var time = DateTime.UtcNow.AddDays(-7);
			_context.Samurais.Where(l => EF.Property<DateTime>(l, "Created") >= time).ToList();
		}
	}
}
