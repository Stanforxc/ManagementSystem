using System.Collections.Generic;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IUserServices
    {
        UserEntity GetUserById(int userId);
        IEnumerable<UserEntity> GetAllUsers();
        int createUser(UserEntity userEntity);
        bool UpdateUser(int userId, UserEntity userEntity);
        bool DeleteUser(int userId);

        int Authenticate(string userName, string password);
    }
}
