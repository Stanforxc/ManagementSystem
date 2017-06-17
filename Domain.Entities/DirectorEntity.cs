using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class DirectorEntity
    {
        public string director_name { get; set; }
        public Nullable<DateTime> born_date { get; set; }
        public virtual ICollection<DirectorGenre> Genres { get; set; }
    }
}
