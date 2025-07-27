using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gnome.Domain.Models
{
    [DataContract]
    public class ProductFilter
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "slug")]
        public string Slug { get; set; }

        [DataMember(Name = "min-players")]
        public string MinPlayers { get; set; }

        [DataMember(Name = "max-players")]
        public string MaxPlayers { get; set; }

        [DataMember(Name = "min-playing-time")]
        public string MinPlayingTime { get; set; }

        [DataMember(Name = "max-playing-time")]
        public string MaxPlayingTime { get; set; }

        [DataMember(Name = "complexity")]
        public string Complexity { get; set; }

        [DataMember(Name = "rating")]
        public decimal? Rating { get; set; }

        [DataMember(Name = "min-price")]
        public decimal? MinPrice { get; set; }

        [DataMember(Name = "max-price")]
        public decimal? MaxPrice { get; set; }
        public List<string> Awards { get; set; } = new List<string>();

        [DataMember(Name = "category-ids")]
        public List<int> CategoryIds { get; set; } = new List<int>();

        [DataMember(Name = "category-slugs")]
        public List<string> CategorySlugs { get; set; } = new List<string>();

        [DataMember(Name = "in-stock-only")]
        public bool? InStockOnly { get; set; }

        [DataMember(Name = "date-from")]
        public DateTime? DateFrom { get; set; }

        [DataMember(Name = "date-to")]
        public DateTime? DateTo { get; set; } = DateTime.Now;
    }
} 