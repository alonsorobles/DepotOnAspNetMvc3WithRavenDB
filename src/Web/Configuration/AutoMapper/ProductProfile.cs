using AutoMapper;
using Depot.Web.Models;

namespace Depot.Web.Configuration.Automapper
{
    public class ProductProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<Product, ProductListModel>();
            CreateMap<Product, ProductEditModel>();
            CreateMap<Product, ProductShowModel>();
            CreateMap<ProductEditModel, Product>();
        }
    }
}