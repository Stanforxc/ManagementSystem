using System.Collections.Generic;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IUserServices
    {
        UserEntity GetUserById(string userId);
        IEnumerable<UserEntity> GetAllUsers();
        string createUser(UserEntity userEntity);
        string UpdateUser(UserEntity userEntity);
        bool DeleteUser(string userId);
    }
}
