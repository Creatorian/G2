using System;

namespace Gnome.Domain.Exceptions
{
    /// <summary>
    /// Base exception for domain-specific errors
    /// </summary>
    public abstract class DomainException : Exception
    {
        public int HttpStatusCode { get; }

        protected DomainException(string message, int httpStatusCode = 400) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        protected DomainException(string message, Exception innerException, int httpStatusCode = 400) : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }
    }

    /// <summary>
    /// Exception thrown when a duplicate entity is found
    /// </summary>
    public class DuplicateEntityException : DomainException
    {
        public string EntityType { get; }
        public string FieldName { get; }
        public string FieldValue { get; }

        public DuplicateEntityException(string entityType, string fieldName, string fieldValue) 
            : base($"{entityType} with {fieldName} '{fieldValue}' already exists. Please choose a different {fieldName}.", 409)
        {
            EntityType = entityType;
            FieldName = fieldName;
            FieldValue = fieldValue;
        }
    }

    /// <summary>
    /// Exception thrown when an entity is not found
    /// </summary>
    public class EntityNotFoundException : DomainException
    {
        public string EntityType { get; }
        public object Id { get; }

        public EntityNotFoundException(string entityType, object id) 
            : base($"{entityType} with ID '{id}' not found.", 404)
        {
            EntityType = entityType;
            Id = id;
        }
    }

    /// <summary>
    /// Exception thrown when a foreign key constraint is violated
    /// </summary>
    public class ForeignKeyConstraintException : DomainException
    {
        public string ReferencedEntity { get; }
        public object ReferencedId { get; }

        public ForeignKeyConstraintException(string referencedEntity, object referencedId) 
            : base($"{referencedEntity} with ID '{referencedId}' does not exist.", 400)
        {
            ReferencedEntity = referencedEntity;
            ReferencedId = referencedId;
        }
    }

    /// <summary>
    /// Exception thrown when validation fails
    /// </summary>
    public class ValidationException : DomainException
    {
        public string FieldName { get; }
        public string ValidationMessage { get; }

        public ValidationException(string fieldName, string validationMessage) 
            : base($"Validation failed for {fieldName}: {validationMessage}", 400)
        {
            FieldName = fieldName;
            ValidationMessage = validationMessage;
        }
    }
} 