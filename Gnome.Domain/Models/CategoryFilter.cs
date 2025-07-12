using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gnome.Domain.Models
{
    [DataContract]
    public class CategoryFilter
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "slug")]
        public string Slug { get; set; }

        [DataMember(Name = "has-products")]
        public bool? HasProducts { get; set; }

        [DataMember(Name = "min-products-count")]
        public int? MinProductsCount { get; set; }

        [DataMember(Name = "max-products-count")]
        public int? MaxProductsCount { get; set; }

        [DataMember(Name = "date-from")]
        public DateTime? DateFrom { get; set; }

        [DataMember(Name = "date-to")]
        public DateTime? DateTo { get; set; } = DateTime.Now;
    }
} 