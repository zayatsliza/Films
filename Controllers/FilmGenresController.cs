using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Films;

namespace Films.Controllers
{
    public class FilmGenresController : Controller
    {
        private readonly DBFilmContext _context;

        public FilmGenresController(DBFilmContext context)
        {
            _context = context;
        }

        // GET: FilmGenres
        public async Task<IActionResult> Index()
        {
            var dBFilmContext = _context.FilmGenres.Include(f => f.IdfilmNavigation).Include(f => f.IdgenresNavigation);
            return View(await dBFilmContext.ToListAsync());
        }

        // GET: FilmGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmGenre = await _context.FilmGenres
                .Include(f => f.IdfilmNavigation)
                .Include(f => f.IdgenresNavigation)
                .FirstOrDefaultAsync(m => m.IdfilmGenres == id);
            if (filmGenre == null)
            {
                return NotFound();
            }

            // return View(filmGenre);
            return RedirectToAction("Index", "Films", new { id = filmGenre.IdfilmGenres, name = filmGenre.Idgenres});
        }

        // GET: FilmGenres/Create
        public IActionResult Create()
        {
            ViewData["Idfilm"] = new SelectList(_context.Films, "Idfilm", "Descript");
            ViewData["Idgenres"] = new SelectList(_context.Genres, "Idgenres", "Name");
            return View();
        }

        // POST: FilmGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdfilmGenres,Idgenres,Idfilm")] FilmGenre filmGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filmGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idfilm"] = new SelectList(_context.Films, "Idfilm", "Descript", filmGenre.Idfilm);
            ViewData["Idgenres"] = new SelectList(_context.Genres, "Idgenres", "Name", filmGenre.Idgenres);
            return View(filmGenre);
        }

        // GET: FilmGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmGenre = await _context.FilmGenres.FindAsync(id);
            if (filmGenre == null)
            {
                return NotFound();
            }
            ViewData["Idfilm"] = new SelectList(_context.Films, "Idfilm", "Descript", filmGenre.Idfilm);
            ViewData["Idgenres"] = new SelectList(_context.Genres, "Idgenres", "Name", filmGenre.Idgenres);
            return View(filmGenre);
        }

        // POST: FilmGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdfilmGenres,Idgenres,Idfilm")] FilmGenre filmGenre)
        {
            if (id != filmGenre.IdfilmGenres)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmGenreExists(filmGenre.IdfilmGenres))
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
            ViewData["Idfilm"] = new SelectList(_context.Films, "Idfilm", "Descript", filmGenre.Idfilm);
            ViewData["Idgenres"] = new SelectList(_context.Genres, "Idgenres", "Name", filmGenre.Idgenres);
            return View(filmGenre);
        }

        // GET: FilmGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmGenre = await _context.FilmGenres
                .Include(f => f.IdfilmNavigation)
                .Include(f => f.IdgenresNavigation)
                .FirstOrDefaultAsync(m => m.IdfilmGenres == id);
            if (filmGenre == null)
            {
                return NotFound();
            }

            return View(filmGenre);
        }

        // POST: FilmGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filmGenre = await _context.FilmGenres.FindAsync(id);
            _context.FilmGenres.Remove(filmGenre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmGenreExists(int id)
        {
            return _context.FilmGenres.Any(e => e.IdfilmGenres == id);
        }
    }
}
