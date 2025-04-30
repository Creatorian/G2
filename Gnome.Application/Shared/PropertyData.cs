using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Gnome.Application.Shared
{
    public class PropertyData
    {
        public string PropertyName { get; set; }

        public string ParameterName { get; set; }

        public Type PropertyType { get; set; }
        public object DefaultValue { get; set; }
        public int? MaxIntegerValue { get; set; }
        public int? MinIntegerValue { get; set;}
        public HttpContext HttpContext { get; set; }
        public ActionContext ActionContext { get; set; }
        public List<object> ValidValues { get; set; }
    }
}