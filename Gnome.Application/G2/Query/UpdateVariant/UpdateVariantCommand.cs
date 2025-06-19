using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.UpdateVariant
{
    [DataContract]
    public class UpdateVariantCommand : IRequest<int>
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "slug")]
        public string Slug { get; set; }
        
        [DataMember(Name = "image")]
        public IFormFile Image { get; set; }
        
        [DataMember(Name = "price")]
        public decimal Price { get; set; }
        
        [DataMember(Name = "stock")]
        public int Stock { get; set; }
        
        [DataMember(Name = "is-primary")]
        public bool IsPrimary { get; set; }
        
        [DataMember(Name = "product-id")]
        public int ProductId { get; set; }
    }
} 