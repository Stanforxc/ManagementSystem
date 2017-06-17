
using System.Collections.Generic;

namespace Domain.Entities
{
    public class GenreEntity
    {
        public string genreStyle { get; set; }
        public string description { get; set; }
        public virtual ICollection<GenreMovie> genreMovies { get; set; }
    }
}
