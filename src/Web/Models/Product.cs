using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Depot.Web.DataAnnotations;

namespace Depot.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
        
        [Required]
        [MinimumValue(typeof(decimal), "0.01", "Should be at least 0.01.")]
        public decimal Price { get; set; }
    }
}