using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data.UOW;
using Services.Interfaces;
using Infrastructure.Data.Data;
using System;
using MD5Cli;
using COMXiaLib;

namespace Services
{
    public class MovieServices : IMovieServices
    {
        private readonly UOW _uow;
        public MovieServices(UOW uow)
        {
            _uow = uow;
        }

        public void testMD5()
        {
            new MD5();
        }

        public void testLOGGER()
        {
            Logger logger = new Logger();
            logger.serialize("put","url");
        }

        public IEnumerable<MovieEntity> GetBest(int star)
        {
            var movies = _uow.MovieRepository.GetMany(m => m.star == star).ToList();
            Mapper.Initialize(x => x.CreateMap<movie, MovieEntity>()
            .ForMember(dest=>dest.MovieDirectors,opt=>opt.Ignore())
            .ForMember(dest=>dest.MovieGenres,opt=>opt.Ignore()));
            List<MovieEntity> container = new List<MovieEntity>();

            foreach(var movie in movies)
            {
                container.Add(GetMovieByName(movie.movie_name));
            }
            return container;
        }

        public string createMovie(MovieEntity movieEntity)
        {
            using (var scope = new TransactionScope())
            {
                if (movieEntity != null)
                {
                    if(!_uow.MovieRepository.Exists(movieEntity.movie_name))
                    {
                        movie movieModel = MovieMapperInitializer(movieEntity);
                        ICollection<movieDirector> movieDirector = MovieDirectorMapperInitializer(movieEntity);
                        ICollection<movieGenre> movieGenre = MovieGenreMapperInitializer(movieEntity);
;
                        _uow.MovieRepository.Insert(movieModel);

                        /*
                        foreach(var dir in movieDirector)
                        {
                            //如果导演不存在,则创建新的导演
                            if (!_uow.DirectoryRepository.Exists(dir.director))
                            {
                                _uow.DirectoryRepository.Insert(new director
                                {
                                    director_name = dir.director
                                });

                                _uow.DirectorMovieRepository.Insert(new directorMovie
                                {
                                    director_Id = dir.director
                                });
                            }
                        }*/

                        _uow.MovieDirectorRepository.InsertBatch(movieDirector);
                        _uow.MovieGenreRepository.InsertBatch(movieGenre);

                        _uow.Commit();
                        scope.Complete();
                        return movieModel.movie_name;
                    }
                    else
                    {
                        return "movie exist";
                    }
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
                Mapper.Initialize(x => x.CreateMap<movie, MovieEntity>()
                    .ForMember(dest => dest.MovieGenres,opt => opt.Ignore())
                    .ForMember(dest => dest.MovieDirectors, opt => opt.Ignore()));
                var moviesModel = Mapper.Map<ICollection<movie>, ICollection<MovieEntity>>(movies);
                return moviesModel;
            }
            return null;
        }

        public MovieEntity GetMovieByName(string movie_name)
        {
            var movie = _uow.MovieRepository.GetByID(movie_name);
            var movieDirectors = (ICollection<movieDirector>)_uow.MovieDirectorRepository.GetMany(md => md.movie_Id == movie_name).ToList();
            var movieGenres = (ICollection<movieGenre>)_uow.MovieGenreRepository.GetMany(mg => mg.movieId == movie_name);
            if (movie != null)
            {
                Mapper.Initialize(x => x.CreateMap<movie, MovieEntity>()
                    .ForMember(dest => dest.MovieDirectors, opt => opt.Ignore())
                    .ForMember(dest => dest.MovieGenres, opt => opt.Ignore())
                    );
                var movieModel = Mapper.Map<movie, MovieEntity>(movie);

                Mapper.Initialize(x => x.CreateMap<movieDirector,MovieDirector>()
                    .ForMember(dest => dest.MovieEntity, opt => opt.Ignore())
                    );
                movieModel.MovieDirectors = Mapper.Map<ICollection<movieDirector>, ICollection<MovieDirector>>(movieDirectors);

                Mapper.Initialize(x => x.CreateMap<movieGenre, MovieGenre>()
                    .ForMember(dest => dest.Movie, opt => opt.Ignore())
                    );
                movieModel.MovieGenres = Mapper.Map<ICollection<movieGenre>, ICollection<MovieGenre>>(movieGenres);


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




        // Director 实体
        private director DirectorMapperIntializer(DirectorEntity directorEntity)
        {
            Mapper.Initialize(x => x.CreateMap<DirectorEntity, movie>()
                       .ForMember(dest => dest.movieDirectors, opt => opt.Ignore())
                       .ForMember(dest => dest.movieGenres, opt => opt.Ignore()));
             return Mapper.Map<DirectorEntity, director>(directorEntity);
        }

        // 加入 directorMovie 表
        private ICollection<directorMovie> DirectorMovieMapperInitializer(DirectorEntity directorEntity)
        { 
            Mapper.Initialize(t =>
            {
                t.CreateMap<DirectorEntity, ICollection<directorMovie>>()
                    .ConstructProjectionUsing(
                        p => p.directorMovies.Select(
                            dm => new directorMovie
                            {
                                movie_Id = dm.movie_Id,
                                director_Id = p.director_name,
                                description = p.directorMovies.FirstOrDefault(g => g.description == dm.description).description
                            }
                            ).ToList()
                            );
            });

            return Mapper.Map<DirectorEntity, ICollection<directorMovie>>(directorEntity);
        }

        // 加入 directorGenre 表
        private ICollection<directorGenre> DirectorGenreMapperIntializer(DirectorEntity directorEntity)
        {
            Mapper.Initialize(t =>
            {
                t.CreateMap<DirectorEntity, ICollection<directorGenre>>()
                    .ConstructProjectionUsing(
                        p => p.directorGenres.Select(
                            dg => new directorGenre
                            {
                                genreStyle = dg.genreStyle,
                                directorId = p.director_name,
                                description = p.directorGenres.FirstOrDefault(g => g.description == dg.description).description

                            }
                            ).ToList()
                            );
            });

            return Mapper.Map<DirectorEntity, ICollection<directorGenre>>(directorEntity);
        }

        // Movie 实体
        private movie MovieMapperInitializer(MovieEntity movieEntity)
        {
            Mapper.Initialize(x => x.CreateMap<MovieEntity, movie>()
                       .ForMember(dest => dest.movieDirectors, opt => opt.Ignore())
                       .ForMember(dest => dest.movieGenres, opt => opt.Ignore()));
            return Mapper.Map<MovieEntity, movie>(movieEntity);
        }

        //加入 movieDirector 表
        private ICollection<movieDirector> MovieDirectorMapperInitializer(MovieEntity movieEntity)
        {
            Mapper.Initialize(t =>
            {
                t.CreateMap<MovieEntity, ICollection<movieDirector>>()
                    .ConstructProjectionUsing(
                        p => p.MovieDirectors.Select(
                            md => new movieDirector
                            {
                                director = md.director,
                                movie_Id = p.movie_name,
                                description = p.MovieDirectors.FirstOrDefault(g => g.movie_Id == md.movie_Id).description
                            }
                            ).ToList()
                            );
               
            });
            return Mapper.Map<MovieEntity, ICollection<movieDirector>>(movieEntity);
        }

        ////加入 movieGenre 表
        private ICollection<movieGenre> MovieGenreMapperInitializer(MovieEntity movieEntity)
        {
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
            return Mapper.Map<MovieEntity, ICollection<movieGenre>>(movieEntity);
        }
    }
}
