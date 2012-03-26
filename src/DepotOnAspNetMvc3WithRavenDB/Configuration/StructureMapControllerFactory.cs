using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace DepotOnAspNetMvc3WithRavenDB.Configuration
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer _container;

        public StructureMapControllerFactory(IContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : _container.GetInstance(controllerType) as Controller;
        }
    }
}