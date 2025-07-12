using Gnome.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Domain.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> GetImageByIdAsync(int id);
        Task<List<Image>> GetImagesByProductIdAsync(int productId);
        Task<Image> GetPrimaryImageByProductIdAsync(int productId);
        Task<int> AddImageAsync(Image image);
        Task<int> UpdateImageAsync(Image image);
        Task<bool> DeleteImageAsync(int id);
        Task<bool> SetPrimaryImageAsync(int imageId);
    }
} 