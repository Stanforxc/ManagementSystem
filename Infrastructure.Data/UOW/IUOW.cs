using Infrastructure.Data.Data;
using Infrastructure.Data.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.UOW
{
    public interface IUOW
    {
        void Commit();
        GenericRepository<User> UserRepository { get; }
        GenericRepository<Item> ItemRepository { get; }

    }
}
