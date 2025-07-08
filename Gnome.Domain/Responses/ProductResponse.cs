using Gnome.Domain.Models;
using Microsoft.AspNetCore.Http;
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

        [DataMember(Name = "rating")]
        public decimal Rating { get; set; }

        [DataMember(Name = "price")]
        public decimal Price { get; set; }

        [DataMember(Name = "awards")]
        public List<string> Awards { get; set; } = new();

        [DataMember(Name = "stock")]
        public int Stock { get; set; }

        [DataMember(Name = "created-date-time")]
        public DateTime? CreatedDateTime { get; set; }

        [DataMember(Name = "categories")]
        public List<CategoryResponse> Categories { get; set; } = new();

        [DataMember(Name = "images")]
        public List<ImageResponse> Images { get; set; } = new();

        [DataMember(Name = "image-count")]
        public int ImageCount { get; set; }
    }
} 