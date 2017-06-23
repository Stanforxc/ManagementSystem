using Infrastructure.Data.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Entity.Validation;
using Infrastructure.Data.GenericRepository;
using Domain.Entities;
using Infrastructure.Data.Repository;

namespace Infrastructure.Data.UOW
{
    public class UOW : IUOW, IDisposable
    {
        private DataEntities _context = null;

        //实体仓库
        private GenericRepository<user> _userRepository;
        private GenericRepository<movie> _movieRepository;
        private GenericRepository<director> _directorRepository;
        private GenericRepository<genre> _genreRepositroy;
        private GenericRepository<history> _historyRepositroy;

        //关系映射仓库
        private GenericRepository<directorGenre> _directorGenreRepository;
        private GenericRepository<directorMovie> _directorMovieRepository;
        private GenericRepository<movieDirector> _movieDirectorRepository;
        private GenericRepository<movieGenre> _movieGenreRepository;
        private GenericRepository<genreMovie> _genreMovieRepository;
        private GenericRepository<genreDirector> _genreDirectorRepository;
        private bool disposed = false;

        public UOW()
        {
            _context = new DataEntities();
        }

        public GenericRepository<user> UserRepository
        {
            get
            {
                if(this._userRepository == null)
                {
                    _userRepository = new GenericRepository<user>(_context);
                }
                return _userRepository;
            }
        }

        public GenericRepository<movie> MovieRepository
        {
            get
            {
                if(this._movieRepository == null)
                {
                    _movieRepository = new GenericRepository<movie>(_context);
                }
                return _movieRepository;
            }
        }

        public GenericRepository<director> DirectoryRepository
        {
            get
            {
                if (this._directorRepository == null)
                {
                    _directorRepository = new GenericRepository<director>(_context);
                }
                return _directorRepository;
            }
        }

        public GenericRepository<directorGenre> DirectorGenreRepository {
            get
            {
                if (this._directorGenreRepository == null)
                {
                    _directorGenreRepository = new GenericRepository<directorGenre>(_context);
                }
                return _directorGenreRepository;
            }
        }

        public GenericRepository<directorMovie> DirectorMovieRepository {
            get
            {
                if(this._directorMovieRepository == null)
                {
                    _directorMovieRepository = new GenericRepository<directorMovie>(_context);
                }
                return _directorMovieRepository;
            }
        }

        public GenericRepository<movieDirector> MovieDirectorRepository {
            get
            {
                if(this._movieDirectorRepository == null)
                {
                    _movieDirectorRepository = new GenericRepository<movieDirector>(_context);
                }
                return _movieDirectorRepository;
            }
        }

        public GenericRepository<movieGenre> MovieGenreRepository {
            get
            {
                if(this._movieGenreRepository == null)
                {
                    _movieGenreRepository = new GenericRepository<movieGenre>(_context);
                }
                return _movieGenreRepository;
            }
        }

        public GenericRepository<genreMovie> GenreMovieRepository {
            get
            {
                if(this._genreMovieRepository == null)
                {
                    _genreMovieRepository = new GenericRepository<genreMovie>(_context);
                }
                return _genreMovieRepository;
            }
        }

        public GenericRepository<genre> GenreRepository {
            get
            {
                if(this._genreRepositroy == null)
                {
                    _genreRepositroy = new GenericRepository<genre>(_context);
                }
                return _genreRepositroy;
            }
        }

        public GenericRepository<genreDirector> GenreDirectorRepository {
            get
            {
                if(this._genreDirectorRepository == null)
                {
                    _genreDirectorRepository = new GenericRepository<genreDirector>(_context);
                }
                return _genreDirectorRepository;
            }
        }

        public GenericRepository<history> HistoryRepository {
            get
            {
                if (this._historyRepositroy == null)
                {
                    _historyRepositroy = new GenericRepository<history>(_context);
                }
                return _historyRepositroy;
            }
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }catch(DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UOW is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
