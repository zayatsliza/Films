using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Films.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly DBFilmContext _context;

        public ChartsController(DBFilmContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var films = _context.Films.ToList();
            List<object> _genres = new List<object>();
            _genres.Add(new[] { "Genre", "FilmCount" });

            foreach (var filmgenre in films)
            {
                var fg = _context.FilmGenres.Where(fg => fg.Idfilm == filmgenre.Idfilm).ToList();
                foreach (var genre in fg)
                {
                    var genres = _context.Genres.Where(g => g.Idgenres == genre.Idgenres).ToList();
                    var film = _context.Films.Where(f => f.Idfilm == genre.Idfilm).ToList();
                    _genres.Add(new object[] { genre.IdgenresNavigation.Name, film.Count });
                }
            }
            return new JsonResult(_genres);
 
        }
        [HttpGet("JsonData1")]
        public JsonResult JsonData1()
        {
            var films = _context.Films.ToList();
            List<object> _years = new List<object>();
            _years.Add(new[] { "Year", "FilmCount" });

            foreach(var film in films)
            {
                _years.Add(new object[] { film.Year.ToString(), films.Count });
            }
            return new JsonResult(_years);
        }

    }
}
