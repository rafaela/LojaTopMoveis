using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Interface
{
    public interface IProduct
    {
        Task<ServiceResponse<List<Product>>> Get();
        Task<ServiceResponse<List<Product>>> GetFeatured();
        Task<ServiceResponse<List<Product>>> GetFilter(Product product);
        Task<ServiceResponse<Product>> Create(Product product);
        Task<ServiceResponse<Product>> GetByID(Guid id);
        Task<ServiceResponse<Product>> Update(Product product);
        Task<ServiceResponse<Product>> Delete(Guid id);
        Task<ServiceResponse<Product>> Inactivate(Guid id);

    }
}
