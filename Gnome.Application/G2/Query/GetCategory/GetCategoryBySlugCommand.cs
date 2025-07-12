using Gnome.Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.GetCategory
{
    [DataContract]
    public class GetCategoryBySlugCommand : IRequest<CategoryResponse>
    {
        [DataMember(Name = "slug")]
        public string Slug { get; set; }
    }
} 