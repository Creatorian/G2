using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.DeleteVariant
{
    [DataContract]
    public class DeleteVariantCommand : IRequest<bool>
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
} 