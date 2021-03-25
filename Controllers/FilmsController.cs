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
    public class FilmsController : Controller
    {
        private readonly DBFilmContext _context;

        public FilmsController(DBFilmContext context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index(int? id, string name)
        {
            ViewBag.IdFilmGenre = id;
            ViewBag.Name = name;
            ViewBag.Context = _context;
            if(id == null)
            {
                var film = _context.Films.Include(b => b.FilmGenres);
                return View(await film.ToListAsync());
            }
            else
            {
                var film = _context.Films.Where(b => b.FilmGenres != null).Include(b => b.FilmGenres);
                return View(await film.ToListAsync());
            }
            //return View(await _context.Films.ToListAsync());
        }

        public async Task<IActionResult> Crew(int? idF)
        {
            if (idF == null)
            {
                return NotFound();
            }

            var crew = await _context.Films
                .Include(r => r.FilmMembers)
                .FirstOrDefaultAsync(m => m.Idfilm == idF);
            if (crew == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "FilmCrews", new { id = idF, name = crew.Name });
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .FirstOrDefaultAsync(m => m.Idfilm == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idfilm,Name,Year,Descript,Rating")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idfilm,Name,Year,Descript,Rating")] Film film)
        {
            if (id != film.Idfilm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Idfilm))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "FilmGenres", new { id = id, name = film.Name, filmGenre = 0 });
                //return RedirectToAction(nameof(Index));
            }
            ViewBag.FilmGenre = film.FilmGenres;
            ViewBag.Name = film.Name;
            ViewBag.Films = id;
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .FirstOrDefaultAsync(m => m.Idfilm == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crew = _context.FilmMembers.Where(f => f.Idfilm == id).ToList();
            if(crew.Count != 0)
            {
                foreach(var person in crew)
                {
                    var a = await _context.FilmMembers.FindAsync(person.Idfilmem);
                    _context.FilmMembers.Remove(a);
                    await _context.SaveChangesAsync();                }
            }
            
            var film = await _context.Films.FindAsync(id);
            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.Idfilm == id);
        }
    }
}
