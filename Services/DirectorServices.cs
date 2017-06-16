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
    public class DirectorServices : IDirectorServices
    {
        private readonly UOW _uow;

        private IMapper _mapper;
        
        public DirectorServices(UOW uow)
        {
            _uow = uow;
        }
        public bool createDirector(DirectorEntity directorEntity)
        {
            using (var scope = new TransactionScope())
            {
                if (directorEntity != null)
                {

                    Mapper.Initialize(x => x.CreateMap<GenresEntity, Genere>());
                   // var genres = Mapper.Map<ICollection<GenresEntity>, ICollection<Genere>>(directorEntity.Genre);

                    var director = new director
                    {
                        director_name = directorEntity.director_name,
                        born_date = directorEntity.born_date,
                        //Generes = genres
                    };

                    try
                    {
                        _uow.GenereRepository.ManyToManyDirectorGenre(director);
                        _uow.DirectoryRepository.Insert(director);
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                    finally
                    {
                        _uow.Commit();
                        scope.Complete();
                    }
                }
                return true;
            }

        }

        public bool DeleteDirector(string director_name)
        {
            var success = false;
            if (director_name != "")
            {
                using (var scope = new TransactionScope())
                {
                    var director = _uow.DirectoryRepository.GetByID(director_name);
                    if (director != null)
                    {
                        _uow.DirectoryRepository.Delete(director);
                        _uow.Commit();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<DirectorEntity> GetAllDirectors()
        {
            var directors = _uow.DirectoryRepository.GetAll().ToList();
            if (directors.Any())
            {
                Mapper.Initialize(x => x.CreateMap<director, DirectorEntity>());
                var directorsModel = Mapper.Map<List<director>, List<DirectorEntity>>(directors);
                return directorsModel;
            }
            return null;
        }

        public ICollection<DirectorGenre> GetDirectorByName(string director_name)
        {
            var director = _uow.DirectoryRepository.GetByID(director_name);
            var genres = _uow.DirectoryRepository.getAllGenreOfDirector(director_name);
            if (director != null)
            {

                Mapper.Initialize(cfg => {
                    cfg.CreateMap<director, ICollection<DirectorGenre>>()
                    .ConstructProjectionUsing(
                        p =>
                            p.Generes.Select(g => new DirectorGenre { director_name = p.director_name, genre = g.genre})
                            .ToList()
                        );
                });

                var directorSet = Mapper.Map<director, ICollection<DirectorGenre>>(director);

                return directorSet;
            }
            return null;
        }

        public bool UpdateDirector(string director_name, DirectorEntity directorEntity)
        {
            var success = false;
            using (var scope = new TransactionScope())
            {
                var director = _uow.DirectoryRepository.GetByID(director_name);
                if (director != null)
                {
                    director.director_name = directorEntity.director_name;
                    _uow.DirectoryRepository.Uppdate(director);
                    _uow.Commit();
                    scope.Complete();
                    success = true;
                }
            }
            return success;
        }
    }
    
}
