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
    public class SamuraisController : Controller
    {
        private readonly ISamuraiRepository _samuraiRepository;

        public SamuraisController(ISamuraiRepository samuraiRepository)
        {
            _samuraiRepository = samuraiRepository;
        }

		// GET: Samurais
		public async Task<IActionResult> Index() => View(await _samuraiRepository.GetSamurais());

		// GET: Samurais/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samurai = await _samuraiRepository.GetSamuraiDetails(id.Value);
            if (samurai == null)
            {
                return NotFound();
            }

            return View(samurai);
        }

		// GET: Samurais/Create
		public IActionResult Create() => View();

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Domain.Samurai samurai)
        {
            if (ModelState.IsValid)
            {
				await _samuraiRepository.AddSamurai(samurai);
                return RedirectToAction(nameof(Index));
            }
            return View(samurai);
        }

		public IActionResult AddQuote() => View();
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> AddQuote()
		//{
		//	return RedirectToAction(nameof(Index));
		//}
		// GET: Samurais/Edit/5
		//public async Task<IActionResult> Edit(int? id)
		//{
		//    if (id == null)
		//    {
		//        return NotFound();
		//    }

		//    var samurai = await _context.Samurais.FindAsync(id);
		//    if (samurai == null)
		//    {
		//        return NotFound();
		//    }
		//    return View(samurai);
		//}

		//// POST: Samurais/Edit/5
		//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		//// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Samurai samurai)
		//{
		//    if (id != samurai.Id)
		//    {
		//        return NotFound();
		//    }

		//    if (ModelState.IsValid)
		//    {
		//        try
		//        {
		//            _context.Update(samurai);
		//            await _context.SaveChangesAsync();
		//        }
		//        catch (DbUpdateConcurrencyException)
		//        {
		//            if (!SamuraiExists(samurai.Id))
		//            {
		//                return NotFound();
		//            }
		//            else
		//            {
		//                throw;
		//            }
		//        }
		//        return RedirectToAction(nameof(Index));
		//    }
		//    return View(samurai);
		//}

		//// GET: Samurais/Delete/5
		//public async Task<IActionResult> Delete(int? id)
		//{
		//    if (id == null)
		//    {
		//        return NotFound();
		//    }

		//    var samurai = await _context.Samurais
		//        .FirstOrDefaultAsync(m => m.Id == id);
		//    if (samurai == null)
		//    {
		//        return NotFound();
		//    }

		//    return View(samurai);
		//}

		//// POST: Samurais/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(int id)
		//{
		//    var samurai = await _context.Samurais.FindAsync(id);
		//    _context.Samurais.Remove(samurai);
		//    await _context.SaveChangesAsync();
		//    return RedirectToAction(nameof(Index));
		//}

		//private bool SamuraiExists(int id)
		//{
		//    return _context.Samurais.Any(e => e.Id == id);
		//}
	}
}
