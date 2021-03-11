using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Films
{
    public partial class Genre
    {
        public Genre()
        {
            FilmGenres = new HashSet<FilmGenre>();
        }

        public int Idgenres { get; set; }
        [Display (Name ="Назва жанру")]
        [Required(ErrorMessage = "Поле не може бути порожнім.")]
        public string Name { get; set; }
        [Display(Name = "Опис")]
        public string Descript { get; set; }

        public virtual ICollection<FilmGenre> FilmGenres { get; set; }
    }
}
