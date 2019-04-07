using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace RequestFlow.Infrastructure
{
    public class HandlerSelectionModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication app)
        {
            app.PostResolveRequestCache += (src, args) =>
              {
                  if (!Compare(app.Context.Request.RequestContext.RouteData.Values, "controller", "Home"))
                  {
                      app.Context.RemapHandler(new InfoHandler());
                  }
              };
        }

        private bool Compare(RouteValueDictionary rvd, string key, string value)
        {
            return string.Equals((string)rvd[key], value, StringComparison.OrdinalIgnoreCase);
        }
    }
}