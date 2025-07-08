using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryListResponse>> GetCategories(int page, int pageSize, CategoryFilter filter, string sortBy = default, string sortOrder = "desc");
        Task<int> CountCategories(CategoryFilter filter);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryBySlugAsync(string slug);
        Task<int> AddCategoryAsync(Category category);
        Task<int> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
