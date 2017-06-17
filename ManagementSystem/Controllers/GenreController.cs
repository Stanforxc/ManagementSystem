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
    [RoutePrefix("api/Genre")]
    public class GenreController : ApiController
    {
        private readonly IGenreServices _genreServices;
        public GenreController(IGenreServices genreServices)
        {
            _genreServices = genreServices;
        }

        [AllowAnonymous]
        public string Post([FromBody]GenreEntity genreEntity)
        {
            return _genreServices.createGenre(genreEntity);
        }
    }
}
