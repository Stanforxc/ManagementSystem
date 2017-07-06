using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GenreDirector
    {
        public string genre_id { get; set; }
        public string director_id { get; set; }
        public string description { get; set; }
        public virtual GenreEntity Genre { get; set; }
    }
}
