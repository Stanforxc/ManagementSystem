using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Services.Interfaces;
using Domain.Entities;

namespace ManagementSystem.Controllers
{
    [EnableCors("*","*","*")]
    [Authorize]
    [RoutePrefix("api/History")]
    public class HistoryController : ApiController
    {
        private readonly IHistoryServices _historyServices;

        public HistoryController(IHistoryServices historyServices)
        {
            _historyServices = historyServices;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _historyServices.allHistory());
        }
/*
        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            //_historyServices.createHistory()
           // return Request.CreateResponse(HttpStatusCode.BadGateway, "jaj");
        }*/

        [AllowAnonymous]
        public string Post(HistoryEntity historyEntity)
        {
            return _historyServices.createHistory(historyEntity);

        }
    }
}
