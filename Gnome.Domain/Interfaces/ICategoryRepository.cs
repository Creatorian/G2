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
        Task<List<CategoryListResponse>> GetCategories(int page, int pageSize, DateTime dateFrom, DateTime dateTo, string name = default, string sortBy = default, string sortOrder = "desc");
        Task<int> CountCategories(DateTime dateFrom, DateTime dateTo, string name = default);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryBySlugAsync(string slug);
        Task<int> AddCategoryAsync(Category category);
        Task<int> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
