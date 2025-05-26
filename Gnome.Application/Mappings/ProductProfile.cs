using AutoMapper;
using Gnome.Application.G2.Query.AddProduct;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductListResponse>();


            CreateMap<AddProductCommand, Product>();
            CreateMap<AddProductCommand.AddVariantDto, Variant>();
        }
    }
}
