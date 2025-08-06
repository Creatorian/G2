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

        [DataMember(Name = "minPlayers")]
        public string MinPlayers { get; set; }

        [DataMember(Name = "maxPlayers")]
        public string MaxPlayers { get; set; }

        [DataMember(Name = "minPlayingTime")]
        public string MinPlayingTime { get; set; }

        [DataMember(Name = "maxPlayingTime")]
        public string MaxPlayingTime { get; set; }

        [DataMember(Name = "complexity")]
        public decimal? Complexity { get; set; }

        [DataMember(Name = "rating")]
        public decimal? Rating { get; set; }

        [DataMember(Name = "minPrice")]
        public decimal? MinPrice { get; set; }

        [DataMember(Name = "maxPrice")]
        public decimal? MaxPrice { get; set; }

        [DataMember(Name = "categoryIds")]
        public List<int> CategoryIds { get; set; }

        [DataMember(Name = "categorySlugs")]
        public List<string> CategorySlugs { get; set; }

        [DataMember(Name = "inStockOnly")]
        public bool? InStockOnly { get; set; }
    }
}
