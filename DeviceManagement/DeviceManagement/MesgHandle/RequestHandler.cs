using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using log4csATLLib;
using System.Runtime.InteropServices;
using Service;

namespace DeviceManagement.MesgHandle
{
    public class RequestFilter: FilterAttribute, IActionFilter
    {
        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {

            var ip = actionContext.Request.Properties;

            string uri = actionContext.Request.RequestUri.AbsoluteUri;

            string method = actionContext.Request.Method.Method;

            if (!checkUri(uri)) {
                MessageService.send();
                HttpResponseMessage msg = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                //return new Task<HttpResponseMessage>(new Func<HttpResponseMessage>());
                return Task.FromResult<HttpResponseMessage>(msg);
            }

            httpRequestLogger logger = new httpRequestLogger();

            logger.serialize(method, uri);

            

            return continuation();
        }

        [DllImport("Win32.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool checkUri([MarshalAs(UnmanagedType.LPTStr)]string rootPathName);
    }
}