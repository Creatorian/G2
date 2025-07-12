using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gnome.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductListResponse>> GetProducts(int page, int pageSize, ProductFilter filter, string sortBy = default, string sortOrder = "desc")
        {
            var products = _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Images)
                .AsQueryable()
                .AsNoTracking();

            #region Filters

            // Date range filter
            if (filter.DateFrom.HasValue)
            {
                products = products.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= filter.DateFrom.Value);
            }
            if (filter.DateTo.HasValue)
            {
                products = products.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value <= filter.DateTo.Value);
            }

            // Text-based filters (case-insensitive contains search)
            if (!string.IsNullOrEmpty(filter.Name))
            {
                products = products.Where(x => EF.Functions.Like(x.Name, $"%{filter.Name}%"));
            }

            if (!string.IsNullOrEmpty(filter.Slug))
            {
                products = products.Where(x => EF.Functions.Like(x.Slug, $"%{filter.Slug}%"));
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                products = products.Where(x => x.Description != null && EF.Functions.Like(x.Description, $"%{filter.Description}%"));
            }

            if (!string.IsNullOrEmpty(filter.ShortDescription))
            {
                products = products.Where(x => x.ShortDescription != null && EF.Functions.Like(x.ShortDescription, $"%{filter.ShortDescription}%"));
            }

            if (!string.IsNullOrEmpty(filter.NumberOfPlayers))
            {
                products = products.Where(x => x.NumberOfPlayers != null && EF.Functions.Like(x.NumberOfPlayers, $"%{filter.NumberOfPlayers}%"));
            }

            if (!string.IsNullOrEmpty(filter.PlayingTime))
            {
                products = products.Where(x => x.PlayingTime != null && EF.Functions.Like(x.PlayingTime, $"%{filter.PlayingTime}%"));
            }

            if (!string.IsNullOrEmpty(filter.CommunityAge))
            {
                products = products.Where(x => x.CommunityAge != null && EF.Functions.Like(x.CommunityAge, $"%{filter.CommunityAge}%"));
            }

            if (!string.IsNullOrEmpty(filter.Complexity))
            {
                products = products.Where(x => x.Complexity != null && EF.Functions.Like(x.Complexity, $"%{filter.Complexity}%"));
            }

            // Numeric range filters
            if (filter.MinRating.HasValue)
            {
                products = products.Where(x => x.Rating >= filter.MinRating.Value);
            }

            if (filter.MaxRating.HasValue)
            {
                products = products.Where(x => x.Rating <= filter.MaxRating.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                products = products.Where(x => x.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                products = products.Where(x => x.Price <= filter.MaxPrice.Value);
            }

            if (filter.MinStock.HasValue)
            {
                products = products.Where(x => x.Stock >= filter.MinStock.Value);
            }

            if (filter.MaxStock.HasValue)
            {
                products = products.Where(x => x.Stock <= filter.MaxStock.Value);
            }

            // Awards filter
            if (filter.Awards != null && filter.Awards.Any())
            {
                products = products.Where(x => !string.IsNullOrEmpty(x.Awards) && 
                    filter.Awards.Any(award => EF.Functions.Like(x.Awards, $"%{award}%")));
            }

            // Category filters
            if (filter.CategoryIds != null && filter.CategoryIds.Any())
            {
                products = products.Where(x => x.ProductCategories.Any(pc => filter.CategoryIds.Contains(pc.CategoryId)));
            }

            if (filter.CategoryNames != null && filter.CategoryNames.Any())
            {
                products = products.Where(x => x.ProductCategories.Any(pc => 
                    filter.CategoryNames.Any(name => EF.Functions.Like(pc.Category.Name, $"%{name}%"))));
            }

            // Boolean filters
            if (filter.HasImages.HasValue)
            {
                if (filter.HasImages.Value)
                {
                    products = products.Where(x => x.Images.Any());
                }
                else
                {
                    products = products.Where(x => !x.Images.Any());
                }
            }

            if (filter.InStockOnly.HasValue && filter.InStockOnly.Value)
            {
                products = products.Where(x => x.Stock > 0);
            }

            #endregion

            #region Sort

            if (!string.IsNullOrEmpty(sortBy))
            {
                var isDescending = sortOrder == "desc";

                switch(sortBy)
                {
                    case "created-date-time":
                        products = isDescending ? products.OrderByDescending(c => c.CreatedDateTime) : products.OrderBy(c => c.CreatedDateTime);
                        break;
                    case "name":
                        products = isDescending ? products.OrderByDescending(c => c.Name) : products.OrderBy(c => c.Name);
                        break;
                    case "price":
                        products = isDescending ? products.OrderByDescending(c => c.Price) : products.OrderBy(c => c.Price);
                        break;
                    case "rating":
                        products = isDescending ? products.OrderByDescending(c => c.Rating) : products.OrderBy(c => c.Rating);
                        break;
                    case "stock":
                        products = isDescending ? products.OrderByDescending(c => c.Stock) : products.OrderBy(c => c.Stock);
                        break;
                    case "complexity":
                        products = isDescending ? products.OrderByDescending(c => c.Complexity) : products.OrderBy(c => c.Complexity);
                        break;
                    case "playing-time":
                        products = isDescending ? products.OrderByDescending(c => c.PlayingTime) : products.OrderBy(c => c.PlayingTime);
                        break;
                }
            }

            #endregion

            products = products.Skip(pageSize * (page - 1)).Take(pageSize);
            
            // Execute the query first to get the data
            var productEntities = await products.ToListAsync();
            
            return productEntities.Select(productEntity => new ProductListResponse
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Slug = productEntity.Slug,
                Description = productEntity.Description,
                ShortDescription = productEntity.ShortDescription,
                NumberOfPlayers = productEntity.NumberOfPlayers,
                PlayingTime = productEntity.PlayingTime,
                CommunityAge = productEntity.CommunityAge,
                Complexity = productEntity.Complexity,
                Rating = productEntity.Rating,
                Price = productEntity.Price,
                Stock = productEntity.Stock,
                Awards = !string.IsNullOrEmpty(productEntity.Awards) ? productEntity.Awards.Split(',').ToList() : new List<string>(),
                CreatedDateTime = productEntity.CreatedDateTime,
                Categories = productEntity.ProductCategories.Select(pc => new CategoryListResponse
                {
                    Id = pc.Category.Id,
                    Name = pc.Category.Name,
                    Slug = pc.Category.Slug,
                    CreatedDateTime = pc.Category.CreatedDateTime,
                    ProductsCount = pc.Category.ProductCategories.Count
                }).ToList(),
                Images = productEntity.Images.Select(img => new ImageResponse
                {
                    Id = img.Id,
                    Url = img.Url,
                    IsPrimary = img.IsPrimary,
                    CreatedDateTime = img.CreatedDateTime
                }).ToList(),
                ImageCount = productEntity.Images.Count
            }).ToList();
        }

        public async Task<int> CountProducts(ProductFilter filter)
        {
            var products = _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.Images)
                .AsQueryable()
                .AsNoTracking();

            #region Filters

            // Date range filter
            if (filter.DateFrom.HasValue)
            {
                products = products.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= filter.DateFrom.Value);
            }
            if (filter.DateTo.HasValue)
            {
                products = products.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value <= filter.DateTo.Value);
            }

            // Text-based filters (case-insensitive contains search)
            if (!string.IsNullOrEmpty(filter.Name))
            {
                products = products.Where(x => EF.Functions.Like(x.Name, $"%{filter.Name}%"));
            }

            if (!string.IsNullOrEmpty(filter.Slug))
            {
                products = products.Where(x => EF.Functions.Like(x.Slug, $"%{filter.Slug}%"));
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                products = products.Where(x => x.Description != null && EF.Functions.Like(x.Description, $"%{filter.Description}%"));
            }

            if (!string.IsNullOrEmpty(filter.ShortDescription))
            {
                products = products.Where(x => x.ShortDescription != null && EF.Functions.Like(x.ShortDescription, $"%{filter.ShortDescription}%"));
            }

            if (!string.IsNullOrEmpty(filter.NumberOfPlayers))
            {
                products = products.Where(x => x.NumberOfPlayers != null && EF.Functions.Like(x.NumberOfPlayers, $"%{filter.NumberOfPlayers}%"));
            }

            if (!string.IsNullOrEmpty(filter.PlayingTime))
            {
                products = products.Where(x => x.PlayingTime != null && EF.Functions.Like(x.PlayingTime, $"%{filter.PlayingTime}%"));
            }

            if (!string.IsNullOrEmpty(filter.CommunityAge))
            {
                products = products.Where(x => x.CommunityAge != null && EF.Functions.Like(x.CommunityAge, $"%{filter.CommunityAge}%"));
            }

            if (!string.IsNullOrEmpty(filter.Complexity))
            {
                products = products.Where(x => x.Complexity != null && EF.Functions.Like(x.Complexity, $"%{filter.Complexity}%"));
            }

            // Numeric range filters
            if (filter.MinRating.HasValue)
            {
                products = products.Where(x => x.Rating >= filter.MinRating.Value);
            }

            if (filter.MaxRating.HasValue)
            {
                products = products.Where(x => x.Rating <= filter.MaxRating.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                products = products.Where(x => x.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                products = products.Where(x => x.Price <= filter.MaxPrice.Value);
            }

            if (filter.MinStock.HasValue)
            {
                products = products.Where(x => x.Stock >= filter.MinStock.Value);
            }

            if (filter.MaxStock.HasValue)
            {
                products = products.Where(x => x.Stock <= filter.MaxStock.Value);
            }

            // Awards filter
            if (filter.Awards != null && filter.Awards.Any())
            {
                products = products.Where(x => !string.IsNullOrEmpty(x.Awards) && 
                    filter.Awards.Any(award => EF.Functions.Like(x.Awards, $"%{award}%")));
            }

            // Category filters
            if (filter.CategoryIds != null && filter.CategoryIds.Any())
            {
                products = products.Where(x => x.ProductCategories.Any(pc => filter.CategoryIds.Contains(pc.CategoryId)));
            }

            if (filter.CategoryNames != null && filter.CategoryNames.Any())
            {
                products = products.Where(x => x.ProductCategories.Any(pc => 
                    filter.CategoryNames.Any(name => EF.Functions.Like(pc.Category.Name, $"%{name}%"))));
            }

            // Boolean filters
            if (filter.HasImages.HasValue)
            {
                if (filter.HasImages.Value)
                {
                    products = products.Where(x => x.Images.Any());
                }
                else
                {
                    products = products.Where(x => !x.Images.Any());
                }
            }

            if (filter.InStockOnly.HasValue && filter.InStockOnly.Value)
            {
                products = products.Where(x => x.Stock > 0);
            }

            #endregion

            return await products.CountAsync();
        }

        public async Task<int> AddProductAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) // Unique constraint violation
            {
                throw new InvalidOperationException("A product with this slug already exists.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error adding product: {ex.Message}");
            }
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            try
            {
                var existingProduct = await _context.Products
                    .Include(p => p.ProductCategories)
                    .FirstOrDefaultAsync(p => p.Id == product.Id);
                
                if (existingProduct == null)
                    throw new InvalidOperationException("Product not found.");

                existingProduct.Name = product.Name;
                existingProduct.Slug = product.Slug;
                existingProduct.Description = product.Description;
                existingProduct.ShortDescription = product.ShortDescription;
                existingProduct.NumberOfPlayers = product.NumberOfPlayers;
                existingProduct.PlayingTime = product.PlayingTime;
                existingProduct.CommunityAge = product.CommunityAge;
                existingProduct.Complexity = product.Complexity;
                existingProduct.Price = product.Price;
                existingProduct.Awards = product.Awards;
                existingProduct.Stock = product.Stock;
                existingProduct.Rating = product.Rating;

                // Handle many-to-many relationship
                if (product.ProductCategories != null)
                {
                    // Remove existing category associations
                    _context.ProductCategories.RemoveRange(existingProduct.ProductCategories);
                    
                    // Add new category associations
                    foreach (var productCategory in product.ProductCategories)
                    {
                        existingProduct.ProductCategories.Add(productCategory);
                    }
                }

                await _context.SaveChangesAsync();
                return product.Id;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) // Unique constraint violation
            {
                throw new InvalidOperationException("A product with this slug already exists.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error updating product: {ex.Message}");
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetProductBySlugAsync(string slug)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting product: {ex.Message}");
            }
        }
    }
}
