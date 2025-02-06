using System.Runtime.Serialization;

namespace Gnome.Domain.Common
{
    [DataContract]
    public class PagedList<T>
    {
        [DataMember(Name = "page")]
        public int Page { get; set; }
        [DataMember(Name = "page-size")]
        public int PageSize { get; set; }
        [DataMember(Name = "total-pages")]
        public int TotalPages { get; set; }
        [DataMember(Name = "total-count")]
        public int TotalCount { get; set; }
        [DataMember(Name = "items")]
        public List<T> Items { get; set; }
    }
}
