using System.Linq;
using System.Web.Mvc;
using Depot.Web.Extensions;
using Depot.Web.Filters;
using Depot.Web.Helpers;
using Depot.Web.Models;
using Raven.Client;

namespace Depot.Web.Controllers
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

            return RedirectToAction("Index").WithFlash(FlashMessageType.Notice, "Product was successfully created.");
        }

        public ActionResult Edit(int id)
        {
            var product = _session.Load<Product>(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var existingProduct = _session.Load<Product>(product.Id);
            existingProduct.Description = product.Description;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.Price = product.Price;
            existingProduct.Title = product.Title;

            return RedirectToAction("Show", new {id = product.Id}).WithFlash(FlashMessageType.Notice, "Product was successfully updated.");
        }

        public ActionResult Show(int id)
        {
            var product = _session.Load<Product>(id);
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var product = _session.Load<Product>(id);
            _session.Delete(product);
            return RedirectToAction("Index").WithFlash(FlashMessageType.Notice, "Product was successfully deleted.");
        }

        public ActionResult ValidateUniqueTitle(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                var isUniqueTitle = !_session.Query<Product>().Any(p => p.Title == title.Trim());
                return Json(isUniqueTitle, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}