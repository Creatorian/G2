using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ImageRepository> _logger;

        public ImageRepository(AppDbContext context, ILogger<ImageRepository> logger)
        {
            _context = context;
            _logger = logger;
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
                _logger.LogInformation("Setting image {ImageId} as primary", imageId);
                
                // First, get the image to find its productId
                var targetImage = await _context.Images
                    .FirstOrDefaultAsync(i => i.Id == imageId);
                
                if (targetImage == null)
                {
                    _logger.LogWarning("Image {ImageId} not found", imageId);
                    return false;
                }

                var productId = targetImage.ProductId;
                _logger.LogInformation("Found image {ImageId} for product {ProductId}", imageId, productId);

                // Use a transaction to ensure atomicity
                using var transaction = await _context.Database.BeginTransactionAsync();
                
                try
                {
                    // First, find and update the current primary image (if any)
                    var currentPrimaryImage = await _context.Images
                        .FirstOrDefaultAsync(i => i.ProductId == productId && i.IsPrimary);
                    
                    if (currentPrimaryImage != null)
                    {
                        currentPrimaryImage.IsPrimary = false;
                        _logger.LogInformation("Set current primary image {ImageId} to non-primary", currentPrimaryImage.Id);
                        
                        // Save this change first to avoid constraint violation
                        await _context.SaveChangesAsync();
                    }

                    // Now set the target image as primary
                    targetImage.IsPrimary = true;
                    _logger.LogInformation("Set image {ImageId} as primary", imageId);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    
                    _logger.LogInformation("Successfully set image {ImageId} as primary for product {ProductId}", imageId, productId);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error setting image {ImageId} as primary", imageId);
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while setting the primary image {ImageId}", imageId);
                throw new InvalidOperationException("An error occurred while setting the primary image.", ex);
            }
        }
    }
} 