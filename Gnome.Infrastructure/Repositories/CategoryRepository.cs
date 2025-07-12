using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gnome.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryListResponse>> GetCategories(int page, int pageSize, CategoryFilter filter, string sortBy = null, string sortOrder = "desc")
        {
            var categories = _context.Categories
                .Include(c => c.ProductCategories)
                .AsQueryable()
                .AsNoTracking();

            #region Filters

            // Date range filter
            if (filter.DateFrom.HasValue)
            {
                categories = categories.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= filter.DateFrom.Value);
            }
            if (filter.DateTo.HasValue)
            {
                categories = categories.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value <= filter.DateTo.Value);
            }

            // Text-based filters (case-insensitive contains search)
            if (!string.IsNullOrEmpty(filter.Name))
            {
                categories = categories.Where(x => EF.Functions.Like(x.Name, $"%{filter.Name}%"));
            }

            if (!string.IsNullOrEmpty(filter.Slug))
            {
                categories = categories.Where(x => EF.Functions.Like(x.Slug, $"%{filter.Slug}%"));
            }

            // Boolean filters
            if (filter.HasProducts.HasValue)
            {
                if (filter.HasProducts.Value)
                {
                    categories = categories.Where(x => x.ProductCategories.Any());
                }
                else
                {
                    categories = categories.Where(x => !x.ProductCategories.Any());
                }
            }

            // Numeric range filters for product count
            if (filter.MinProductsCount.HasValue)
            {
                categories = categories.Where(x => x.ProductCategories.Count >= filter.MinProductsCount.Value);
            }

            if (filter.MaxProductsCount.HasValue)
            {
                categories = categories.Where(x => x.ProductCategories.Count <= filter.MaxProductsCount.Value);
            }

            #endregion

            #region Sort

            if (!string.IsNullOrEmpty(sortBy))
            {
                var isDescending = sortOrder == "desc";

                switch (sortBy)
                {
                    case "created-date-time":
                        categories = isDescending ? categories.OrderByDescending(c => c.CreatedDateTime) : categories.OrderBy(c => c.CreatedDateTime);
                        break;
                    case "name":
                        categories = isDescending ? categories.OrderByDescending(c => c.Name) : categories.OrderBy(c => c.Name);
                        break;
                    case "slug":
                        categories = isDescending ? categories.OrderByDescending(c => c.Slug) : categories.OrderBy(c => c.Slug);
                        break;
                    case "products-count":
                        categories = isDescending ? categories.OrderByDescending(c => c.ProductCategories.Count) : categories.OrderBy(c => c.ProductCategories.Count);
                        break;
                }
            }

            #endregion

            categories = categories.Skip(pageSize * (page - 1)).Take(pageSize);
            return await categories.Select(categoriesEntity => new CategoryListResponse
            {
                Id = categoriesEntity.Id,
                Name = categoriesEntity.Name,
                Slug = categoriesEntity.Slug,
                CreatedDateTime = categoriesEntity.CreatedDateTime,
                ProductsCount = categoriesEntity.ProductCategories.Count
            }).ToListAsync();
        }

        public async Task<int> CountCategories(CategoryFilter filter)
        {
            var categories = _context.Categories
                .Include(c => c.ProductCategories)
                .AsQueryable()
                .AsNoTracking();

            #region Filters

            // Date range filter
            if (filter.DateFrom.HasValue)
            {
                categories = categories.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= filter.DateFrom.Value);
            }
            if (filter.DateTo.HasValue)
            {
                categories = categories.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value <= filter.DateTo.Value);
            }

            // Text-based filters (case-insensitive contains search)
            if (!string.IsNullOrEmpty(filter.Name))
            {
                categories = categories.Where(x => EF.Functions.Like(x.Name, $"%{filter.Name}%"));
            }

            if (!string.IsNullOrEmpty(filter.Slug))
            {
                categories = categories.Where(x => EF.Functions.Like(x.Slug, $"%{filter.Slug}%"));
            }

            // Boolean filters
            if (filter.HasProducts.HasValue)
            {
                if (filter.HasProducts.Value)
                {
                    categories = categories.Where(x => x.ProductCategories.Any());
                }
                else
                {
                    categories = categories.Where(x => !x.ProductCategories.Any());
                }
            }

            // Numeric range filters for product count
            if (filter.MinProductsCount.HasValue)
            {
                categories = categories.Where(x => x.ProductCategories.Count >= filter.MinProductsCount.Value);
            }

            if (filter.MaxProductsCount.HasValue)
            {
                categories = categories.Where(x => x.ProductCategories.Count <= filter.MaxProductsCount.Value);
            }

            #endregion

            return await categories.CountAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.ProductCategories)
                    .ThenInclude(pc => pc.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> GetCategoryBySlugAsync(string slug)
        {
            return await _context.Categories
                .Include(c => c.ProductCategories)
                    .ThenInclude(pc => pc.Product)
                .FirstOrDefaultAsync(c => c.Slug == slug);
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category.Id;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) // Unique constraint violation
            {
                throw new InvalidOperationException("A category with this slug already exists.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error adding category: {ex.Message}");
            }
        }

        public async Task<int> UpdateCategoryAsync(Category category)
        {
            try
            {
                var existingCategory = await _context.Categories.FindAsync(category.Id);
                if (existingCategory == null)
                    throw new InvalidOperationException("Category not found.");

                existingCategory.Name = category.Name;
                existingCategory.Slug = category.Slug;

                await _context.SaveChangesAsync();
                return category.Id;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) // Unique constraint violation
            {
                throw new InvalidOperationException("A category with this slug already exists.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error updating category: {ex.Message}");
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                    return false;

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting category: {ex.Message}");
            }
        }
    }
}
