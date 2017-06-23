using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data.UOW;
using Services.Interfaces;
using Infrastructure.Data.Data;
using System.Diagnostics;

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
                    Mapper.Initialize(x => x.CreateMap<UserEntity, user>());
                    var user = Mapper.Map<UserEntity, user>(userEntity);
                   

                    _uow.UserRepository.Insert(user);
                    _uow.Commit();
                    scope.Complete();
                    return user.user_name;
                }
                return "-1";
            }
        }

        public bool DeleteUser(string userId)
        {
            var success = false;
            if(userId != null)
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
        /*
        public void MTCtest()
        {
            ResultsCLR cppClrResults;
            Stopwatch sw = new System.Diagnostics.Stopwatch();
            using (var testCppClr = new TorusMontecarloCLR())
            {
                sw.Start();
                cppClrResults = testCppClr.Calculate(100000);
                sw.Stop();
            }
        }*/

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

        public UserEntity GetUserById(string userId)
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

        public string UpdateUser(UserEntity userEntity)
        {
            var success = false;
            using(var scope= new TransactionScope())
            {
                var user = _uow.UserRepository.GetByID(userEntity.user_name);
                if (user != null)
                {
                    if (user!=null)
                    {
                        user.user_name = userEntity.user_name;
                    }
                    else
                    {
                        return "username exists";
                    }
                    user.password = userEntity.password;
                    _uow.UserRepository.Uppdate(user);
                    _uow.Commit();
                    scope.Complete();
                    success = true;
                }
            }
            return success.ToString();
        }

        
    }
}
