using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Shared
{
    public class CommandOrQueryBase<T> : IRequest<T>
    {
        [DataMember(Name = "Content-Type", EmitDefaultValue = false)]
        [FromHeader]
        public string ContentType { get; set; }

        [DataMember(Name = "Date", EmitDefaultValue = false)]
        [FromHeader]
        public string Date { get; set; }
    }
}
