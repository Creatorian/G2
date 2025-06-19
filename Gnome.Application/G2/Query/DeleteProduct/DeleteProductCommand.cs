using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.DeleteProduct
{
    [DataContract]
    public class DeleteProductCommand : IRequest<bool>
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
} 