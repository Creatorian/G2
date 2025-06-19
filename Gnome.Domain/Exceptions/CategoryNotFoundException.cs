using System;

namespace Gnome.Domain.Exceptions
{
    public class CategoryNotFoundException : DomainException
    {
        public int CategoryId { get; }

        public CategoryNotFoundException(int categoryId) 
            : base($"Category with ID {categoryId} was not found.")
        {
            CategoryId = categoryId;
        }
    }
} 