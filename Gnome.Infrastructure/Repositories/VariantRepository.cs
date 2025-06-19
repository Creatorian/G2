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
    public class VariantRepository : IVariantRepository
    {
        private readonly AppDbContext _context;

        public VariantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<VariantListResponse>> GetVariants(int page, int pageSize, DateTime dateFrom, DateTime dateTo, string name = null, string sortBy = null, string sortOrder = "desc")
        {
            var variants = _context.Variants.AsQueryable().AsNoTracking();

            #region Filters

            variants = variants.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= dateFrom && x.CreatedDateTime <= dateTo);

            if (name != default)
            {
                variants = variants.Where(x => x.Name.Equals(name));
            }

            #endregion


            #region Sort

            if (!string.IsNullOrEmpty(sortBy))
            {
                var isDescending = sortOrder == "desc";

                switch (sortBy)
                {
                    case "created-date-time":
                        variants = isDescending ? variants.OrderByDescending(c => c.CreatedDateTime) : variants.OrderBy(c => c.CreatedDateTime);
                        break;
                    case "name":
                        variants = isDescending ? variants.OrderByDescending(c => c.Name) : variants.OrderBy(c => c.Name);
                        break;
                }
            }

            #endregion

            variants = variants.Skip(pageSize * (page - 1)).Take(pageSize);
            return await variants.Select(variantsEntity => new VariantListResponse
            {
                Id = variantsEntity.Id,
                Name = variantsEntity.Name,
                Slug = variantsEntity.Slug,
                Image = variantsEntity.Image,
                Price = variantsEntity.Price,
                Stock = variantsEntity.Stock,
                IsPrimary = variantsEntity.IsPrimary,
                CreatedDateTime = variantsEntity.CreatedDateTime,
                ProductId = variantsEntity.ProductId,
            }).ToListAsync();
        }

        public async Task<int> CountVariants(DateTime dateFrom, DateTime dateTo, string name = null)
        {
            var variants = _context.Variants.AsQueryable().AsNoTracking();

            #region Filters

            variants = variants.Where(x => x.CreatedDateTime.HasValue && x.CreatedDateTime.Value >= dateFrom && x.CreatedDateTime <= dateTo);

            if (name != default)
            {
                variants = variants.Where(x => x.Name.Equals(name));
            }

            #endregion

            return await variants.CountAsync();
        }


        public async Task<int> AddVariantAsync(Variant variant)
        {
            try
            {
                _context.Variants.Add(variant);
                await _context.SaveChangesAsync();
                return variant.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the variant.", ex);
            }
        }

        public async Task<Variant> GetVariantByIdAsync(int id)
        {
            return await _context.Variants
                .Include(v => v.Product)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Variant> GetVariantBySlugAsync(string slug)
        {
            return await _context.Variants
                .Include(v => v.Product)
                .FirstOrDefaultAsync(v => v.Slug == slug);
        }

        public async Task<List<Variant>> GetVariantsByProductIdAsync(int productId)
        {
            return await _context.Variants
                .Where(v => v.ProductId == productId)
                .ToListAsync();
        }

        public async Task<int> UpdateVariantAsync(Variant variant)
        {
            try
            {
                var existingVariant = await _context.Variants.FindAsync(variant.Id);
                if (existingVariant == null)
                    throw new InvalidOperationException("Variant not found.");

                existingVariant.Name = variant.Name;
                existingVariant.Slug = variant.Slug;
                existingVariant.Image = variant.Image;
                existingVariant.Price = variant.Price;
                existingVariant.Stock = variant.Stock;
                existingVariant.IsPrimary = variant.IsPrimary;
                existingVariant.ProductId = variant.ProductId;

                await _context.SaveChangesAsync();
                return variant.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the variant.", ex);
            }
        }

        public async Task<bool> DeleteVariantAsync(int id)
        {
            try
            {
                var variant = await _context.Variants.FindAsync(id);
                if (variant == null)
                    return false;

                _context.Variants.Remove(variant);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the variant.", ex);
            }
        }
    }
}
