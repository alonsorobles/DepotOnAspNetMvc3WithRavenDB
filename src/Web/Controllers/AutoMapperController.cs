using System;
using System.Web.Mvc;
using AutoMapper;

namespace Depot.Web.Controllers
{
    public abstract class AutoMapperController : Controller
    {
        protected AutoMappedViewResult AutoMapView<TViewModel>(object source, ViewResult viewResult)
        {
            return new AutoMappedViewResult(source.GetType(), typeof (TViewModel), source, viewResult);
        }
    }

    public class AutoMappedViewResult : ActionResult
    {
        private readonly Type _sourceType;
        private readonly Type _destinationType;
        private readonly object _sourceObject;
        private readonly ViewResult _viewResult;

        public AutoMappedViewResult(Type sourceType, Type destinationType, object sourceObject, ViewResult viewResult)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
            _sourceObject = sourceObject;
            _viewResult = viewResult;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var viewModel = Mapper.Map(_sourceObject, _sourceType, _destinationType);
            _viewResult.ViewData.Model = viewModel;
            _viewResult.ExecuteResult(context);
        }
    }
}