using Gnome.Application.Shared;
using Gnome.Domain.Common;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using System.Runtime.Serialization;
using System;

namespace Gnome.Application.G2.Query.ListCategories
{
    [DataContract]
    public class ListCategoriesQueryCommand : PagedQuery<SortedPagedList<CategoryListResponse>>
    {
        [DataMember(Name = "filter")]
        public CategoryFilter Filter { get; set; } = new CategoryFilter();
    }
}