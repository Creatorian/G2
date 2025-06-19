using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Domain.Interfaces
{
    public interface IVariantRepository
    {
        Task<List<VariantListResponse>> GetVariants(int page, int pageSize, DateTime dateFrom, DateTime dateTo, string name = default, string sortBy = default, string sortOrder = "desc");
        Task<int> CountVariants(DateTime dateFrom, DateTime dateTo, string name = default);
        Task<Variant> GetVariantByIdAsync(int id);
        Task<Variant> GetVariantBySlugAsync(string slug);
        Task<List<Variant>> GetVariantsByProductIdAsync(int productId);
        Task<int> AddVariantAsync(Variant variant);
        Task<int> UpdateVariantAsync(Variant variant);
        Task<bool> DeleteVariantAsync(int id);
    }
}
