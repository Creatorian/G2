using System;
using System.Runtime.Serialization;

namespace Gnome.Domain.Responses
{
    /// <summary>
    /// Response with a single product image for the FE
    /// </summary>
    [DataContract]
    public class ImageResponse
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "url")]
        public string Url { get; set; }
        
        [DataMember(Name = "is-primary")]
        public bool IsPrimary { get; set; }
        
        [DataMember(Name = "created-date-time")]
        public DateTime CreatedDateTime { get; set; }
    }
} 