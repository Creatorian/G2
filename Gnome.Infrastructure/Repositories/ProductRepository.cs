using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
                Name = productsEntity.Name,
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

        public async Task<int> AddProductAsync(Product product)
        {
            //try
            //{

            //    // Prepare parameters
            //    var productNameParam = new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = product.Name };
            //    var productSlugParam = new SqlParameter("@ProductSlug", SqlDbType.NVarChar) { Value = product.Slug };
            //    var descriptionParam = new SqlParameter("@Description", SqlDbType.NVarChar) { Value = (object?)product.Description ?? DBNull.Value };
            //    var categoryIdParam = new SqlParameter("@CategoryId", SqlDbType.Int) { Value = product.CategoryId };


            //    var sql = @"
            //        DECLARE @NewProductId INT;
            //        EXEC [dbo].[CreateProduct]
            //            @ProductName = @ProductName,
            //            @ProductSlug = @ProductSlug,
            //            @Description = @Description,
            //            @CategoryId = @CategoryId
            //        SELECT @NewProductId = SCOPE_IDENTITY();
            //        SELECT @NewProductId AS Id;
            //    ";

            //    var newProductId = 0;
            //    using (var command = _context.Database.GetDbConnection().CreateCommand())
            //    {
            //        command.CommandText = sql;
            //        command.CommandType = CommandType.Text;
            //        command.Parameters.Add(productNameParam);
            //        command.Parameters.Add(productSlugParam);
            //        command.Parameters.Add(descriptionParam);
            //        command.Parameters.Add(categoryIdParam);

            //        if (command.Connection.State != ConnectionState.Open)
            //            await command.Connection.OpenAsync();

            //        using (var reader = await command.ExecuteReaderAsync())
            //        {
            //            while (await reader.ReadAsync())
            //            {
            //                newProductId = reader.GetInt32(0);
            //            }
            //        }
            //    }

            //    return newProductId;
            //}
            //catch (SqlException ex)
            //{
            //    // Log the SQL error (replace with your logger if available)
            //    // _logger?.LogError(ex, "SQL error occurred while adding product.");
            //    throw new InvalidOperationException("A database error occurred while adding the product.", ex);
            //}
            //catch (Exception ex)
            //{
            //    // Log the general error (replace with your logger if available)
            //    // _logger?.LogError(ex, "An error occurred while adding product.");
            //    throw new InvalidOperationException("An unexpected error occurred while adding the product.", ex);
            //}

            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the product.", ex);
            }
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            try
            {
                var existingProduct = await _context.Products.FindAsync(product.Id);
                if (existingProduct == null)
                    throw new InvalidOperationException("Product not found.");

                existingProduct.Name = product.Name;
                existingProduct.Slug = product.Slug;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;

                await _context.SaveChangesAsync();
                return product.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the product.", ex);
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetProductBySlugAsync(string slug)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Variants)
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
                throw new InvalidOperationException("An error occurred while deleting the product.", ex);
            }
        }
    }
}
