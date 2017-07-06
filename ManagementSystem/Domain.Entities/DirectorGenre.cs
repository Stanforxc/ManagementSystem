

namespace Domain.Entities
{
    public class DirectorGenre
    {
        public string directorId { get; set; }
        public string genreStyle { get; set; }
        public string description { get; set; }

        public virtual DirectorEntity DirectorEntity { get; set; }
    }
}
