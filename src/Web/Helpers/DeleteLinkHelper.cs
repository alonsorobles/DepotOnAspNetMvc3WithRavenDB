using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Xml.Linq;

namespace Depot.Web.Helpers
{
    public static class DeleteLinkHelper
    {
        private static readonly Dictionary<string, Object> DefaultDeleteLinkHtmlAttributes =
            new Dictionary<string, Object> {{"delete-link", "true"}, {"delete-link-method", "delete"}};

        public static MvcHtmlString DeleteLink(this HtmlHelper instance,
                                               string linkText,
                                               string actionName,
                                               Object routeValues,
                                               DeleteLinkOptions options = null)
        {
            var routeValueDictonary = routeValues == null ? new RouteValueDictionary() : new RouteValueDictionary(routeValues);
            var htmlAttributes = new Dictionary<string, Object>(DefaultDeleteLinkHtmlAttributes);
            if (options != null)
            {
                htmlAttributes["delete-link-method"] = options.HttpMethod.ToString().ToUpper();
                if (!string.IsNullOrWhiteSpace(options.Confirm))
                    htmlAttributes.Add("delete-link-confirm", options.Confirm);
                if (options.Parents > 0)
                    htmlAttributes.Add("delete-link-num-parents", options.Parents);

            }
            return instance.ActionLink(linkText,
                                       actionName,
                                       routeValueDictonary,
                                       htmlAttributes);
        }
    }

    public class DeleteLinkOptions
    {
        public DeleteLinkOptions()
        {
            HttpMethod = HttpVerbs.Delete;
            Confirm = null;
            Parents = 0;
        }

        public string Confirm { get; set; }
        public int Parents { get; set; }
        public HttpVerbs HttpMethod { get; set; }
    }
}