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
                    Mapper.Initialize(x => x.CreateMap<GenreEntity, genre>()
                        .ForMember(dest=>dest.genreMovies,opt=>opt.Ignore()));
                    var genre = Mapper.Map<GenreEntity, genre>(genreEntity);
                    _uow.GenreRepository.Insert(genre);
                    _uow.Commit();
                    scope.Complete();
                    return genre.genreStyle;
                }
                return "-1";
            }
        }

        public bool DeleteGenre(string genre_style)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenreEntity> GetAllGenres()
        {
            throw new NotImplementedException();
        }

        public GenreEntity GetGenreByName(string genre_style)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGenre(string genre_style, GenreEntity movieEntity)
        {
            throw new NotImplementedException();
        }
    }
}
