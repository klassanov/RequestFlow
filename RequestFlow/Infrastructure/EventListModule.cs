using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RequestFlow.Infrastructure
{
    public class EventListModule : IHttpModule
    {
        private List<string> ignoredExtensions = new List<string>() { ".css", ".ico" };

        public void Dispose()
        {

        }

        public void Init(HttpApplication app)
        {
            app.Error += (src, args) =>
            {
                WriteEvent("Error");
            };

            string[] events =
            {
                "BeginRequest", "AuthenticateRequest", "PostAuthenticateRequest", "AuthorizeRequest",
                "ResolveRequestCache", "PostResolveRequestCache", "MapRequestHandler", "PostMapRequestHandler",
                "AcquireRequestState", "PostAcquireRequestState", "PreRequestHandlerExecute",
                "PostRequestHandlerExecute", "ReleaseRequestState", "PostReleaseRequestState",
                "UpdateRequestCache", "LogRequest", "PostLogRequest", "EndRequest",
                "PreSendRequestHeaders", "PreSendRequestContent"
            };

            MethodInfo methodInfo = GetType().GetMethod("HandleEvent");
            foreach (string eventName in events)
            {
                EventInfo eventInfo = app.GetType().GetEvent(eventName);
                if (eventInfo != null)
                {
                    eventInfo.AddEventHandler(app,
                        Delegate.CreateDelegate(eventInfo.EventHandlerType, this, methodInfo));
                }
            }

        }

        public void HandleEvent(object src, EventArgs args)
        {
            string eventName = HttpContext.Current.CurrentNotification.ToString();
            if (HttpContext.Current.IsPostNotification &&
                !ignoredExtensions.Contains(HttpContext.Current.Request.CurrentExecutionFilePathExtension))
            {
                eventName = string.Format("Post{0}", eventName);
            }

            if (eventName == "BeginRequest")
            {
                Write("-----------------------");
            }

            WriteEvent(eventName);
        }

        private void WriteEvent(string eventName)
        {
            Write(string.Format("Event: {0}", eventName));
        }

        private void Write(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }
    }
}