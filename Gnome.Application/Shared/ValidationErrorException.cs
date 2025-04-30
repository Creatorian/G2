using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gnome.Application.Shared
{
    [Obsolete("Please use ValidationErrorsException instead. It supports constructor passing only single validation error. Will be removed in next major update (.NET 8).")]
    public class ValidationErrorException : Exception, IExceptionHttpResponse
    {
        //
        // Summary:
        //     ValidationError instance
        public ValidationError ValidationError { get; set; }

        public int HttpStatusCode => 400;

        public bool HasBody => true;

        //
        // Summary:
        //     Constructor taking validation error as paramter
        //
        // Parameters:
        //   error:
        public ValidationErrorException(ValidationError error)
        {
            ValidationError = error;
        }

        public string GetHttpJsonResponse()
        {
            return CaseUtilities.ConvertFromObjectToJson(new ValidationErrors
            {
                Errors = new List<ValidationError> { ValidationError }
            });
        }
    }
}