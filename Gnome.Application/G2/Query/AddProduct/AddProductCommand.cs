using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.AddProduct
{
    [DataContract]
    public class AddProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string NumberOfPlayers { get; set; }
        public string PlayingTime { get; set; }
        public int CommunityAge { get; set; }
        public decimal Complexity { get; set; }
        public decimal Rating { get; set; }
        public decimal Price { get; set; }
        private List<string> _awards = new List<string>();
        public List<string> Awards 
        { 
            get => _awards;
            set => _awards = value;
        }
        
        // Property to handle comma-separated string from form data
        public string AwardsString
        {
            get => string.Join(",", _awards);
            set => _awards = !string.IsNullOrEmpty(value) ? value.Split(',').Select(s => s.Trim()).ToList() : new List<string>();
        }
        public int Stock { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}
