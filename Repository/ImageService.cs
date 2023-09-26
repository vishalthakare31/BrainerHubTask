using System;
using System.Linq;
using BrainerHubTask.Data;
using BrainerHubTask.Interface;
using BrainerHubTask.Model;

namespace BrainerHubTask.Services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _dbContext;

        public ImageService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Image AddImageToProduct(int productId, Image image)
        {
            var product = _dbContext.Products.Find(productId);
            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }

            image.ProductId = productId;
            _dbContext.Images.Add(image);
            _dbContext.SaveChanges();

            return image;
        }

        public Image GetImageById(int imageId)
        {
            var image = _dbContext.Images.Find(imageId);
            return image;
        }

        public Image UpdateImage(int imageId, Image updatedImage)
        {
            var existingImage = _dbContext.Images.Find(imageId);
            if (existingImage == null)
            {
                throw new ArgumentException("Image not found");
            }

            // Update properties of the existing image with the new values
            existingImage.FilePath = updatedImage.FilePath;
            // Update other properties as needed

            _dbContext.SaveChanges();
            return existingImage;
        }

        public void DeleteImage(int imageId)
        {
            var image = _dbContext.Images.Find(imageId);
            if (image == null)
            {
                throw new ArgumentException("Image not found");
            }

            _dbContext.Images.Remove(image);
            _dbContext.SaveChanges();
        }

        public IQueryable<Image> GetImagesForProduct(int productId)
        {
            var images = _dbContext.Images.Where(img => img.ProductId == productId);
            return images;
        }
    }
}
