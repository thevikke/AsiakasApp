using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AsiakasApp.Data;
using AsiakasApp.Models;

namespace AsiakasApp.Controllers
{
    public class testController : Controller
    {
        private readonly AsiakasContext _context;

        public testController(AsiakasContext context)
        {
            _context = context;
        }

        // GET: test
        public async Task<IActionResult> Index()
        {
            var asiakasContext = _context.Asiakkaat.Include(a => a.Asiakastyyppi);
            return View(await asiakasContext.ToListAsync());
        }

        // GET: test/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asiakas = await _context.Asiakkaat
                .Include(a => a.Asiakastyyppi)
                .SingleOrDefaultAsync(m => m.Avain == id);
            if (asiakas == null)
            {
                return NotFound();
            }

            return View(asiakas);
        }

        // GET: test/Create
        public IActionResult Create()
        {
            ViewData["AsiakastyyppiID"] = new SelectList(_context.AsiakasTyypit, "AsiakastyyppiID", "AsiakastyyppiID");
            return View();
        }

        // POST: test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Avain,Nimi,Osoite,Postinro,Postitmp,Luontipvm,AsiakastyyppiID")] Asiakas asiakas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asiakas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsiakastyyppiID"] = new SelectList(_context.AsiakasTyypit, "AsiakastyyppiID", "AsiakastyyppiID", asiakas.AsiakastyyppiID);
            return View(asiakas);
        }

        // GET: test/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asiakas = await _context.Asiakkaat.SingleOrDefaultAsync(m => m.Avain == id);
            if (asiakas == null)
            {
                return NotFound();
            }
            ViewData["AsiakastyyppiID"] = new SelectList(_context.AsiakasTyypit, "AsiakastyyppiID", "AsiakastyyppiID", asiakas.AsiakastyyppiID);
            return View(asiakas);
        }

        // POST: test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Avain,Nimi,Osoite,Postinro,Postitmp,Luontipvm,AsiakastyyppiID")] Asiakas asiakas)
        {
            if (id != asiakas.Avain)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asiakas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsiakasExists(asiakas.Avain))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsiakastyyppiID"] = new SelectList(_context.AsiakasTyypit, "AsiakastyyppiID", "AsiakastyyppiID", asiakas.AsiakastyyppiID);
            return View(asiakas);
        }

        // GET: test/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asiakas = await _context.Asiakkaat
                .Include(a => a.Asiakastyyppi)
                .SingleOrDefaultAsync(m => m.Avain == id);
            if (asiakas == null)
            {
                return NotFound();
            }

            return View(asiakas);
        }

        // POST: test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asiakas = await _context.Asiakkaat.SingleOrDefaultAsync(m => m.Avain == id);
            _context.Asiakkaat.Remove(asiakas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsiakasExists(int id)
        {
            return _context.Asiakkaat.Any(e => e.Avain == id);
        }
    }
}
