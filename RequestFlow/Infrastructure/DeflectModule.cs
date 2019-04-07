using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RequestFlow.Infrastructure
{
    public class DeflectModule : IHttpModule
    {
        public void Dispose()
        {
           
        }

        public void Init(HttpApplication app)
        {
            app.BeginRequest += (src, args) =>
              {
                  if (app.Request.RawUrl.ToLower().StartsWith("/home"))
                  {
                      app.Response.StatusCode = 500;
                      app.CompleteRequest();
                  }
              };
        }
    }
}