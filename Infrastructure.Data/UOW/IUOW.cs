using Infrastructure.Data.Data;
using Infrastructure.Data.GenericRepository;
using Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.UOW
{
    public interface IUOW
    {
        void Commit();
        GenericRepository<user> UserRepository { get; }
        GenericRepository<movie> MovieRepository { get; }
        RepoDirector DirectoryRepository { get; }
        RepoGenres GenereRepository { get; }

    }
}
