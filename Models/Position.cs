using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Films
{
    public partial class Position
    {
        public Position()
        {
            FilmMembers = new HashSet<FilmMember>();
        }

        
        public int Idpost { get; set; }
        [Display(Name = "Посада")]
        [Required(ErrorMessage = "Поле не може бути порожнім.")]
        public string Name { get; set; }

        public virtual ICollection<FilmMember> FilmMembers { get; set; }
    }
}
