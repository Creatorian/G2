using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace Gnome.Application.Shared
{

    public static class PropertyDataExtensions
    {
        public static string GetCasedParam(this PropertyData propertyData)
        {
            if (propertyData.ParameterName != null)
            {
                return propertyData.ParameterName;
            }

            return propertyData.PropertyName.ToKebabCase();
        }

        public static object ChangeType(this PropertyData propertyData, Type type, object value)
        {
            if (type.IsEnum)
            {
                if (value != null)
                {
                    return EnumerationUtilities.ToEnum(value.ToString(), type);
                }

                return null;
            }

            try
            {
                object obj = ((value == null) ? null : Convert.ChangeType(value, type));
                if (propertyData.MaxIntegerValue.HasValue && (int)obj > propertyData.MaxIntegerValue)
                {
                    obj = propertyData.MaxIntegerValue;
                }
                else if (propertyData.MinIntegerValue.HasValue && (int)obj < propertyData.MinIntegerValue)
                {
                    obj = propertyData.MinIntegerValue;
                }

                if (propertyData.ValidValues != null && propertyData.ValidValues.Count > 0 && !propertyData.ValidValues.Contains(obj))
                {
                    throw new ValidationErrorException(new ValidationError(propertyData.GetCasedParam(), ValidationErrorCode.NotOnList, "Value supplied for \"" + propertyData.GetCasedParam() + "\" is not on list"));
                }

                return obj;
            }
            catch (Exception ex)
            {
                if (ex is FormatException || ex is OverflowException)
                {
                    throw new ValidationErrorException(new ValidationError(propertyData.GetCasedParam(), ValidationErrorCode.InvalidFormat, "Value supplied for \"" + propertyData.GetCasedParam() + "\" does not have expected format"));
                }

                throw;
            }
        }

        public static object GetQueryParam(this PropertyData propertyData)
        {
            StringValues stringValues = propertyData.HttpContext.Request.Query[propertyData.GetCasedParam()];
            Type type = Nullable.GetUnderlyingType(propertyData.PropertyType) ?? propertyData.PropertyType;
            object obj = null;
            if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
            {
                if (stringValues.Count == 0 || (stringValues.Count == 1 && string.IsNullOrEmpty(stringValues[0])))
                {
                    return propertyData.DefaultValue;
                }

                obj = ((stringValues.Count != 1) ? stringValues.AsEnumerable() : stringValues[0].Split(',').AsEnumerable());
                object obj2 = Activator.CreateInstance(type);
                {
                    foreach (string item in obj as IEnumerable<string>)
                    {
                        type.GetMethod("Add").Invoke(obj2, new object[1] { propertyData.ChangeType(type.GenericTypeArguments[0], item.Trim()) });
                    }

                    return obj2;
                }
            }

            obj = ((stringValues.Count > 0 && !string.IsNullOrEmpty(stringValues[0])) ? stringValues[0] : propertyData.DefaultValue);
            return propertyData.ChangeType(type, obj);
        }

        public static object GetRouteParmater(this PropertyData propertyData)
        {
            object value = propertyData.ActionContext?.RouteData?.Values[propertyData.GetCasedParam()] ?? propertyData.DefaultValue;
            Type type = Nullable.GetUnderlyingType(propertyData.PropertyType) ?? propertyData.PropertyType;
            return propertyData.ChangeType(type, value);
        }

        public static object GetTokenClaim(this PropertyData propertyData)
        {
            HttpContext httpContext = propertyData.HttpContext;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                return httpContext.User.Claims.Where((Claim x) => x.Type == propertyData.GetCasedParam()).FirstOrDefault()?.Value;
            }

            return null;
        }

        public static object GetUserSubject(this PropertyData propertyData)
        {
            propertyData.ParameterName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            return propertyData.GetTokenClaim()?.ToString();
        }

        public static object GetUserSubjectWithoutIdp(this PropertyData propertyData)
        {
            propertyData.ParameterName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            return propertyData.GetTokenClaim()?.ToString().Split('@').FirstOrDefault()
                .ToString();
        }

        public static object GetIdentityProvider(this PropertyData propertyData)
        {
            propertyData.ParameterName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            string text = propertyData.GetTokenClaim()?.ToString();
            if (!string.IsNullOrEmpty(text) && text.Contains('@') && !text.EndsWith('@'))
            {
                return text.Split('@').LastOrDefault().ToString();
            }

            propertyData.ParameterName = "http://schemas.microsoft.com/identity/claims/identityprovider";
            return propertyData.GetTokenClaim()?.ToString();
        }

        public static object GetProvidedParams(this PropertyData propertyData, string bodyData)
        {
            if (bodyData == null)
            {
                return null;
            }

            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            foreach (string item in (IEnumerable<string>)(from p in JObject.Parse(bodyData).Properties()
                                                          select p.Name).ToList())
            {
                string key = item.ToPascalCase();
                dictionary.Add(key, value: true);
            }

            return dictionary;
        }

        public static object GetHeaderParameter(this PropertyData propertyData)
        {
            StringValues? stringValues = propertyData.HttpContext.Request?.Headers[propertyData.GetCasedParam()];
            object value = ((stringValues.HasValue && stringValues.Value.Count > 0) ? stringValues.Value[0] : propertyData.DefaultValue);
            Type type = Nullable.GetUnderlyingType(propertyData.PropertyType) ?? propertyData.PropertyType;
            return propertyData.ChangeType(type, value);
        }

        public static object GetFormFileNameParameter(this PropertyData propertyData)
        {
            if (!propertyData.HttpContext.Request.HasFormContentType)
            {
                return null;
            }

            string casedParam = propertyData.GetCasedParam();
            return (propertyData.HttpContext.Request?.Form.Files)[casedParam]?.FileName;
        }

        public static object GetFormFileParameter(this PropertyData propertyData)
        {
            if (!propertyData.HttpContext.Request.HasFormContentType)
            {
                return null;
            }

            string casedParam = propertyData.GetCasedParam();
            Type type = Nullable.GetUnderlyingType(propertyData.PropertyType) ?? propertyData.PropertyType;

            // Handle collections of IFormFile
            if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
            {
                var files = propertyData.HttpContext.Request?.Form.Files.Where(f => f.Name == casedParam).ToList();
                if (files != null && files.Any())
                {
                    object collection = Activator.CreateInstance(type);
                    foreach (var file in files)
                    {
                        if (type.GenericTypeArguments[0].Equals(typeof(IFormFile)) || type.GenericTypeArguments[0].IsAssignableFrom(typeof(IFormFile)))
                        {
                            type.GetMethod("Add").Invoke(collection, new object[1] { file });
                        }
                    }
                    return collection;
                }
                return propertyData.DefaultValue;
            }

            // Handle single IFormFile
            IFormFile formFile = (propertyData.HttpContext.Request?.Form.Files)[casedParam];
            if (formFile != null)
            {
                if (propertyData.PropertyType.Equals(typeof(byte[])))
                {
                    using MemoryStream memoryStream = new MemoryStream();
                    formFile.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }

                if (propertyData.PropertyType.Equals(typeof(Stream)))
                {
                    return formFile.OpenReadStream();
                }

                if (propertyData.PropertyType.Equals(typeof(IFormFile)) || propertyData.PropertyType.IsAssignableFrom(typeof(IFormFile)))
                {
                    return formFile;
                }
            }

            return null;
        }

        public static object GetFormParameter(this PropertyData propertyData)
        {
            if (!propertyData.HttpContext.Request.HasFormContentType)
            {
                return null;
            }

            StringValues? stringValues = propertyData.HttpContext.Request?.Form[propertyData.GetCasedParam()];
            object obj = ((stringValues.HasValue && stringValues.Value.Count > 0) ? stringValues.Value[0] : propertyData.DefaultValue);
            if (obj == null)
            {
                return null;
            }

            Type type = Nullable.GetUnderlyingType(propertyData.PropertyType) ?? propertyData.PropertyType;
            
            if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
            {
                if (stringValues.Value.Count == 0 || (stringValues.Value.Count == 1 && string.IsNullOrEmpty(stringValues.Value[0])))
                {
                    return propertyData.DefaultValue;
                }

                var values = ((stringValues.Value.Count != 1) ? stringValues.Value.AsEnumerable() : stringValues.Value[0].Split(',').AsEnumerable());
                object collection = Activator.CreateInstance(type);
                {
                    foreach (string item in values)
                    {
                        type.GetMethod("Add").Invoke(collection, new object[1] { propertyData.ChangeType(type.GenericTypeArguments[0], item.Trim()) });
                    }

                    return collection;
                }
            }

            return propertyData.ChangeType(type, obj);
        }
    }
}
