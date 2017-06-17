

namespace Domain.Entities
{
    public class GenreMovie
    {
        public string genreStyle { get; set; }
        public string movieId { get; set; }
        public string description { get; set; }

        public virtual GenreMovie genreMovie { get; set; }
        public virtual MovieEntity movieEntity { get; set; }
    }
}
