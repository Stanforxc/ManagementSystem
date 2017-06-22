using System;
using System.Collections.Generic;
using System.Linq;
using Crud;
using EntityModel;
using Service;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;

namespace DeviceManagement.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LoginController : ApiController
    {

        SecurityService security = new SecurityService();

        [HttpGet]
        [Route("check")]
        public Boolean login(string userid, string pwd){
            return security.authentication(userid, pwd);
        }

    }
}