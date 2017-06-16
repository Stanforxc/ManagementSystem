using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data.UOW;
using Services.Interfaces;
using Infrastructure.Data.Data;

namespace Services
{
    public class UserServices : IUserServices
    {
        private readonly UOW _uow;

        public UserServices(UOW uow)
        {
            _uow = uow;
        }

        public string createUser(UserEntity userEntity)
        {
            using(var scope = new TransactionScope())
            {
                if(userEntity != null)
                {
                    var user = new user
                    {
                        Id = userEntity.id,
                        UserName = userEntity.name,
                        PasswordHash = userEntity.passwd
                    };

                    _uow.UserRepository.Insert(user);
                    _uow.Commit();
                    scope.Complete();
                    return user.Id;
                }
                return "-1";
            }
        }

        public bool DeleteUser(int userId)
        {
            var success = false;
            if(userId > 0)
            {
                using(var scope = new TransactionScope())
                {
                    var user = _uow.UserRepository.GetByID(userId);
                    if(user != null)
                    {
                        _uow.UserRepository.Delete(user);
                        _uow.Commit();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            var users = _uow.UserRepository.GetAll().ToList();
            if (users.Any())
            {
                Mapper.Initialize(x => x.CreateMap<user, UserEntity>());
                var usersModel = Mapper.Map<List<user>, List<UserEntity>>(users);
                return usersModel;
            }
            return null;
        }

        public UserEntity GetUserById(int userId)
        {
            var user = _uow.UserRepository.GetByID(userId);
            if(user != null)
            {
                Mapper.Initialize(x => x.CreateMap<user, UserEntity>());
                var userModel = Mapper.Map<user, UserEntity>(user);
                return userModel;
            }
            return null;
        }

        public bool UpdateUser(int userId, UserEntity userEntity)
        {
            var success = false;
            using(var scope= new TransactionScope())
            {
                var user = _uow.UserRepository.GetByID(userId);
                if (user != null)
                {
                    user.UserName = userEntity.name;
                    _uow.UserRepository.Uppdate(user);
                    _uow.Commit();
                    scope.Complete();
                    success = true;
                }
            }
            return success;
        }

        
    }
}
