using System;
using System.Linq;
using System.Web.Mvc;
using DepotOnAspNetMvc3WithRavenDB.Filters;
using DepotOnAspNetMvc3WithRavenDB.Models;
using Raven.Client;
using Raven.Client.Linq;

namespace DepotOnAspNetMvc3WithRavenDB.Controllers
{
    [RavenDocumentSession]
    public class ProductsController: Controller
    {
        private readonly IDocumentSession _session;

        public ProductsController(IDocumentSession session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            var products = _session.Query<Product>().ToArray();
            return View(products);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _session.Store(product);
            _session.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}