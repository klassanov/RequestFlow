using System;
using System.Collections.Generic;
using System.Web;

namespace RequestFlow.Infrastructure
{
    public class DebugModule : IHttpModule
    {
        private static List<string> RequestUrls = new List<string>();
        private static object lockObject = new object();

        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication app)
        {
            app.BeginRequest += (src, agrs) =>
            {
                lock (lockObject)
                {
                    if (app.Request.RawUrl == "/Stats")
                    {
                        app.Response.Write(string.Format("<div>There have been {0} requests</div>", RequestUrls.Count));
                        app.Response.Write("<br />");
                        app.Response.Write(string.Format("<table><tr><th>ID</th><th>URL</th></tr>"));
                        for (int i = 0; i < RequestUrls.Count; i++)
                        {
                            app.Response.Write(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", i, RequestUrls[i]));
                        }
                        app.Response.Write(string.Format("</table>"));
                        app.CompleteRequest();
                    }

                    //Do not take into consideration requests for favicon.ico
                    else if(app.Request.RawUrl=="/favicon.ico")
                    {

                    }

                    else
                    {
                        RequestUrls.Add(app.Request.RawUrl);
                    }
                }
            };
        }
        #endregion
    }
}
