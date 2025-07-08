using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductListResponse>> GetProducts(int page, int pageSize, ProductFilter filter, string sortBy = default, string sortOrder = "desc");
        Task<int> CountProducts(ProductFilter filter);
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProductBySlugAsync(string slug);
        Task<int> AddProductAsync(Product product);
        Task<int> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
