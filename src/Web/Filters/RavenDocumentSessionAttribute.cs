using System;
using System.Web.Mvc;
using Depot.Web.Configuration;
using Raven.Client;

namespace Depot.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class RavenDocumentSessionAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
                return;
            using (var session = Bootstrapper.Container.GetInstance<IDocumentSession>())
                session.SaveChanges();
        }
    }
}