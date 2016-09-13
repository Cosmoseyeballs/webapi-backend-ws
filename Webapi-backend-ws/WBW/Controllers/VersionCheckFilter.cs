using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WBW.Controllers
{
    public class VersionCheckFilter : IActionFilter
    {
        public bool AllowMultiple
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext,
                                                                    CancellationToken cancellationToken,
                                                                    Func<Task<HttpResponseMessage>> continuation)
        {

            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("X-Version", out values) && values.First() != "42")
            {
                return new HttpResponseMessage((HttpStatusCode)418);
            }
            HttpResponseMessage response = await continuation();
            return response;


        }
    }
}