using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DirectorMovie
    {
        public string director_Id { get; set; }
        public string movie_Id { get; set; }
        public string description { get; set; }

        //public virtual DirectorEntity DirectorEntity { get; set; }
    }
}
