using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Gnome.Application.Shared
{
    public class LinqModelBinder : IModelBinder
    {
        private readonly LinqModelBuilderConfiguration _configuration;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private readonly bool _usingNewtonsoftJson;

        public LinqModelBinder(LinqModelBuilderConfiguration linqModelBuilderConfiguration, IActionContextAccessor actionContextAccessor, IServiceProvider serviceProvider)
        {
            _actionContextAccessor = actionContextAccessor;
            _configuration = linqModelBuilderConfiguration;
            _usingNewtonsoftJson = serviceProvider.GetService<DynamicNamingStrategyOptions>() != null;
            if (_usingNewtonsoftJson)
            {
                IOptions<MvcNewtonsoftJsonOptions> service = serviceProvider.GetService<IOptions<MvcNewtonsoftJsonOptions>>();
                _jsonSerializerSettings = service.Value.SerializerSettings;
            }
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Type modelType = bindingContext.ModelType;
            object obj = null;
            List<KeyValuePair<Type, List<ModelPropertyConfiguration>>> list = _configuration.TypeConfiguration.Where((KeyValuePair<Type, List<ModelPropertyConfiguration>> x) => x.Key.Equals(modelType) || modelType.IsSubclassOf(x.Key)).ToList();
            foreach (KeyValuePair<Type, List<ModelPropertyConfiguration>> typeConfiguration in list)
            {
                string text = null;
                HttpContext httpContext = bindingContext.HttpContext;
                if (obj == null)
                {
                    if (typeConfiguration.Value.Any((ModelPropertyConfiguration x) => x.PropertyLocation == PropertyLocation.BODY))
                    {
                        using MemoryStream buffer = new MemoryStream();
                        await httpContext.Request.Body.CopyToAsync(buffer);
                        buffer.Position = 0L;
                        httpContext.Request.Body = buffer;
                        text = Encoding.UTF8.GetString(buffer.ToArray());
                        obj = ((!_usingNewtonsoftJson) ? System.Text.Json.JsonSerializer.Deserialize(text, modelType, FormatterUtilities.GetSerializerOptions(bindingContext.HttpContext)) : JsonConvert.DeserializeObject(text, modelType, _jsonSerializerSettings));
                    }
                    else
                    {
                        obj = Activator.CreateInstance(modelType);
                    }
                }

                PropertyInfo[] properties = modelType.GetProperties();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    string propertyName = propertyInfo.Name;
                    ModelPropertyConfiguration modelPropertyConfiguration = typeConfiguration.Value.Where((ModelPropertyConfiguration x) => x.PropertyName.Equals(propertyName)).FirstOrDefault();
                    if (modelPropertyConfiguration == null)
                    {
                        continue;
                    }

                    PropertyData propertyData = new PropertyData
                    {
                        PropertyName = propertyName,
                        ParameterName = modelPropertyConfiguration.ParameterName,
                        PropertyType = propertyInfo.PropertyType,
                        ActionContext = _actionContextAccessor.ActionContext,
                        DefaultValue = modelPropertyConfiguration.DefaultValue,
                        MaxIntegerValue = modelPropertyConfiguration.MaxIntegerValue,
                        MinIntegerValue = modelPropertyConfiguration.MinIntegerValue,
                        ValidValues = modelPropertyConfiguration.ValidValues,
                        HttpContext = httpContext
                    };
                    object obj2 = null;
                    switch (modelPropertyConfiguration.PropertyLocation)
                    {
                        case PropertyLocation.PATH:
                            obj2 = propertyData.GetRouteParmater();
                            break;
                        case PropertyLocation.QUERY:
                            obj2 = propertyData.GetQueryParam();
                            break;
                        case PropertyLocation.HEADER:
                            obj2 = propertyData.GetHeaderParameter();
                            break;
                        case PropertyLocation.FORM:
                            obj2 = propertyData.GetFormParameter();
                            break;
                        case PropertyLocation.FORM_FILE:
                            obj2 = propertyData.GetFormFileParameter();
                            break;
                        case PropertyLocation.FORM_FILE_NAME:
                            obj2 = propertyData.GetFormFileNameParameter();
                            break;
                        case PropertyLocation.TOKEN:
                            obj2 = propertyData.GetTokenClaim();
                            break;
                        case PropertyLocation.USER_SUBJECT:
                            obj2 = propertyData.GetUserSubject();
                            break;
                        case PropertyLocation.USER_SUBJECT_WITHOUT_IDP:
                            obj2 = propertyData.GetUserSubjectWithoutIdp();
                            break;
                        case PropertyLocation.IDENTITY_PROVIDER:
                            obj2 = propertyData.GetIdentityProvider();
                            break;
                        case PropertyLocation.PROVIDED_PARAMS:
                            obj2 = propertyData.GetProvidedParams(text);
                            break;
                    }

                    if (obj2 != null)
                    {
                        if (obj2 is string text2 && modelPropertyConfiguration.UrlDecode)
                        {
                            obj2 = ((!modelPropertyConfiguration.SlashDecode) ? HttpUtility.UrlDecode(text2, Encoding.UTF8) : text2.Replace("%2f", "/"));
                        }

                        propertyInfo.SetValue(obj, obj2);
                    }
                }
            }
            bindingContext.Result = ModelBindingResult.Success(obj);
        }
    }
}
