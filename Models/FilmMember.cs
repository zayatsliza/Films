using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace Films
{
    public partial class FilmMember
    {
        public int Idfilmem { get; set; }
        [Display(Name = "Посада")]
        [Required(ErrorMessage = "Поле не може бути порожнім.")]
        public int Idpost { get; set; }
        [Display(Name = "Назва фільму")]
        public int Idfilm { get; set; }
        [Display(Name = "Дата народження")]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Стать")]
        [Required(ErrorMessage = "Поле не може бути порожнім.")]
        public int Idgender { get; set; }
        [Display(Name ="Ім'я Прізвище")]
        [Required(ErrorMessage = "Поле не може бути порожнім.")]
        public string Name { get; set; }
        [Display(Name ="Дата смерті")]
        public DateTime? DateOfDeath { get; set; }

        [Display(Name = "Назва фільму")]
        public virtual Film IdfilmNavigation { get; set; }
        [Display(Name = "Стать")]
        public virtual Gender IdgenderNavigation { get; set; }
        [Display(Name = "Посада")]
        public virtual Position IdpostNavigation { get; set; }
    }
}
