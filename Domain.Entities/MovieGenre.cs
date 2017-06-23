
namespace Domain.Entities
{
    public class MovieGenre
    {
        public string genreStyle { get; set; }
        public string movieId { get; set; }
        public string description { get; set; }

        public virtual MovieEntity Movie { get; set; }
    }
}
