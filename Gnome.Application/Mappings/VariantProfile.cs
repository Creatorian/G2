using AutoMapper;
using Gnome.Application.G2.Query.AddVariant;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Mappings
{
    public class VariantProfile : Profile
    {
        public VariantProfile()
        {
            CreateMap<Variant, VariantListResponse>();
            CreateMap<AddVariantCommand, Variant>();
        }
    }
}
