using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WBW.Controllers
{
    public class VersionCheckHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Process request");

            IEnumerable<string> values;
            if (request.Content.Headers.TryGetValues("X-Version", out values) && values.First() != "42")
            {
                return Task.FromResult(new HttpResponseMessage( (HttpStatusCode)418));
            }

            var ret = base.SendAsync(request, cancellationToken);
            Debug.WriteLine("Process response");
            return ret;
        }
    }
}