using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Depot.DataAnnotations;

namespace Depot.Web.Models
{
    public class ProductEditModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"\S+\.([Gg][Ii][Ff]|[Jj][Pp][Ee]?[Gg]|[Pp][Nn][Gg])$", ErrorMessage = "Must be a URL for GIF, JPG, ir PNG image.")]
        public string ImageUrl { get; set; }

        [GreaterThanOrEqualTo(typeof(decimal), "0.01")]
        public decimal Price { get; set; }
    }
}