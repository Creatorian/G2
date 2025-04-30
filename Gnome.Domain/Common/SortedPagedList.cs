using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Domain.Common
{
    [DataContract]
    public class SortedPagedList<T> : PagedList<T>
    {
        [DataMember(Name = "sort-order")]
        public string SortOrder { get; set; }
        [DataMember(Name = "sort-by")]
        public string SortBy { get; set; }
    }
}
