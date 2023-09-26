using System.Linq;
using BrainerHubTask.Model;

namespace BrainerHubTask.Interface
{
    public interface IImageService
    {
        Image AddImageToProduct(int productId, Image image);
        Image GetImageById(int imageId);
        Image UpdateImage(int imageId, Image updatedImage);
        void DeleteImage(int imageId);
        IQueryable<Image> GetImagesForProduct(int productId);
    }
}
