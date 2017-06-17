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
                   movie movieModel= null;
                   ICollection<movieDirector> movieDirector = null;
                   ICollection<movieGenre> movieGenre = null;

                    movieMapper(movieEntity,out movieModel, out movieDirector, out movieGenre);



                    _uow.MovieRepository.Insert(movieModel);
                    _uow.MovieDirectorRepository.InsertBatch(movieDirector);
                    _uow.MovieGenreRepository.InsertBatch(movieGenre);

                    _uow.Commit();
                    scope.Complete();
                    return movieModel.movie_name;
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
                    var movie = _uow.MovieRepository.GetByID(movie_name);
                    if (movie != null)
                    {
                        _uow.MovieRepository.Delete(movie);
                        _uow.MovieDirectorRepository.Delete(md => md.movie_Id == movie_name);
                        _uow.MovieGenreRepository.Delete(mg => mg.movieId == movie_name);
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



        // entity to model
        private void movieMapper(MovieEntity movieEntity,out movie movieModel,out ICollection<movieDirector> movieDirectors, out ICollection<movieGenre> movieGenres)
        {
            Mapper.Initialize(x => x.CreateMap<MovieEntity, movie>()
                       .ForMember(dest => dest.movieDirectors, opt => opt.Ignore())
                       .ForMember(dest => dest.movieGenres, opt => opt.Ignore()));
            movieModel = Mapper.Map<MovieEntity, movie>(movieEntity);

            //加入 movieDirector 表

            Mapper.Initialize(cfg => {
                cfg.CreateMap<MovieEntity, ICollection<movieDirector>>()
                    .ConstructProjectionUsing(
                        p => p.MovieDirectors.Select(
                            md => new movieDirector
                            {
                                movie_Id = p.movie_name,
                                director = md.director,
                                description = p.MovieDirectors.FirstOrDefault(m => m.movie_Id == p.movie_name).description
                            }
                            ).ToList()
                    );
            });

            movieDirectors = Mapper.Map<MovieEntity, ICollection<movieDirector>>(movieEntity);


            //加入 movieGenre 表

            Mapper.Initialize(t =>
            {
                t.CreateMap<MovieEntity, ICollection<movieGenre>>()
                    .ConstructProjectionUsing(
                        p => p.MovieGenres.Select(
                            mg => new movieGenre
                            {
                                genreStyle = mg.genreStyle,
                                description = p.MovieGenres.FirstOrDefault(g => g.genreStyle == mg.genreStyle).description,
                                movieId = p.movie_name
                            }
                            ).ToList()
                        );
            });
            movieGenres = Mapper.Map<MovieEntity, ICollection<movieGenre>>(movieEntity);
        }
    }
}
