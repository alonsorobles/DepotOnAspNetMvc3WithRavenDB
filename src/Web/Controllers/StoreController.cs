using System.Linq;
using System.Web.Mvc;
using Depot.Web.Filters;
using Depot.Web.Models;
using Raven.Client;
using Raven.Client.Linq;

namespace Depot.Web.Controllers
{
    [RavenDocumentSession]
    public class StoreController: AutoMapperController
    {
        private readonly IDocumentSession _session;

        public StoreController(IDocumentSession session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            var products = _session.Query<Product>().OrderBy(product => product.Title).ToArray();
            return AutoMapView<ProductListModel[]>(products, View());
        }
    }
}