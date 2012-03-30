using System.ComponentModel.DataAnnotations;

namespace Depot.Web.Models
{
    public class ProductListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
    }
}