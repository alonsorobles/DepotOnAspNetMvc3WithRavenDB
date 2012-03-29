using System;
using System.ComponentModel.DataAnnotations;
using Depot.Extensions;
using Raven.Client;

namespace Depot.DataLoader
{
    public class ProductDataLoader : IDataLoader
    {
        private static readonly Product[] Products = new[]
                                                 {
                                                     new Product
                                                         {
                                                             Title = "Pragmatic Project Automation",
                                                             Description =
                                                                 @"<p><em>Pragmatic Project Automation</em> shows you how to improve the consistency and repeatability of your project's procedures using automation to reduce risk and errors.</p><p>Simply put, we're going to put this thing called a computer to work for you doing the mundane (but important) project stuff. That means you'll have more time and energy to do the really exciting---and difficult---stuff, like writing quality code.</p>",
                                                             Id = 1,
                                                             ImageUrl = "/images/auto.jpg",
                                                             Price = 29.95m
                                                         },
                                                     new Product
                                                         {
                                                             Title = "Pragmatic Version Control",
                                                             Description =
                                                                 @"<p>This book is a recipe-based approach to using Subversion that will get you up and running quickly---and correctly. All projects need version control: it's a foundational piece of any project's infrastructure. Yet half of all project teams in the U.S. don't use any version control at all. Many others don't use it well, and end up experiencing time-consuming problems.</p>",
                                                             Id = 2,
                                                             ImageUrl = "/images/svn.jpg",
                                                             Price = 28.50m
                                                         },
                                                     new Product
                                                         {
                                                             Title = "Pragmatic Unit Testing (C#)",
                                                             Description =
                                                                 @"<p>Pragmatic programmers use feedback to drive their development and personal processes. The most valuable feedback you can get while coding comes from unit testing.</p><p>Without good tests in place, coding can become a frustrating game of ""whack-a-mole."" That's the carnival game where the player strikes at a mechanical mole; it retreats and another mole pops up on the opposite side of the field. The moles pop up and down so fast that you end up flailing your mallet helplessly as the moles continue to pop up where you least expect them.</p>",
                                                             Id = 3,
                                                             ImageUrl = "/images/utc.jpg",
                                                             Price = 27.75m
                                                         }
                                                 };

        private readonly IDocumentSession _session;

        public ProductDataLoader(IDocumentSession session)
        {
            _session = session;
        }

        public void Load()
        {
            Products.Each(Validate);
            Products.Each(product => _session.Store(product));
            _session.SaveChanges();
        }

        private static void Validate(Product product)
        {
            var validationContext = new ValidationContext(product, null, null);
            Validator.ValidateObject(product, validationContext);
        }

        public void UnLoad()
        {
            Products.Each(MarkForDeletion);
            _session.SaveChanges();
        }

        private void MarkForDeletion(Product product)
        {
            var existingProduct = _session.Load<Product>(product.Id);
            if (existingProduct != null)
                _session.Delete(existingProduct);
        }
    }
}