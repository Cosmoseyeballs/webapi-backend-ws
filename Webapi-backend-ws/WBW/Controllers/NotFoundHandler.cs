using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace WBW.Controllers
{
    public class NotFoundHandler : IExceptionHandler
    {
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            if (context.ExceptionContext.Exception.GetType() == typeof(NotFoundException))
            {
                context.Result =  new StatusCodeResult(HttpStatusCode.NotFound, context.Request);
            }
            
            return Task.FromResult(0);
            
        }
    }
}