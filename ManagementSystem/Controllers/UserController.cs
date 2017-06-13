using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Entities;
using Services.Interfaces;
using Services;

namespace ManagementSystem.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserServices _userServices;

        public UserController()
        {
            _userServices = new UserServices();
        }

        //Get api/user
        public HttpResponseMessage Get()
        {
            var users = _userServices.GetAllUsers();
            if(User != null)
            {
                var userEntities = users as List<UserEntity> ?? users.ToList();
                if (userEntities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, userEntities);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found");
        }

        public HttpResponseMessage Get(int id)
        {
            var user = _userServices.GetUserById(id);
            if(user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No user found for this id");
        }

        public int Post([FromBody] UserEntity userEntity)
        {
            return _userServices.createUser(userEntity);
        }

        public bool Put(int id,[FromBody]UserEntity userEntity)
        {
            if(id > 0)
            {
                return _userServices.UpdateUser(id, userEntity);
            }
            return false;
        }

        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _userServices.DeleteUser(id);
            }
            return false;
        }
    }
}
