using System.Collections.Generic;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IGenresServices
    {
        GenresEntity GetGenreByName(string genre_name);
        IEnumerable<GenresEntity> GetAllGenres();
        string createGenre(GenresEntity genreEntity);
        bool UpdateGenre(string genre_name, GenresEntity genreEntity);
        bool DeleteGenre(string genre_name);
    }
}
