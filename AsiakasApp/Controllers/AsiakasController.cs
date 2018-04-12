using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AsiakasApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AsiakasApp.Models;

namespace AsiakasApp.Controllers
{
    public class AsiakasController : Controller
    {
        private readonly AsiakasContext _context;

        public AsiakasController(AsiakasContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        private bool OppilasExists(int id)
        {
            return _context.Asiakkaat.Any(e => e.Avain == id);
        }
        public IActionResult Delete(int id)
        {
            var r = _context.Asiakkaat.Where(t => t.Avain == id).SingleOrDefault();

            _context.Asiakkaat.Remove(r);
            _context.SaveChanges();

            // HUOM! ObjectResult sisältää automatic content negotiation:n
            return new JsonResult(new { status = "OK" });
            //return Json(new { status = "OK" });
        }

        public IActionResult DeleteView(int id)
        {
            var r = from s in _context.Asiakkaat
                    select s;

            r = r.Where(t => t.Avain == id);

            return PartialView("StudentList", r);
        }
        public IActionResult EditView(int id)
        {
            var oppilas = _context.Asiakkaat.SingleOrDefault(m => m.Avain == id);
            if (oppilas == null)
            {
                return NotFound();
            }
            ViewData["AsiakastyyppiID"] = new SelectList(_context.AsiakasTyypit, "AsiakastyyppiID", "AsiakastyyppiID", oppilas.AsiakastyyppiID);

            return PartialView("Edit", oppilas);
        }
        public IActionResult CreateView()
        {

            ViewBag.AsiakastyyppiID = new SelectList(_context.AsiakasTyypit, "AsiakastyyppiID", "Selite");
            return PartialView("Create");
        }
        [HttpPost]
        public IActionResult CreateStudent([Bind("Avain,Nimi,Osoite,Postinro,Postitmp,Luontipvm,AsiakastyyppiID")] Asiakas newoppilas)
        {
            newoppilas.AsiakastyyppiID = null;
            if (ModelState.IsValid)
            {
                _context.Add(newoppilas);
                _context.SaveChanges();
                return Json(new
                {
                    msg = "Oppilas lisätty!"
                });
            }
            return Json(new
            {
                msg = "Lisäys epäonnistui!"
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStudent([Bind("Avain,Nimi,Osoite,Postinro,Postitmp,Luontipvm,AsiakastyyppiID")] Asiakas oppilas)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oppilas);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OppilasExists(oppilas.Avain))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new
                {
                    msg = "Oppilas muokattu!"
                });
            }
            ViewData["AsiakastyyppiID"] = new SelectList(_context.AsiakasTyypit, "AsiakastyyppiID", "AsiakastyyppiID", oppilas.AsiakastyyppiID);
            return PartialView("Edit", oppilas);
        }

        public IActionResult GetStudents(string name, string address)
        {

            // Include ei löydy jos using-lauseesta puuttuu using Microsoft.EntityFrameworkCore;
            var r = _context.Asiakkaat.Include(t => t.Asiakastyyppi).Select(t => t);
            

            if ( !string.IsNullOrEmpty(name) )
            {
                r = r.Where(t => t.Nimi.StartsWith(name));
            }
            if (!string.IsNullOrEmpty(address))
            {
                r = r.Where(t => t.Osoite.StartsWith(address));
            }

            return PartialView("StudentList", r);
        }
    }
}