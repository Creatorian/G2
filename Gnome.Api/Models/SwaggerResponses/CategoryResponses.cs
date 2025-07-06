using System;
using System.Collections.Generic;

namespace Gnome.Api.Models.SwaggerResponses
{
    /// <summary>
    /// Response DTO for category list
    /// </summary>
    public class CategoryListResponse
    {
        /// <summary>
        /// List of categories
        /// </summary>
        public List<CategoryResponse> Items { get; set; } = new();
        
        /// <summary>
        /// Total number of categories
        /// </summary>
        /// <example>5</example>
        public int TotalCount { get; set; }
        
        /// <summary>
        /// Current page number
        /// </summary>
        /// <example>1</example>
        public int PageNumber { get; set; }
        
        /// <summary>
        /// Number of items per page
        /// </summary>
        /// <example>10</example>
        public int PageSize { get; set; }
        
        /// <summary>
        /// Total number of pages
        /// </summary>
        /// <example>1</example>
        public int TotalPages { get; set; }
        
        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        /// <example>false</example>
        public bool HasPreviousPage { get; set; }
        
        /// <summary>
        /// Whether there is a next page
        /// </summary>
        /// <example>false</example>
        public bool HasNextPage { get; set; }
    }

    /// <summary>
    /// Response DTO for category details
    /// </summary>
    public class CategoryResponse
    {
        /// <summary>
        /// Category ID
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        
        /// <summary>
        /// Category name
        /// </summary>
        /// <example>Strategy Games</example>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// URL-friendly category name
        /// </summary>
        /// <example>strategy-games</example>
        public string Slug { get; set; } = string.Empty;
        
        /// <summary>
        /// Number of products in this category
        /// </summary>
        /// <example>5</example>
        public int ProductsCount { get; set; }
        
        /// <summary>
        /// Category creation date
        /// </summary>
        /// <example>2024-01-01T00:00:00Z</example>
        public DateTime CreatedDateTime { get; set; }
        
        /// <summary>
        /// Category last update date
        /// </summary>
        /// <example>2024-01-15T10:00:00Z</example>
        public DateTime UpdatedDateTime { get; set; }
    }

    /// <summary>
    /// Response DTO for category creation
    /// </summary>
    public class CategoryCreatedResponse
    {
        /// <summary>
        /// The ID of the newly created category
        /// </summary>
        /// <example>2</example>
        public int Id { get; set; }
    }

    /// <summary>
    /// Response DTO for category updates
    /// </summary>
    public class CategoryUpdatedResponse
    {
        /// <summary>
        /// The ID of the updated category
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }

    /// <summary>
    /// Response DTO for category deletion
    /// </summary>
    public class CategoryDeletedResponse
    {
        /// <summary>
        /// Confirmation that the category was deleted successfully
        /// </summary>
        /// <example>true</example>
        public bool Success { get; set; }
    }
} 