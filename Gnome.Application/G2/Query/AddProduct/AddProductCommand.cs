using MediatR;
using Microsoft.AspNetCore.Http;
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
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public decimal Rating { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}
