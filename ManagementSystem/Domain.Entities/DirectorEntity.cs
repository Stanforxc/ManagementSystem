using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class DirectorEntity
    {
        public string director_name { get; set; }
        public Nullable<System.DateTime> born_date { get; set; }

        public virtual ICollection<DirectorGenre> directorGenres { get; set; }
        
        public virtual ICollection<DirectorMovie> directorMovies { get; set; }
    }
}
