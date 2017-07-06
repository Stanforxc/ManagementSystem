using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Entities;
using Services.Interfaces;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using System.Threading;
using ManagementSystem.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Http.Cors;

namespace ManagementSystem.Controllers
{
    [EnableCors("*","*","*")]
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

       

        private IAuthenticationManager Authentication
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

       [AllowAnonymous]
        //Get api/user
        public HttpResponseMessage Get()
        {
            var users = _userServices.GetAllUsers();
            if(users != null)
            {
                var userEntities = users as List<UserEntity> ?? users.ToList();
                if (userEntities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, userEntities);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "User not found");
        }

        public HttpResponseMessage Get(string id)
        {
            var user = _userServices.GetUserById(id);
            if(user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No user found for this id");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("create")]
        public string Post([FromBody] UserEntity userEntity)
        {
            return _userServices.createUser(userEntity);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage login([FromBody]UserEntity userEntity)
        {
            if (_userServices.GetUserById(userEntity.user_name) != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,userEntity);
            }
            return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "register first");
           
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("change")]
        public string changePassword([FromBody]UserEntity userEntity)
        {
            if(userEntity != null)
            {
                return _userServices.UpdateUser(userEntity);
            }
            return "cannot put";
        }

        public bool Delete(string id)
        {
            if (id != null)
            {
                return _userServices.DeleteUser(id);
            }
            return false;
        }
        
        /*
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<HttpResponseMessage> LoginUser([FromBody]UserEntity userEntity)
        {
            var request = HttpContext.Current.Request;
            var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "api/Token";
            using(var client = new HttpClient())
            {
                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type","password"),
                    new KeyValuePair<string, string>("username",userEntity.user_name),
                    new KeyValuePair<string, string>("password", userEntity.password)
                };
                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
                var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                var responseCode = tokenServiceResponse.StatusCode;
                var responseMsg = new HttpResponseMessage(responseCode)
                {
                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
                };
                return responseMsg;
            }
        }



        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register([FromBody]UserEntity userEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { User = userEntity };

            IdentityResult result = await UserManager.CreateAsync(user, userEntity.passwd);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Logout")]
        public HttpResponseMessage Logout()
        {
            this.Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Request.CreateResponse(HttpStatusCode.OK, "Log out successful");
        }


        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // 没有可发送的 ModelState 错误，因此仅返回空 BadRequest。
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }*/
    }
}
