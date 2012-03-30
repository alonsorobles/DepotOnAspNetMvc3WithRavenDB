using System.ComponentModel.DataAnnotations;
using Depot.DataAnnotations;

namespace Depot
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"(?i:\S+\.(gif|jpg|jpeg|png)$)", ErrorMessage = "Must be a URL for GIF, JPG, ir PNG image.")]
        public string ImageUrl { get; set; }

        [GreaterThanOrEqualTo(typeof(decimal), "0.01")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
    }
}