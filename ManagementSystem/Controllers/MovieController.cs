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
    [RoutePrefix("api/Movie")]
    public class MovieController : ApiController
    {
        private readonly IMovieServices _movieServices;
        
        public MovieController(IMovieServices movieServices)
        {
            _movieServices = movieServices;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("rank")]
        public HttpResponseMessage GetAllStatistics(int rank)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _movieServices.GetBest(rank));

        }


        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            var movies = _movieServices.GetAllMovies();
            if (movies != null)
            {
                var movieEntities = movies as List<MovieEntity> ?? movies.ToList();
                if (movieEntities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, movieEntities);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Movies are all not available.");
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(string movie_name)
        {
            var movie = _movieServices.GetMovieByName(movie_name);
            if (movie != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, movie);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, movie_name + " is not found");
        }

        [AllowAnonymous]
        public string Post([FromBody] MovieEntity movieEntity)
        {
            return _movieServices.createMovie(movieEntity);
        }

        [AllowAnonymous]
        public bool Put(string movie_name, [FromBody]MovieEntity movieEntity)
        {
            if (movie_name != null)
            {
                return _movieServices.UpdateMovie(movie_name, movieEntity);
            }
            return false;
        }

        [AllowAnonymous]
        public bool Delete(string movie_name)
        {
            if (movie_name != null)
            {
                return _movieServices.DeleteMovie(movie_name);
            }
            return false;
        }



    }
}
