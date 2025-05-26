using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.AddProduct
{
    [DataContract]
    public class AddProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<AddVariantDto> Variants { get; set; } = new();

        public class AddVariantDto
        {
            public string Name { get; set; }
            public string Slug { get; set; }
            public string Image { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public bool IsPrimary { get; set; }
        }
    }
}
