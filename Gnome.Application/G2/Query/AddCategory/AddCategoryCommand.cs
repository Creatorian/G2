using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.AddCategory
{
    [DataContract]
    public class AddCategoryCommand : IRequest<int>
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "slug")]
        public string Slug { get; set; }
    }
} 