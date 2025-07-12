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
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.ImageCount, opt => opt.MapFrom(src => src.Images.Count))
                .ForMember(dest => dest.Awards, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Awards)
                        ? new List<string>()
                        : src.Awards.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(a => a.Trim())
                            .Where(a => !string.IsNullOrEmpty(a))
                            .ToList()
                ));
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.ImageCount, opt => opt.MapFrom(src => src.Images.Count))
                .ForMember(dest => dest.Awards, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Awards)
                        ? new List<string>()
                        : src.Awards.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(a => a.Trim())
                            .Where(a => !string.IsNullOrEmpty(a))
                            .ToList()
                ));

            CreateMap<Image, ImageResponse>();

            CreateMap<AddProductCommand, Product>()
                .ForMember(dest => dest.Awards, opt => opt.MapFrom(src =>
                    src.Awards != null && src.Awards.Any()
                        ? string.Join(",", src.Awards.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => a.Trim()))
                        : null
                ));
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Awards, opt => opt.MapFrom(src =>
                    src.Awards != null && src.Awards.Any()
                        ? string.Join(",", src.Awards.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => a.Trim()))
                        : null
                ));
        }
    }
}