using Gnome.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Domain.Responses
{
    /// <summary>
    /// Response with all the variants for the FE
    /// </summary>
    [DataContract]
    public class VariantListResponse
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "slug")]
        public string Slug { get; set; }
        [DataMember(Name = "image")]
        public string Image { get; set; }
        [DataMember(Name = "price")]
        public decimal Price { get; set; }
        [DataMember(Name = "stock")]
        public int Stock { get; set; }
        [DataMember(Name = "is-primary")]
        public bool IsPrimary { get; set; }
        [DataMember(Name = "created-date-time")]
        public DateTime? CreatedDateTime { get; set; }
        [DataMember(Name = "product-id")]
        public int ProductId { get; set; }
    }
}
