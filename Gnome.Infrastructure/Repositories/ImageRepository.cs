using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly AppDbContext _context;

        public ImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Image> GetImageByIdAsync(int id)
        {
            return await _context.Images
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Image>> GetImagesByProductIdAsync(int productId)
        {
            return await _context.Images
                .Where(i => i.ProductId == productId)
                .OrderByDescending(i => i.IsPrimary)
                .ThenBy(i => i.CreatedDateTime)
                .ToListAsync();
        }

        public async Task<Image> GetPrimaryImageByProductIdAsync(int productId)
        {
            return await _context.Images
                .FirstOrDefaultAsync(i => i.ProductId == productId && i.IsPrimary);
        }

        public async Task<int> AddImageAsync(Image image)
        {
            try
            {
                _context.Images.Add(image);
                await _context.SaveChangesAsync();
                return image.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the image.", ex);
            }
        }

        public async Task<int> UpdateImageAsync(Image image)
        {
            try
            {
                var existingImage = await _context.Images.FindAsync(image.Id);
                if (existingImage == null)
                    throw new InvalidOperationException("Image not found.");

                existingImage.Url = image.Url;
                existingImage.IsPrimary = image.IsPrimary;
                existingImage.ProductId = image.ProductId;

                await _context.SaveChangesAsync();
                return image.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the image.", ex);
            }
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            try
            {
                var image = await _context.Images.FindAsync(id);
                if (image == null)
                    return false;

                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the image.", ex);
            }
        }

        public async Task<bool> SetPrimaryImageAsync(int imageId)
        {
            try
            {
                // First, get the image to find its productId
                var targetImage = await _context.Images
                    .FirstOrDefaultAsync(i => i.Id == imageId);
                
                if (targetImage == null)
                    return false;

                var productId = targetImage.ProductId;

                // Get all images for the product
                var images = await _context.Images
                    .Where(i => i.ProductId == productId)
                    .ToListAsync();

                if (!images.Any())
                    return false;

                // Set all images as non-primary
                foreach (var image in images)
                {
                    image.IsPrimary = false;
                }

                // Set the target image as primary
                targetImage.IsPrimary = true;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while setting the primary image.", ex);
            }
        }
    }
} 