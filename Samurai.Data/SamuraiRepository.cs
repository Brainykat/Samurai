﻿using Microsoft.EntityFrameworkCore;
using Samurai.Domain;
using Samurai.Domain.Interfaces;
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
		public async Task<int> AddSamurai(Domain.Samurai samurai)
		{
			_ = await _context.Samurais.AddAsync(samurai);
			return await _context.SaveChangesAsync();
		}
		public async Task<Domain.Samurai> GetSamurai(int id) => await _context.Samurais.FindAsync(id);
		public async Task<Domain.Samurai> GetSamuraiDetails(int id) =>
			await _context.Samurais.Include(q => q.Quotes).FirstOrDefaultAsync(k => k.Id == id);

		public async Task<List<Domain.Samurai>> GetSamurais() => await _context.Samurais.ToListAsync();

		public async Task<int> AddQuote(Quote quote)
		{
			_context.Quotes.Add(quote);
			return await _context.SaveChangesAsync();
		}
		public async Task<List<Quote>> GetQuotes() => await _context.Quotes.Include(j => j.Samurai).ToListAsync();

		public async Task<List<Quote>> GetQuotes(int samuraiId) => await _context.Quotes.Where(h => h.SamuraiId == samuraiId).ToListAsync();


	}
}
