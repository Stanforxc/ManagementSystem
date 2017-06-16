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

namespace ManagementSystem.Controllers
{
    [Authorize]
    [RoutePrefix("api/Director")]
    public class DirectorController : ApiController
    {
        private readonly IDirectorServices _directorServices;
        public DirectorController(IDirectorServices directorServices)
        {
            _directorServices = directorServices;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            var directors = _directorServices.GetAllDirectors();
            if (directors != null)
            {
                var directorEntities = directors as List<DirectorEntity> ?? directors.ToList();
                if (directorEntities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, directors);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Directors are all not available.");
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(string director_name)
        {
            var director = _directorServices.GetDirectorByName(director_name).ToList();
            if (director != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, director);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, director_name + " is not found");
        }

        [AllowAnonymous]
        public bool Post([FromBody] DirectorEntity directorEntity)
        {
            return _directorServices.createDirector(directorEntity);
        }
    }
}
