using System.Collections.Generic;
using Domain.Entities;


namespace Services.Interfaces
{
    public interface IMovieServices
    {
        MovieEntity GetMovieByName(string movie_name);
        IEnumerable<MovieEntity> GetAllMovies();
        string createMovie(MovieEntity movieEntity);
        bool UpdateMovie(string movie_name, MovieEntity movieEntity);
        bool DeleteMovie(string movie_name);
        IEnumerable<MovieEntity> GetBest (int star);

        void testMD5();
        void testLOGGER();

        void testFilter();
    }
}
