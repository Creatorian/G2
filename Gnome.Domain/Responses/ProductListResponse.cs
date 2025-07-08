using Gnome.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Domain.Responses
{
    /// <summary>
    /// Response with all the products for the FE
    /// </summary>
    [DataContract]
    public class ProductListResponse
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "slug")]
        public string Slug { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "short-description")]
        public string ShortDescription { get; set; }
        [DataMember(Name = "price")]
        public decimal Price { get; set; }
        [DataMember(Name = "stock")]
        public int Stock { get; set; }
        [DataMember(Name = "rating")]
        public decimal Rating { get; set; }
        [DataMember(Name = "created-date-time")]
        public DateTime? CreatedDateTime { get; set; }
        [DataMember(Name = "categories")]
        public List<CategoryListResponse> Categories { get; set; } = new();
        [DataMember(Name = "images")]
        public List<ImageResponse> Images { get; set; } = new();
        [DataMember(Name = "image-count")]
        public int ImageCount { get; set; }
    }
}
