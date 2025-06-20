﻿using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.AddVariant
{
    [DataContract]
    public class AddVariantCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsPrimary { get; set; }
        public int ProductId { get; set; }
    }
}
