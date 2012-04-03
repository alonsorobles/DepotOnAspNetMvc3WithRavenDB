using System.Linq;
using System.Web.Mvc;
using Depot.Web.Filters;
using Depot.Web.Helpers;
using Depot.Web.Models;
using Raven.Client;
using Raven.Client.Linq;
using Depot.Web.Extensions;

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

        private Cart FindCart()
        {
            if (Session["Cart"] == null)
                Session["Cart"] = new Cart();
            return Session["Cart"] as Cart;
        }

        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            var product = _session.Load<Product>(id);
            if (product == null)
                return RedirectToAction("Index").WithFlash(FlashMessageType.Error, "Product was not found");
            var cart = FindCart();
            cart.AddProduct(product);
            return View(cart);
        }

        [HttpPost]
        public ActionResult EmptyCart()
        {
            Session["Cart"] = null;
            return RedirectToAction("Index").WithFlash(FlashMessageType.Notice, "Your cart is currently empty");
        }
    }

    
}