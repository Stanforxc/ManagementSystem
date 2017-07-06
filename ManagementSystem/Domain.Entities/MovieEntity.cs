using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class MovieEntity
    {
        public MovieEntity()
        {
            this.MovieDirectors = new HashSet<MovieDirector>();
            this.MovieGenres = new HashSet<MovieGenre>();
        }

        public string movie_name { get; set; }
        public Nullable<System.DateTime> online_time { get; set; }
        public Nullable<int> star { get; set; }
        public string cast { get; set; }
        public Nullable<int> price { get; set; }
        public string runtime { get; set; }
        public string description { get; set; }

        public virtual ICollection<MovieDirector> MovieDirectors { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

    }
}
