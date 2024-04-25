using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetFinal_2147037.Data;
using ProjetFinal_2147037.Models;

namespace ProjetFinal_2147037.Controllers
{
    public class EmissionTelevisionsController : Controller
    {
        private readonly ProjetFinal_2147037Context _context;

        public EmissionTelevisionsController(ProjetFinal_2147037Context context)
        {
            _context = context;
        }

        // GET: EmissionTelevisions
        public async Task<IActionResult> Index()
        {
            var projetFinal_2147037Context = _context.EmissionTelevisions.Include(e => e.Plateforme);
            return View(await projetFinal_2147037Context.ToListAsync());
        }

        public async Task<IActionResult> IndexAvecViewSQL()
        {
            return View(await _context.VwActeurPersonnageEmissions.ToListAsync());
        }

        // GET: EmissionTelevisions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmissionTelevisions == null)
            {
                return NotFound();
            }

            var emissionTelevision = await _context.EmissionTelevisions
                .Include(e => e.Plateforme)
                .FirstOrDefaultAsync(m => m.EmissionTelevisionId == id);
            if (emissionTelevision == null)
            {
                return NotFound();
            }

            return View(emissionTelevision);
        }

        // GET: EmissionTelevisions/Create
        public IActionResult Create()
        {
            ViewData["PlateformeId"] = new SelectList(_context.Plateformes, "PlateformeId", "PlateformeId");
            return View();
        }

        // POST: EmissionTelevisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmissionTelevisionId,Nom,Descriptions,EstCoreen,Cote,NbVisionnement,CoutProduction,PlateformeId")] EmissionTelevision emissionTelevision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emissionTelevision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlateformeId"] = new SelectList(_context.Plateformes, "PlateformeId", "PlateformeId", emissionTelevision.PlateformeId);
            return View(emissionTelevision);
        }

        // GET: EmissionTelevisions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmissionTelevisions == null)
            {
                return NotFound();
            }

            var emissionTelevision = await _context.EmissionTelevisions.FindAsync(id);
            if (emissionTelevision == null)
            {
                return NotFound();
            }
            ViewData["PlateformeId"] = new SelectList(_context.Plateformes, "PlateformeId", "PlateformeId", emissionTelevision.PlateformeId);
            return View(emissionTelevision);
        }

        // POST: EmissionTelevisions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmissionTelevisionId,Nom,Descriptions,EstCoreen,Cote,NbVisionnement,CoutProduction,PlateformeId")] EmissionTelevision emissionTelevision)
        {
            /*if (id != emissionTelevision.EmissionTelevisionId)
            {
                return NotFound();
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emissionTelevision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmissionTelevisionExists(emissionTelevision.EmissionTelevisionId))
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
            ViewData["PlateformeId"] = new SelectList(_context.Plateformes, "PlateformeId", "PlateformeId", emissionTelevision.PlateformeId);
            return View(emissionTelevision);
        }

        // GET: EmissionTelevisions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmissionTelevisions == null)
            {
                return NotFound();
            }

            var emissionTelevision = await _context.EmissionTelevisions
                .Include(e => e.Plateforme)
                .FirstOrDefaultAsync(m => m.EmissionTelevisionId == id);
            if (emissionTelevision == null)
            {
                return NotFound();
            }

            return View(emissionTelevision);
        }

        // POST: EmissionTelevisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmissionTelevisions == null)
            {
                return Problem("Entity set 'ProjetFinal_2147037Context.EmissionTelevisions'  is null.");
            }
            var emissionTelevision = await _context.EmissionTelevisions.FindAsync(id);
            if (emissionTelevision != null)
            {
                _context.EmissionTelevisions.Remove(emissionTelevision);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmissionTelevisionExists(int id)
        {
          return (_context.EmissionTelevisions?.Any(e => e.EmissionTelevisionId == id)).GetValueOrDefault();
        }
    }
}
