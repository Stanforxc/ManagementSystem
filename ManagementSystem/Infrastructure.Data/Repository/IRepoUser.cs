using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Data.GenericRepository;
using Domain.Entities;

namespace Infrastructure.Data.Repository
{
    public interface IRepoUser : IRepository<UserEntity>
    {
        //specific method
    }
}
