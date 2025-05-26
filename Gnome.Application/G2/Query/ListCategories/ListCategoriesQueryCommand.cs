using Gnome.Application.Shared;
using Gnome.Domain.Common;
using Gnome.Domain.Responses;
using System.Runtime.Serialization;
using System;

namespace Gnome.Application.G2.Query.ListCategories
{
    [DataContract]
    public class ListCategoriesQueryCommand : PagedQuery<SortedPagedList<CategoryListResponse>>
    {
        [DataMember(Name = "dateFrom")]
        public DateTime DateFrom { get; set; }

        [DataMember(Name = "dateTo")]
        public DateTime DateTo { get; set; } = DateTime.Now;

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}