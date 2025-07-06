using System;
using System.Collections.Generic;

namespace Gnome.Api.Models.SwaggerResponses
{
    /// <summary>
    /// Response DTO for product list
    /// </summary>
    public class ProductListResponse
    {
        /// <summary>
        /// List of products
        /// </summary>
        public List<ProductResponse> Items { get; set; } = new();
        
        /// <summary>
        /// Total number of products
        /// </summary>
        /// <example>1</example>
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
    /// Response DTO for product details
    /// </summary>
    public class ProductResponse
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
        
        /// <summary>
        /// Product description
        /// </summary>
        /// <example>Professional chess set with premium pieces</example>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Associated categories
        /// </summary>
        public List<CategoryDto> Categories { get; set; } = new();
        
        /// <summary>
        /// Associated variants
        /// </summary>
        public List<VariantDto> Variants { get; set; } = new();
        
        /// <summary>
        /// Product creation date
        /// </summary>
        /// <example>2024-01-01T00:00:00Z</example>
        public DateTime CreatedDateTime { get; set; }
        
        /// <summary>
        /// Product last update date
        /// </summary>
        /// <example>2024-01-15T10:00:00Z</example>
        public DateTime UpdatedDateTime { get; set; }
    }

    /// <summary>
    /// DTO for category information in product responses
    /// </summary>
    public class CategoryDto
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
    }

    /// <summary>
    /// DTO for variant information in product responses
    /// </summary>
    public class VariantDto
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
    }

    /// <summary>
    /// Response DTO for product creation
    /// </summary>
    public class ProductCreatedResponse
    {
        /// <summary>
        /// The ID of the newly created product
        /// </summary>
        /// <example>2</example>
        public int Id { get; set; }
    }

    /// <summary>
    /// Response DTO for product updates
    /// </summary>
    public class ProductUpdatedResponse
    {
        /// <summary>
        /// The ID of the updated product
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }

    /// <summary>
    /// Response DTO for product deletion
    /// </summary>
    public class ProductDeletedResponse
    {
        /// <summary>
        /// Confirmation that the product was deleted successfully
        /// </summary>
        /// <example>true</example>
        public bool Success { get; set; }
    }
} 