using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.UpdateProduct
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<UpdateVariantDto> Variants { get; set; } = new();

        public class UpdateVariantDto
        {
            public int? VariantId { get; set; } // Null for new variants
            public string Name { get; set; }
            public string Slug { get; set; }
            public string Image { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public bool IsPrimary { get; set; }
        }
    }
}
