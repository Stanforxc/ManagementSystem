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
        
        public DirectorServices(UOW uow)
        {
            _uow = uow;
        }
        public string createDirector(DirectorEntity directorEntity)
        {
            using (var scope = new TransactionScope())
            {
                if (directorEntity != null)
                {
                    if (!_uow.DirectoryRepository.Exists(directorEntity.director_name))
                    {
                        director director = DirectorMapperInitializer(directorEntity);
                        _uow.DirectoryRepository.Insert(director);
                        if (director.directorMovies.Count() > 0)
                        {
                           ICollection<directorMovie> directorMovies = DirectorMovieMapperInitializer(directorEntity);
                           _uow.DirectorMovieRepository.InsertBatch(directorMovies);
                        }

                        if (director.directorGenres.Count() > 0)
                        {
                            ICollection<directorGenre> directorGenres = DirectorGenreMapperIntializer(directorEntity);
                            _uow.DirectorGenreRepository.InsertBatch(directorGenres);
                        }


                        _uow.Commit();
                        scope.Complete();

                        return director.director_name;
                    }
                    else
                    {
                        return "director exists";
                    }
                }

                return "-1";
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
                Mapper.Initialize(x => x.CreateMap<director, DirectorEntity>()
                .ForMember(dest => dest.directorGenres, opt => opt.Ignore())
                .ForMember(dest => dest.directorMovies, opt => opt.Ignore()));
                var directorsModel = Mapper.Map<ICollection<director>, ICollection<DirectorEntity>>(directors);
                return directorsModel;
            }
            return null;
        }

        public ICollection<DirectorGenre> GetDirectorByName(string director_name)
        {
            throw new NotImplementedException();
        }

        /*
public ICollection<Domain.Entities.directorGenre> GetDirectorByName(string director_name)
{
   var director = _uow.DirectoryRepository.GetByID(director_name);
   //var genres = _uow.DirectoryRepository.getAllGenreOfDirector(director_name);
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

       var directorSet = Mapper.Map<director, ICollection<Domain.Entities.directorGenre>>(director);

       return directorSet;
   }
   return null;
}*/

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

        private director DirectorMapperInitializer(DirectorEntity directorEntity)
        {
            Mapper.Initialize(x=>x.CreateMap<DirectorEntity,director>()
                .ForMember(dest => dest.directorMovies,opt => opt.Ignore())
                .ForMember(dest => dest.directorGenres,opt => opt.Ignore()));
            return Mapper.Map<DirectorEntity, director>(directorEntity);
        }

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
    }
    
}
