using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<List<CategoryListResponse>> GetCategories(int page, int pageSize, DateTime dateFrom, DateTime dateTo, string name = null, string sortBy = null, string sortOrder = "desc")
        {
            var categories = _context.Categories
                .Include(c => c.ProductCategories)
                .AsQueryable()
                .AsNoTracking();

            #region Filters

            categories = categories.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= dateFrom && x.CreatedDateTime <= dateTo);

            if (name != default)
            {
                categories = categories.Where(x => x.Name.Equals(name));
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

        public async Task<int> CountCategories(DateTime dateFrom, DateTime dateTo, string name = null)
        {
            var categories = _context.Categories.AsQueryable().AsNoTracking();

            #region Filters

            categories = categories.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= dateFrom && x.CreatedDateTime <= dateTo);

            if (name != default)
            {
                categories = categories.Where(x => x.Name.Equals(name));
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
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the category.", ex);
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
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the category.", ex);
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
                throw new InvalidOperationException("An error occurred while deleting the category.", ex);
            }
        }
    }
}
