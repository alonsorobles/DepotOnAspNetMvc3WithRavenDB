using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Depot.Web.Models
{
    public class Cart
    {
        public Cart()
        {
            Items = new List<CartItem>();
        }

        public IList<CartItem> Items { get; private set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalPrice
        {
            get { return 0.00m + Items.Sum(cartItem => cartItem.Price); }
        }

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

        public string Title
        {
            get { return Product.Title; }
        }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price
        {
            get { return Product.Price*Quantity; }
        }
    }
}