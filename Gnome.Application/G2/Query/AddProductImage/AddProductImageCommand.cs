using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.AddProductImage
{
    [DataContract]
    public class AddProductImageCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public bool SetAsPrimary { get; set; } = false;
    }
} 