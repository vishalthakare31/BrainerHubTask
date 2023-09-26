using BrainerHubTask.Model;

namespace BrainerHubTask.Interface
{
    public interface IProductService
    {


        // CRUD operations for products
        Product CreateProduct(Product product);
        Product GetProductById(int productId);
        Product UpdateProduct(int productId, Product updatedProduct);
        void DeleteProduct(int productId);
        IQueryable<Product> GetProducts(string searchKeyword = null, int page = 1, int pageSize = 10);


    }
}
