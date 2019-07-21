using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Samurai.Domain;
using Samurai.Domain.Interfaces;
using Samurai.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Samurai.UI.Controllers
{
	public class SamuraisController : Controller
	{
		private readonly ISamuraiRepository _samuraiRepository;

		public SamuraisController(ISamuraiRepository samuraiRepository)
		{
			_samuraiRepository = samuraiRepository;
		}

		public async Task<IActionResult> Index() => View(await _samuraiRepository.GetSamurais());

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var samurai = await _samuraiRepository.GetSamuraiDetails(id.Value);
			samurai.SamuraiBattles = await _samuraiRepository.GetBattles(id.Value);
			//There was a bug for double include hence above second trip 
			if (samurai == null)
			{
				return NotFound();
			}

			return View(samurai);
		}

		public IActionResult Create() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Domain.Samurai samurai)
		{
			if (ModelState.IsValid)
			{
				samurai.Salary = Money.Create(samurai.Salary.Currency, samurai.Salary.Amount, DateTime.UtcNow);
				await _samuraiRepository.AddSamurai(samurai);
				return RedirectToAction(nameof(Index));
			}
			return View(samurai);
		}

		public IActionResult AddQuote() => View();

		public async Task<IActionResult> AddToBattle()
		{
			ViewData["SamuraiId"] = new SelectList(await _samuraiRepository.GetSamurais(), "Id", "Name");
			ViewData["BattleId"] = new SelectList(await _samuraiRepository.GetBattles(), "Id", "Name");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddToBattle(SamuraiBattle samuraiBattle)
		{
			await _samuraiRepository.AddSamuraiBattle(samuraiBattle);
			return RedirectToAction(nameof(Details), new { id = samuraiBattle.SamuraiId });
		}
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
