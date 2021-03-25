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
    public class FilmMembersController : Controller
    {
        private readonly DBFilmContext _context;

        public FilmMembersController(DBFilmContext context)
        {
            _context = context;
        }

        // GET: FilmMembers
        public async Task<IActionResult> Index(int? id, string name)
        {
            if (id == 0) return RedirectToAction("Films","Index");
            ViewBag.Id = id;
            ViewBag.Name = name;
            var film = _context.FilmMembers.Include(b => b.IdfilmNavigation).Include(b => b.IdpostNavigation);
            //var dBFilmContext = _context.FilmMembers.Include(f => f.IdfilmNavigation).Include(f => f.IdgenderNavigation).Include(f => f.IdpostNavigation);
            //return View(await dBFilmContext.ToListAsync());
            return View(await film.ToListAsync());
        }

        // GET: FilmMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmMember = await _context.FilmMembers
                .Include(f => f.IdfilmNavigation)
                .Include(f => f.IdgenderNavigation)
                .Include(f => f.IdpostNavigation)
                .FirstOrDefaultAsync(m => m.Idfilmem == id);
            if (filmMember == null)
            {
                return NotFound();
            }

            return View(filmMember);
        }

        // GET: FilmMembers/Create
        public IActionResult Create()
        {
            ViewData["Idfilm"] = new SelectList(_context.Films, "Idfilm", "Name");
            ViewData["Idgender"] = new SelectList(_context.Genders, "Idgender", "Name");
            ViewData["Idpost"] = new SelectList(_context.Positions, "Idpost", "Name");
            return View();
        }

        // POST: FilmMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idfilmem,Idpost,Idfilm,DateOfBirth,Idgender,Name,DateOfDeath")] FilmMember filmMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filmMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idfilm"] = new SelectList(_context.Films, "Idfilm", "Name", filmMember.Idfilm);
            ViewData["Idgender"] = new SelectList(_context.Genders, "Idgender", "Name", filmMember.Idgender);
            ViewData["Idpost"] = new SelectList(_context.Positions, "Idpost", "Name", filmMember.Idpost);
            return View(filmMember);
        }

        // GET: FilmMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmMember = await _context.FilmMembers.FindAsync(id);
            if (filmMember == null)
            {
                return NotFound();
            }
            ViewData["Idfilm"] = new SelectList(_context.Films, "Idfilm", "Name", filmMember.Idfilm);
            ViewData["Idgender"] = new SelectList(_context.Genders, "Idgender", "Name", filmMember.Idgender);
            ViewData["Idpost"] = new SelectList(_context.Positions, "Idpost", "Name", filmMember.Idpost);
            return View(filmMember);
        }

        // POST: FilmMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idfilmem,Idpost,Idfilm,DateOfBirth,Idgender,Name,DateOfDeath")] FilmMember filmMember)
        {
            if (id != filmMember.Idfilmem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmMemberExists(filmMember.Idfilmem))
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
            ViewData["Idfilm"] = new SelectList(_context.Films, "Idfilm", "Name", filmMember.Idfilm);
            ViewData["Idgender"] = new SelectList(_context.Genders, "Idgender", "Name", filmMember.Idgender);
            ViewData["Idpost"] = new SelectList(_context.Positions, "Idpost", "Name", filmMember.Idpost);
            return View(filmMember);
        }

        // GET: FilmMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmMember = await _context.FilmMembers
                .Include(f => f.IdfilmNavigation)
                .Include(f => f.IdgenderNavigation)
                .Include(f => f.IdpostNavigation)
                .FirstOrDefaultAsync(m => m.Idfilmem == id);
            if (filmMember == null)
            {
                return NotFound();
            }

            return View(filmMember);
        }

        // POST: FilmMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filmMember = await _context.FilmMembers.FindAsync(id);
            _context.FilmMembers.Remove(filmMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmMemberExists(int id)
        {
            return _context.FilmMembers.Any(e => e.Idfilmem == id);
        }
    }
}
