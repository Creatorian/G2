using Gnome.Application.Shared;
using Gnome.Domain.Common;
using Gnome.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gnome.Application.G2.Query.ListProducts
{
    [DataContract]
    public class ListProductsQueryCommand : PagedQuery<SortedPagedList<ProductListResponse>>
    {
        [DataMember(Name = "dateFrom")]
        public DateTime? DateFrom { get; set; }

        [DataMember(Name = "dateTo")]
        public DateTime? DateTo { get; set; } = DateTime.Now;

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "slug")]
        public string Slug { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "shortDescription")]
        public string ShortDescription { get; set; }

        [DataMember(Name = "numberOfPlayers")]
        public string NumberOfPlayers { get; set; }

        [DataMember(Name = "playingTime")]
        public string PlayingTime { get; set; }

        [DataMember(Name = "communityAge")]
        public string CommunityAge { get; set; }

        [DataMember(Name = "complexity")]
        public string Complexity { get; set; }

        [DataMember(Name = "minRating")]
        public decimal? MinRating { get; set; }

        [DataMember(Name = "maxRating")]
        public decimal? MaxRating { get; set; }

        [DataMember(Name = "minPrice")]
        public decimal? MinPrice { get; set; }

        [DataMember(Name = "maxPrice")]
        public decimal? MaxPrice { get; set; }

        [DataMember(Name = "minStock")]
        public int? MinStock { get; set; }

        [DataMember(Name = "maxStock")]
        public int? MaxStock { get; set; }

        [DataMember(Name = "awards")]
        public List<string> Awards { get; set; }

        [DataMember(Name = "categoryIds")]
        public List<int> CategoryIds { get; set; }

        [DataMember(Name = "categoryNames")]
        public List<string> CategoryNames { get; set; }

        [DataMember(Name = "hasImages")]
        public bool? HasImages { get; set; }

        [DataMember(Name = "inStockOnly")]
        public bool? InStockOnly { get; set; }
    }
}
