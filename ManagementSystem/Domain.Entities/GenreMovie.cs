

namespace Domain.Entities
{
    public class GenreMovie
    {
        public string genre_Style { get; set; }
        public string movie_id { get; set; }
        public string description { get; set; }
        public virtual GenreEntity Genre { get; set; }
    }
}
