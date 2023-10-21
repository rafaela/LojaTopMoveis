using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public interface IProductInterface
    {
        Task<ServiceResponse<List<Product>>> GetProducts();
        Task<ServiceResponse<List<Product>>> CreateProducts(Product product);
        Task<ServiceResponse<Product>> GetProductByID(Guid id);
        Task<ServiceResponse<List<Product>>> UpdateProduct(Product product);
        Task<ServiceResponse<List<Product>>> DeleteProduct(Guid id);
        Task<ServiceResponse<List<Product>>> InactivateProduct(Guid id);

    }
}
