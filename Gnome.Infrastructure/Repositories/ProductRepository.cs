using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProductListResponse>> GetProducts(int page, int pageSize, DateTime dateFrom, DateTime dateTo, string name = default, string sortBy = default, string sortOrder = "desc")
        {
            var products = _context.Products.AsQueryable().AsNoTracking();

            #region Filters

            products = products.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= dateFrom && x.CreatedDateTime <= dateTo);

            if(name != default)
            {
                products = products.Where(x => x.Name.Equals(name));
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
                }
            }

            #endregion

            products = products.Skip(pageSize * (page - 1)).Take(pageSize);
            return await products.Select(productsEntity => new ProductListResponse
            {
                Id = productsEntity.Id,
                Slug = productsEntity.Slug,
                Description = productsEntity.Description,
                CreatedDateTime = productsEntity.CreatedDateTime,
                Variants = productsEntity.Variants
            }).ToListAsync();
        }

        public async Task<int> CountProducts(DateTime dateFrom, DateTime dateTo, string name = default)
        {
            var products = _context.Products.AsQueryable().AsNoTracking();

            #region Filters

            products = products.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= dateFrom && x.CreatedDateTime <= dateTo);

            if (name != default)
            {
                products = products.Where(x => x.Name.Equals(name));
            }

            #endregion

            return await products.CountAsync();
        }

    }
}
