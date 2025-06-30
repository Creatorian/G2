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
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
