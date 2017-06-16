using System.Collections.Generic;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IUserServices
    {
        UserEntity GetUserById(int userId);
        IEnumerable<UserEntity> GetAllUsers();
        string createUser(UserEntity userEntity);
        bool UpdateUser(int userId, UserEntity userEntity);
        bool DeleteUser(int userId);
    }
}
