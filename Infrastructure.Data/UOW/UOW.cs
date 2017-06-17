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

        private GenericRepository<user> _userRepository;
        private GenericRepository<movie> _movieRepository;
        private GenericRepository<director> _directorRepository;
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


        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }catch(DbEntityValidationException e)
            {
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
