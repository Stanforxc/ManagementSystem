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
    [RoutePrefix("api/Genres")]
    public class GenresController : ApiController
    {
        private readonly IGenresServices _genresServices;
        public GenresController(IGenresServices genresServices)
        {
            _genresServices = genresServices;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            var genres = _genresServices.GetAllGenres();
            if (genres != null)
            {
                var genresEntities = genres as List<GenresEntity> ?? genres.ToList();
                if (genresEntities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, genres);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Directors are all not available.");
        }
    }
}
