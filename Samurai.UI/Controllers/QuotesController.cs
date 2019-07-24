using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Samurai.Data;
using Samurai.Domain;
using Samurai.Domain.Interfaces;

namespace Samurai.UI.Controllers
{
    public class QuotesController : Controller
    {
        
		private readonly ISamuraiRepository _samuraiRepository;

		public QuotesController(ISamuraiRepository samuraiRepository)
		{
			_samuraiRepository = samuraiRepository;
		}

		// GET: Quotes
		public async Task<IActionResult> Index() => View(await _samuraiRepository.GetQuotes());



		// GET: Quotes/Create
		public async Task<IActionResult> Create()
        {
            ViewData["SamuraiId"] = new SelectList(await _samuraiRepository.GetSamurais(), "Id", "Name.FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,SamuraiId")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                await _samuraiRepository.AddQuote(quote);
                return RedirectToAction(nameof(Index));
            }
            ViewData["SamuraiId"] = new SelectList(await _samuraiRepository.GetSamurais(), "Id", "Id", quote.SamuraiId);
            return View(quote);
        }

        
    }
}
