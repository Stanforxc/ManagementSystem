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
    public class GenreServices : IGenreServices
    {
        private readonly UOW _uow;
        public GenreServices(UOW uow)
        {
            _uow = uow;
        }
        public string createGenre(GenreEntity genreEntity)
        {
            using (var scope = new TransactionScope())
            {
                if (genreEntity != null)
                {
                    if (!_uow.GenreRepository.Exists(genreEntity.genreStyle))
                    {
                        var genre = GenreMapperInitializer(genreEntity);
                        _uow.GenreRepository.Insert(genre);
                        if(genreEntity.GenreMovies != null)
                        {
                            var genreMovies = GenreMovieMapperInitializer(genreEntity);
                            _uow.GenreMovieRepository.InsertBatch(genreMovies);
                        }
                        _uow.Commit();
                        scope.Complete();
                        return genre.genreStyle;
                    }
                    else
                    {
                        return "genres exist";
                    }
                }
                return "-1";
            }
        }

        public bool DeleteGenre(string genre_style)
        {
            var success = false;
            if (genre_style != "")
            {
                using (var scope = new TransactionScope())
                {
                    var genre = _uow.GenreRepository.GetByID(genre_style);
                    if (genre != null)
                    {
                        _uow.GenreRepository.Delete(genre);
                        _uow.Commit();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<GenreEntity> GetAllGenres()
        {
            var genres = (ICollection<genre>)_uow.GenreRepository.GetAll().ToList();
            if (genres.Any())
            {
                Mapper.Initialize(x => x.CreateMap<genre, GenreEntity>()
                    .ForMember(dest => dest.GenreDirectors, opt => opt.Ignore())
                    .ForMember(dest => dest.GenreMovies, opt => opt.Ignore()));
                var genresModel = Mapper.Map<ICollection<genre>, ICollection<GenreEntity>>(genres);
                

                return genresModel;
            }
            return null;
        }

        public GenreEntity GetGenreByName(string genre_style)
        {
            var genre = _uow.GenreRepository.GetByID(genre_style);
            var genreDirectors = (ICollection<genreDirector>)_uow.GenreDirectorRepository.GetMany(gd => gd.genre_id == genre_style).ToList();
            var genreMovies = (ICollection<genreMovie>)_uow.GenreMovieRepository.GetMany(gm => gm.genre_Style == genre_style).ToList();
            if (genre != null)
            {
                Mapper.Initialize(x => x.CreateMap<genre, GenreEntity>()
                    .ForMember(dest => dest.GenreMovies, opt => opt.Ignore())
                    .ForMember(dest => dest.GenreDirectors, opt=>opt.Ignore()));
                var genreModel = Mapper.Map<genre, GenreEntity>(genre);


                Mapper.Initialize(x => x.CreateMap<genreDirector, GenreDirector>()
                .ForMember(dest => dest.Genre,opt => opt.Ignore()));
                genreModel.GenreDirectors = Mapper.Map<ICollection<genreDirector>, ICollection<GenreDirector>>(genreDirectors);

                Mapper.Initialize(x => x.CreateMap<genreMovie, GenreMovie>()
                .ForMember(dest => dest.Genre, opt => opt.Ignore()));
                genreModel.GenreMovies = Mapper.Map<ICollection<genreMovie>, ICollection<GenreMovie>>(genreMovies);
                return genreModel;
            }
            return null;
        }

        public bool UpdateGenre(string genre_style, GenreEntity movieEntity)
        {
            throw new NotImplementedException();
        }

        private genre GenreMapperInitializer(GenreEntity genreEntity)
        {
            Mapper.Initialize(x => x.CreateMap<GenreEntity, genre>()
                       .ForMember(dest => dest.genreMovies, opt => opt.Ignore()));
            return Mapper.Map<GenreEntity, genre>(genreEntity);
        }

        private ICollection<genreMovie> GenreMovieMapperInitializer(GenreEntity genreEntity)
        {
            Mapper.Initialize(t =>
            {
                t.CreateMap<GenreEntity, ICollection<genreMovie>>()
                    .ConstructProjectionUsing(
                        p => p.GenreMovies.Select(
                            gm => new genreMovie
                            {
                                genre_Style = p.genreStyle,
                                movie_id = gm.movie_id,
                                description = p.GenreMovies.FirstOrDefault( g => g.movie_id == gm.movie_id).description
                            }
                            ).ToList()
                        
                            );
            });

            return Mapper.Map<GenreEntity, ICollection<genreMovie>>(genreEntity);
            
           
        }
    }
}
