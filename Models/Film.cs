using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace Films
{
    public partial class Film
    {
        public Film()
        {
            FilmGenres = new HashSet<FilmGenre>();
            FilmMembers = new HashSet<FilmMember>();
        }

        public int Idfilm { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім.")]
        [Display(Name = "Назва фільму")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім.")]
        [Display(Name = "Рік виходу")]
        public int Year { get; set; }
        [Display(Name = "Опис")]
        public string Descript { get; set; }
       
        [Display(Name = "Рейтинг")]
        [Required(ErrorMessage = "Поле не може бути порожнім.")]
        public double Rating { get; set; }

        //public int FilmGenreId { get; set; }

        public virtual ICollection<FilmGenre> FilmGenres { get; set; }
        public virtual ICollection<FilmMember> FilmMembers { get; set; }
    }
}
