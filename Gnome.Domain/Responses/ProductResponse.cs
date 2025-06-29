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
    /// Response with a single product for the FE
    /// </summary>
    [DataContract]
    public class ProductResponse
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "slug")]
        public string Slug { get; set; }
        
        [DataMember(Name = "description")]
        public string Description { get; set; }
        
        [DataMember(Name = "created-date-time")]
        public DateTime? CreatedDateTime { get; set; }
        
        [DataMember(Name = "category-id")]
        public int CategoryId { get; set; }
        
        [DataMember(Name = "category")]
        public CategoryResponse Category { get; set; }
        
        [DataMember(Name = "variants")]
        public List<VariantListResponse> Variants { get; set; } = new();
    }
} 