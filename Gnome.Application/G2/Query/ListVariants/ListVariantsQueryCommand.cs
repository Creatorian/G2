using Gnome.Application.Shared;
using Gnome.Domain.Common;
using Gnome.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.ListVariants
{
    [DataContract]
    public class ListVariantsQueryCommand : PagedQuery<SortedPagedList<VariantListResponse>>
    {
        [DataMember(Name = "dateFrom")]
        public DateTime DateFrom { get; set; }

        [DataMember(Name = "dateTo")]
        public DateTime DateTo { get; set; } = DateTime.Now;

        [DataMember(Name = "name")]
        public string Name { get; set; }

    }
}
