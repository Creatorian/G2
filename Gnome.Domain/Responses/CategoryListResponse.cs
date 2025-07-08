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
    /// Response with all the categories for the FE
    /// </summary>
    [DataContract]
    public class CategoryListResponse
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "slug")]
        public string Slug { get; set; }
        [DataMember(Name = "created-date-time")]
        public DateTime? CreatedDateTime { get; set; }
        [DataMember(Name = "products-count")]
        public int ProductsCount { get; set; }
    }
}
