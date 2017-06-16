using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class GenresEntity
    {
        public string genre { get; set; }
        public virtual ICollection<DirectorGenre> Directors { get; set; }
    }
}
