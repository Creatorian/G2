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

        /// <summary>
        /// Product complexity
        /// </summary>
        /// <example>Medium</example>
        public string Complexity { get; set; } = string.Empty;
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
        /// Short description of the product
        /// </summary>
        /// <example>Premium chess set for serious players</example>
        public string ShortDescription { get; set; } = string.Empty;

        /// <summary>
        /// Product price
        /// </summary>
        /// <example>29.99</example>
        public decimal Price { get; set; }

        /// <summary>
        /// Available stock quantity
        /// </summary>
        /// <example>50</example>
        public int Stock { get; set; }

        /// <summary>
        /// Product rating
        /// </summary>
        /// <example>4.5</example>
        public decimal Rating { get; set; }

        /// <summary>
        /// Associated categories
        /// </summary>
        public List<CategoryDto> Categories { get; set; } = new();
        
        /// <summary>
        /// Associated product images
        /// </summary>
        public List<ImageDto> Images { get; set; } = new();
        
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

        /// <summary>
        /// Product complexity
        /// </summary>
        /// <example>Medium</example>
        public string Complexity { get; set; } = string.Empty;
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
    /// DTO for product image information in product responses
    /// </summary>
    public class ImageDto
    {
        /// <summary>
        /// Image ID
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        
        /// <summary>
        /// Image URL
        /// </summary>
        /// <example>https://res.cloudinary.com/example/image/upload/v1/chess-product.jpg</example>
        public string Url { get; set; } = string.Empty;
        
        /// <summary>
        /// Whether this is the primary image
        /// </summary>
        /// <example>true</example>
        public bool IsPrimary { get; set; }
        
        /// <summary>
        /// Image creation date
        /// </summary>
        /// <example>2024-01-01T00:00:00Z</example>
        public DateTime CreatedDateTime { get; set; }
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
    /// Response DTO for product update
    /// </summary>
    public class ProductUpdatedResponse
    {
        /// <summary>
        /// The ID of the updated product
        /// </summary>
        /// <example>2</example>
        public int Id { get; set; }
    }

    /// <summary>
    /// Response DTO for product deletion
    /// </summary>
    public class ProductDeletedResponse
    {
        /// <summary>
        /// Whether the deletion was successful
        /// </summary>
        /// <example>true</example>
        public bool Success { get; set; }
    }
} 