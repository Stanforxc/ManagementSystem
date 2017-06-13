using Infrastructure.Data.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Entity.Validation;
using Infrastructure.Data.GenericRepository;
using Domain.Entities;

namespace Infrastructure.Data.UOW
{
    public class UOW : IUOW, IDisposable
    {
        private DataEntities _context = null;

        private GenericRepository<User> _userRepository;
        private GenericRepository<Item> _itemRepository;
        private bool disposed = false;

        public UOW()
        {
            _context = new DataEntities();
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if(this._userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }

        public GenericRepository<Item> ItemRepository
        {
            get
            {
                if (this._itemRepository == null)
                {
                    _itemRepository = new GenericRepository<Item>(_context);
                }
                return _itemRepository;
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
