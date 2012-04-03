﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Depot.DataAnnotations;

namespace Depot.Web.Models
{
    public class ProductEditModel
    {
        public int Id { get; set; }
        
        [Required]
        [Remote("ValidateUniqueTitle", "Products", ErrorMessage = "Title is not available.")]
        public string Title { get; set; }
        
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"\S+\.(gif|jpg|jpeg|png)$", ErrorMessage = "Must be a URL for GIF, JPG, ir PNG image.")]
        public string ImageUrl { get; set; }

        [GreaterThanOrEqualTo(typeof(decimal), "0.01")]
        public decimal Price { get; set; }
    }
}