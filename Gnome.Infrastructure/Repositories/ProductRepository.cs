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

            // Apply basic filters
            if (!string.IsNullOrEmpty(filter.Name))
            {
                products = products.Where(x => EF.Functions.Like(x.Name, $"%{filter.Name}%"));
            }

            if (!string.IsNullOrEmpty(filter.Slug))
            {
                products = products.Where(x => EF.Functions.Like(x.Slug, $"%{filter.Slug}%"));
            }

            if (filter.DateFrom.HasValue)
            {
                products = products.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= filter.DateFrom.Value);
            }

            if (filter.DateTo.HasValue)
            {
                products = products.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value <= filter.DateTo.Value);
            }

            if (filter.Rating.HasValue)
            {
                var databaseRating = filter.Rating.Value * 2;
                products = products.Where(x => x.Rating >= databaseRating);
            }

            if (filter.MinPrice.HasValue)
            {
                products = products.Where(x => x.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                products = products.Where(x => x.Price <= filter.MaxPrice.Value);
            }

            // Category filters
            if (filter.CategoryIds != null && filter.CategoryIds.Any())
            {
                products = products.Where(x => x.ProductCategories.Any(pc => filter.CategoryIds.Contains(pc.CategoryId)));
            }

            if (filter.CategorySlugs != null && filter.CategorySlugs.Any())
            {
                products = products.Where(x => 
                    filter.CategorySlugs.All(requiredSlug => 
                        x.ProductCategories.Any(pc => pc.Category.Slug == requiredSlug)));
            }

            if (filter.InStockOnly.HasValue && filter.InStockOnly.Value)
            {
                products = products.Where(x => x.Stock > 0);
            }

            var productList = await products.ToListAsync();

            if (!string.IsNullOrEmpty(filter.Complexity))
            {
                if (int.TryParse(filter.Complexity, out int complexityValue))
                {
                    productList = productList.Where(x => x.Complexity != null && 
                        decimal.TryParse(x.Complexity, out decimal dbComplexity) && dbComplexity >= complexityValue).ToList();
                }
            }

            if (!string.IsNullOrEmpty(filter.MinPlayers))
            {
                var minPlayersValue = int.Parse(filter.MinPlayers);
                productList = productList.Where(x => x.NumberOfPlayers != null && 
                    (x.NumberOfPlayers.Contains("-") 
                        ? int.Parse(x.NumberOfPlayers.Substring(0, x.NumberOfPlayers.IndexOf("-"))) <= minPlayersValue
                        : int.Parse(x.NumberOfPlayers) <= minPlayersValue)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.MaxPlayers))
            {
                var maxPlayersValue = int.Parse(filter.MaxPlayers);
                productList = productList.Where(x => x.NumberOfPlayers != null && 
                    (x.NumberOfPlayers.Contains("-") 
                        ? int.Parse(x.NumberOfPlayers.Substring(x.NumberOfPlayers.IndexOf("-") + 1)) >= maxPlayersValue
                        : int.Parse(x.NumberOfPlayers) >= maxPlayersValue)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.MinPlayingTime))
            {
                var minPlayingTimeValue = int.Parse(filter.MinPlayingTime);
                productList = productList.Where(x => x.PlayingTime != null && 
                    (x.PlayingTime.Contains("-") 
                        ? int.Parse(x.PlayingTime.Substring(0, x.PlayingTime.IndexOf("-"))) <= minPlayingTimeValue
                        : int.Parse(x.PlayingTime) <= minPlayingTimeValue)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.MaxPlayingTime))
            {
                var maxPlayingTimeValue = int.Parse(filter.MaxPlayingTime);
                productList = productList.Where(x => x.PlayingTime != null && 
                    (x.PlayingTime.Contains("-") 
                        ? int.Parse(x.PlayingTime.Substring(x.PlayingTime.IndexOf("-") + 1)) >= maxPlayingTimeValue
                        : int.Parse(x.PlayingTime) >= maxPlayingTimeValue)).ToList();
            }

            #region Sort

            if (!string.IsNullOrEmpty(sortBy))
            {
                var isDescending = sortOrder == "desc";

                switch(sortBy)
                {
                    case "created-date-time":
                        productList = isDescending ? productList.OrderByDescending(c => c.CreatedDateTime).ToList() : productList.OrderBy(c => c.CreatedDateTime).ToList();
                        break;
                    case "name":
                        productList = isDescending ? productList.OrderByDescending(c => c.Name).ToList() : productList.OrderBy(c => c.Name).ToList();
                        break;
                    case "price":
                        productList = isDescending ? productList.OrderByDescending(c => c.Price).ToList() : productList.OrderBy(c => c.Price).ToList();
                        break;
                    case "rating":
                        productList = isDescending ? productList.OrderByDescending(c => c.Rating).ToList() : productList.OrderBy(c => c.Rating).ToList();
                        break;
                    case "stock":
                        productList = isDescending ? productList.OrderByDescending(c => c.Stock).ToList() : productList.OrderBy(c => c.Stock).ToList();
                        break;
                    case "complexity":
                        productList = isDescending ? productList.OrderByDescending(c => c.Complexity).ToList() : productList.OrderBy(c => c.Complexity).ToList();
                        break;
                }
            }

            #endregion

            productList = productList.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            
            return productList.Select(productEntity => new ProductListResponse
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

            // Apply basic filters
            if (!string.IsNullOrEmpty(filter.Name))
            {
                products = products.Where(x => EF.Functions.Like(x.Name, $"%{filter.Name}%"));
            }

            if (!string.IsNullOrEmpty(filter.Slug))
            {
                products = products.Where(x => EF.Functions.Like(x.Slug, $"%{filter.Slug}%"));
            }

            if (filter.DateFrom.HasValue)
            {
                products = products.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= filter.DateFrom.Value);
            }

            if (filter.DateTo.HasValue)
            {
                products = products.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value <= filter.DateTo.Value);
            }

            if (filter.Rating.HasValue)
            {
                var databaseRating = filter.Rating.Value * 2;
                products = products.Where(x => x.Rating >= databaseRating);
            }

            if (filter.MinPrice.HasValue)
            {
                products = products.Where(x => x.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                products = products.Where(x => x.Price <= filter.MaxPrice.Value);
            }

            // Category filters
            if (filter.CategoryIds != null && filter.CategoryIds.Any())
            {
                products = products.Where(x => x.ProductCategories.Any(pc => filter.CategoryIds.Contains(pc.CategoryId)));
            }

            if (filter.CategorySlugs != null && filter.CategorySlugs.Any())
            {
                products = products.Where(x => 
                    filter.CategorySlugs.All(requiredSlug => 
                        x.ProductCategories.Any(pc => pc.Category.Slug == requiredSlug)));
            }

            if (filter.InStockOnly.HasValue && filter.InStockOnly.Value)
            {
                products = products.Where(x => x.Stock > 0);
            }

            // Execute the query to get data for client-side filtering
            var productList = await products.ToListAsync();

            // Apply complex filters on the client side
            if (!string.IsNullOrEmpty(filter.Complexity))
            {
                if (int.TryParse(filter.Complexity, out int complexityValue))
                {
                    productList = productList.Where(x => x.Complexity != null && 
                        decimal.TryParse(x.Complexity, out decimal dbComplexity) && dbComplexity >= complexityValue).ToList();
                }
            }

            if (!string.IsNullOrEmpty(filter.MinPlayers))
            {
                var minPlayersValue = int.Parse(filter.MinPlayers);
                productList = productList.Where(x => x.NumberOfPlayers != null && 
                    (x.NumberOfPlayers.Contains("-") 
                        ? int.Parse(x.NumberOfPlayers.Substring(0, x.NumberOfPlayers.IndexOf("-"))) <= minPlayersValue
                        : int.Parse(x.NumberOfPlayers) <= minPlayersValue)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.MaxPlayers))
            {
                var maxPlayersValue = int.Parse(filter.MaxPlayers);
                productList = productList.Where(x => x.NumberOfPlayers != null && 
                    (x.NumberOfPlayers.Contains("-") 
                        ? int.Parse(x.NumberOfPlayers.Substring(x.NumberOfPlayers.IndexOf("-") + 1)) >= maxPlayersValue
                        : int.Parse(x.NumberOfPlayers) >= maxPlayersValue)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.MinPlayingTime))
            {
                var minPlayingTimeValue = int.Parse(filter.MinPlayingTime);
                productList = productList.Where(x => x.PlayingTime != null && 
                    (x.PlayingTime.Contains("-") 
                        ? int.Parse(x.PlayingTime.Substring(0, x.PlayingTime.IndexOf("-"))) <= minPlayingTimeValue
                        : int.Parse(x.PlayingTime) <= minPlayingTimeValue)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.MaxPlayingTime))
            {
                var maxPlayingTimeValue = int.Parse(filter.MaxPlayingTime);
                productList = productList.Where(x => x.PlayingTime != null && 
                    (x.PlayingTime.Contains("-") 
                        ? int.Parse(x.PlayingTime.Substring(x.PlayingTime.IndexOf("-") + 1)) >= maxPlayingTimeValue
                        : int.Parse(x.PlayingTime) >= maxPlayingTimeValue)).ToList();
            }

            return productList.Count;
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
