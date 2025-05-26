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
    }
}
