using System;
using System.Linq;
using System.Collections.Generic;
using BrainerHubTask.Model;
using Microsoft.EntityFrameworkCore;
using BrainerHubTask.Data;
using BrainerHubTask.Interface;

namespace BrainerHubTask.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext context)
        {
            dbContext = context;
        }

        // CRUD operations for products

        public Product CreateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            
            var existingProduct = dbContext.Products.FirstOrDefault(p => p.Name == product.Name);

            if (existingProduct != null)
            {
               
                throw new InvalidOperationException("Product name must be unique.");
            }


            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            return product;
        }

        public Product GetProductById(int productId)
        {
            return dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public Product UpdateProduct(int productId, Product updatedProduct)
        {
            var existingProduct = dbContext.Products.FirstOrDefault(p => p.ProductId == productId);

            if (existingProduct == null)
                throw new ArgumentException("Product not found");

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Quantity = updatedProduct.Quantity;

            dbContext.SaveChanges();

            return existingProduct;
        }

        public void DeleteProduct(int productId)
        {
            var productToDelete = dbContext.Products.FirstOrDefault(p => p.ProductId == productId);

            if (productToDelete == null)
                throw new ArgumentException("Product not found");

            dbContext.Products.Remove(productToDelete);
            dbContext.SaveChanges();
        }

        public IQueryable<Product> GetProducts(string searchKeyword = null, int page = 1, int pageSize = 10)
        {
            var query = dbContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                query = query.Where(p =>
                    p.Name.Contains(searchKeyword) ||
                    p.Description.Contains(searchKeyword)
                );
            }

            // Apply pagination
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return query;
        }

       
    }
}
