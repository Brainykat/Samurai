using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Samurai.Domain.Interfaces
{
	public interface ISamuraiRepository
	{
		Task<int> AddSamurai(Samurai samurai);
		Task<List<Samurai>> GetSamurais();
		Task<Samurai> GetSamurai(int id);
		Task<List<Quote>> GetQuotes();
		Task<List<Quote>> GetQuotes(int samuraiId);
		Task<int> AddQuote(Quote quote);
		Task<Samurai> GetSamuraiDetails(int id);
	}
}
