using System.Collections.Generic;

namespace Gnome.Application.Shared
{
    public class ValidationErrors
    {
        public List<ValidationError> Errors { get; set; }

        public ValidationErrors() 
        {
            Errors = new List<ValidationError>();
        }

        public ValidationErrors(ValidationError error)
        {
            Errors = new List<ValidationError> { error };
        }

        public ValidationErrors(List<ValidationError> errors)
        {
            Errors = errors;
        }
    }
}