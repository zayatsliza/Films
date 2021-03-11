using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace Films
{
    public partial class FilmGenre
    {
        public int IdfilmGenres { get; set; }
        [Display(Name = "Жанри")]
        public int Idgenres { get; set; }
        [Display(Name = "Назва фільму")]
        public int Idfilm { get; set; }
        [Display(Name = "Назва фільму")]
        public virtual Film IdfilmNavigation { get; set; }
        [Display(Name = "Жанри")]
        public virtual Genre IdgenresNavigation { get; set; }
    }
}
