using System.Collections.Generic;
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
            var cart = FindCart();
            cart.AddProduct(product);
            return View(cart);
        }
    }

    public class Cart
    {
        public Cart()
        {
            Items = new List<CartItem>();
        }

        public IList<CartItem> Items { get; private set; }

        public void AddProduct(Product product)
        {
            var currentCartItem = Items.FirstOrDefault(cartItem => cartItem.Product.Equals(product));
            if (currentCartItem != null)
                currentCartItem.IncrementQuantity();
            else
                Items.Add(new CartItem(product));
        }
    }

    public class CartItem
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        public CartItem(Product product)
        {
            Product = product;
            Quantity = 1;
        }

        public void IncrementQuantity()
        {
            Quantity += 1;
        }

        public string Title { get { return Product.Title; } }
        public decimal Price { get { return Product.Price*Quantity; } }
    }
}