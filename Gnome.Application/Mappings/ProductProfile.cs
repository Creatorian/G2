using AutoMapper;
using Gnome.Application.G2.Query.AddProduct;
using Gnome.Application.G2.Query.UpdateProduct;
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
            CreateMap<Product, ProductListResponse>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<Image, ImageResponse>();

            CreateMap<AddProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
        }
    }
}