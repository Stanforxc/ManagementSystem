using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data.UOW;
using Services.Interfaces;
using Infrastructure.Data.Data;
using System;

namespace Services
{
    public class MovieServices : IMovieServices
    {
        private readonly UOW _uow;

        public MovieServices(UOW uow)
        {
            _uow = uow;
        }
        public string createMovie(MovieEntity movieEntity)
        {
            using (var scope = new TransactionScope())
            {
                if (movieEntity != null)
                {
                    var movie = new movie
                    {
                        movie_name = movieEntity.movie_name,
                        online_time = movieEntity.online_time,
                        star = movieEntity.star,
                        director = movieEntity.director,
                        cast = movieEntity.cast,
                        price = movieEntity.price,
                        runtime = movieEntity.runtime,
                        description = movieEntity.description
                    };

                    _uow.MovieRepository.Insert(movie);
                    _uow.Commit();
                    scope.Complete();
                    return movie.movie_name;
                }
                return "-1";
            }
        }

        public bool DeleteMovie(string movie_name)
        {
            var success = false;
            if (movie_name != "")
            {
                using (var scope = new TransactionScope())
                {
                    var user = _uow.MovieRepository.GetByID(movie_name);
                    if (user != null)
                    {
                        _uow.MovieRepository.Delete(user);
                        _uow.Commit();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<MovieEntity> GetAllMovies()
        {
            var movies = _uow.MovieRepository.GetAll().ToList();
            if (movies.Any())
            {
                Mapper.Initialize(x => x.CreateMap<movie, MovieEntity>());
                var moviesModel = Mapper.Map<List<movie>, List<MovieEntity>>(movies);
                return moviesModel;
            }
            return null;
        }

        public MovieEntity GetMovieByName(string movie_name)
        {
            var movie = _uow.MovieRepository.GetByID(movie_name);
            if (movie != null)
            {
                Mapper.Initialize(x => x.CreateMap<movie, MovieEntity>());
                var movieModel = Mapper.Map<movie, MovieEntity>(movie);
                return movieModel;
            }
            return null;
        }

        public bool UpdateMovie(string movie_name, MovieEntity movieEntity)
        {
            var success = false;
            using (var scope = new TransactionScope())
            {
                var movie = _uow.MovieRepository.GetByID(movie_name);
                if (movie != null)
                {
                    movie.movie_name = movieEntity.movie_name;
                    movie.online_time = movieEntity.online_time;
                    movie.star = movieEntity.star;
                    movie.director = movieEntity.director;
                    movie.cast = movieEntity.cast;
                    movie.price = movieEntity.price;
                    movie.runtime = movieEntity.runtime;
                    movie.description = movieEntity.description;
                    _uow.MovieRepository.Uppdate(movie);
                    _uow.Commit();
                    scope.Complete();
                    success = true;
                }
            }
            return success;
        }
    }
}
