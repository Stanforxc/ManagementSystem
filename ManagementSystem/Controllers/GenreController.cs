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
    [RoutePrefix("api/Genre")]
    public class GenreController : ApiController
    {
        private readonly IGenreServices _genreServices;
        public GenreController(IGenreServices genreServices)
        {
            _genreServices = genreServices;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            var genres = _genreServices.GetAllGenres();
            if (genres != null)
            {
                var genreEntities = genres as List<GenreEntity> ?? genres.ToList();
                if (genreEntities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, genreEntities);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Movies are all not available.");
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(string genreStyle)
        {
            var genre = _genreServices.GetGenreByName(genreStyle);
            if (genre != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, genre);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, genreStyle + " is not found");
        }

        [AllowAnonymous]
        public string Post([FromBody]GenreEntity genreEntity)
        {
            return _genreServices.createGenre(genreEntity);
        }

        [AllowAnonymous]
        public bool Delete(string genre_style)
        {
            if (genre_style != null)
            {
                return _genreServices.DeleteGenre(genre_style);
            }
            return false;
        }

    }
}
