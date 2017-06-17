using System;
using System.Collections.Generic;
using Domain.Entities;
namespace Services.Interfaces
{
    public interface IGenreServices
    {
        GenreEntity GetGenreByName(string genre_style);
        IEnumerable<GenreEntity> GetAllGenres();
        string createGenre(GenreEntity movieEntity);
        bool UpdateGenre(string genre_style, GenreEntity movieEntity);
        bool DeleteGenre(string genre_style);
    }
}
