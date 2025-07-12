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

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "short-description")]
        public string ShortDescription { get; set; }

        [DataMember(Name = "number-of-players")]
        public string NumberOfPlayers { get; set; }

        [DataMember(Name = "playing-time")]
        public string PlayingTime { get; set; }

        [DataMember(Name = "community-age")]
        public string CommunityAge { get; set; }

        [DataMember(Name = "complexity")]
        public string Complexity { get; set; }

        [DataMember(Name = "min-rating")]
        public decimal? MinRating { get; set; }

        [DataMember(Name = "max-rating")]
        public decimal? MaxRating { get; set; }

        [DataMember(Name = "min-price")]
        public decimal? MinPrice { get; set; }

        [DataMember(Name = "max-price")]
        public decimal? MaxPrice { get; set; }

        [DataMember(Name = "min-stock")]
        public int? MinStock { get; set; }

        [DataMember(Name = "max-stock")]
        public int? MaxStock { get; set; }

        [DataMember(Name = "awards")]
        public List<string> Awards { get; set; } = new List<string>();

        [DataMember(Name = "category-ids")]
        public List<int> CategoryIds { get; set; } = new List<int>();

        [DataMember(Name = "category-names")]
        public List<string> CategoryNames { get; set; } = new List<string>();

        [DataMember(Name = "has-images")]
        public bool? HasImages { get; set; }

        [DataMember(Name = "in-stock-only")]
        public bool? InStockOnly { get; set; }

        [DataMember(Name = "date-from")]
        public DateTime? DateFrom { get; set; }

        [DataMember(Name = "date-to")]
        public DateTime? DateTo { get; set; } = DateTime.Now;
    }
} 