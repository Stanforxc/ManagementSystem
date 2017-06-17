
namespace Domain.Entities
{
    public class MovieGenre
    {
        public string movieId { get; set; }
        public string genreStyle { get; set; }
        public string description { get; set; }

        public virtual MovieEntity MovieEntity { get; set; }
    }
}
