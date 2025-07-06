using System;
using System.Collections.Generic;

namespace Gnome.Api.Models.SwaggerResponses
{
    /// <summary>
    /// Response DTO for variant list
    /// </summary>
    public class VariantListResponse
    {
        /// <summary>
        /// List of variants
        /// </summary>
        public List<VariantResponse> Items { get; set; } = new();
        
        /// <summary>
        /// Total number of variants
        /// </summary>
        /// <example>3</example>
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
    /// Response DTO for variant details
    /// </summary>
    public class VariantResponse
    {
        /// <summary>
        /// Variant ID
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        
        /// <summary>
        /// Variant name
        /// </summary>
        /// <example>Standard Edition</example>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// URL-friendly variant name
        /// </summary>
        /// <example>standard-edition</example>
        public string Slug { get; set; } = string.Empty;
        
        /// <summary>
        /// Variant price
        /// </summary>
        /// <example>29.99</example>
        public decimal Price { get; set; }
        
        /// <summary>
        /// Available stock quantity
        /// </summary>
        /// <example>50</example>
        public int Stock { get; set; }
        
        /// <summary>
        /// Whether this is the primary variant
        /// </summary>
        /// <example>true</example>
        public bool IsPrimary { get; set; }
        
        /// <summary>
        /// Variant image URL
        /// </summary>
        /// <example>https://res.cloudinary.com/example/image/upload/v1/chess-standard.jpg</example>
        public string? ImageUrl { get; set; }
        
        /// <summary>
        /// Associated product information
        /// </summary>
        public ProductDto Product { get; set; } = new();
        
        /// <summary>
        /// Variant creation date
        /// </summary>
        /// <example>2024-01-01T00:00:00Z</example>
        public DateTime CreatedDateTime { get; set; }
        
        /// <summary>
        /// Variant last update date
        /// </summary>
        /// <example>2024-01-15T10:00:00Z</example>
        public DateTime UpdatedDateTime { get; set; }
    }

    /// <summary>
    /// DTO for product information in variant responses
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Product ID
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        
        /// <summary>
        /// Product name
        /// </summary>
        /// <example>Chess Master Pro</example>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// URL-friendly product name
        /// </summary>
        /// <example>chess-master-pro</example>
        public string Slug { get; set; } = string.Empty;
    }

    /// <summary>
    /// Response DTO for variant creation
    /// </summary>
    public class VariantCreatedResponse
    {
        /// <summary>
        /// The ID of the newly created variant
        /// </summary>
        /// <example>2</example>
        public int Id { get; set; }
    }

    /// <summary>
    /// Response DTO for variant updates
    /// </summary>
    public class VariantUpdatedResponse
    {
        /// <summary>
        /// The ID of the updated variant
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }

    /// <summary>
    /// Response DTO for variant deletion
    /// </summary>
    public class VariantDeletedResponse
    {
        /// <summary>
        /// Confirmation that the variant was deleted successfully
        /// </summary>
        /// <example>true</example>
        public bool Success { get; set; }
    }
} 