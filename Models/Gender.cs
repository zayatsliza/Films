using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace Films
{
    public partial class Gender
    {
        public Gender()
        {
            FilmMembers = new HashSet<FilmMember>();
        }

        public int Idgender { get; set; }
        [Display(Name = "Стать")]
        [Required(ErrorMessage = "Поле не може бути порожнім.")]
        public string Name { get; set; }

        public virtual ICollection<FilmMember> FilmMembers { get; set; }
    }
}
