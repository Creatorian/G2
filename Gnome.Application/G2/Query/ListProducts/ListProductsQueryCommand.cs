using Gnome.Application.Shared;
using Gnome.Domain.Common;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.ListProducts
{
    [DataContract]
    public class ListProductsQueryCommand : PagedQuery<SortedPagedList<ProductListResponse>>
    {
        [DataMember(Name = "filter")]
        public ProductFilter Filter { get; set; } = new ProductFilter();
    }
}
