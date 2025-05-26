using Gnome.Domain.Interfaces;
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
            var categories = _context.Categories.AsQueryable().AsNoTracking();

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
                CreatedDateTime = categoriesEntity.CreatedDateTime
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
    }
}
