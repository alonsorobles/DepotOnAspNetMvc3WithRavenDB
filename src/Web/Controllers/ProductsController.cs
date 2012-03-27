﻿using System.Linq;
using System.Web.Mvc;
using Depot.Web.Filters;
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
            
            return RedirectToAction("Index");
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

            return RedirectToAction("Show", new {id = product.Id});
        }

        public ActionResult Show(int id)
        {
            var product = _session.Load<Product>(id);
            return View(product);
        }
    }
}