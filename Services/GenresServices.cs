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
    public class GenresServices : IGenresServices
    {
        private readonly UOW _uow;

        public GenresServices(UOW uow)
        {
            _uow = uow;
        }
        public string createGenre(GenresEntity genreEntity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGenre(string genre_name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenresEntity> GetAllGenres()
        {
            var genres = _uow.GenereRepository.GetAll().ToList();
            if (genres.Any())
            {
                Mapper.Initialize(x => x.CreateMap<Genere,GenresEntity>());
                var genreModel = Mapper.Map<List<Genere>, List<GenresEntity>>(genres);
                return genreModel;
            }
            return null;
        }

        public GenresEntity GetGenreByName(string genre_name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGenre(string genre_name, GenresEntity genreEntity)
        {
            throw new NotImplementedException();
        }
    }
}
