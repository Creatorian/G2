using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Gnome.Application.Shared
{
    public class DynamicNamingStrategyOptions
    {
        public Dictionary<CaseType, NamingStrategy> NamingStrategies { get; set; }

        public CaseType DefaultCase { get; set; }
        public Func<IHttpContextAccessor> HttpContextAccessorProvider { get; set; } 
    }
}