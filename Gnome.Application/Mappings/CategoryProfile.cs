using AutoMapper;
using Gnome.Application.G2.Query.AddCategory;
using Gnome.Application.G2.Query.UpdateCategory;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.ProductCategories.Count));

            CreateMap<Category, CategoryListResponse>()
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.ProductCategories.Count));

            CreateMap<AddCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}
