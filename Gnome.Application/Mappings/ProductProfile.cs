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
            CreateMap<Product, ProductListResponse>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<UpdateProductCommand.UpdateVariantDto, Variant>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VariantId ?? 0));
        }
    }
}
