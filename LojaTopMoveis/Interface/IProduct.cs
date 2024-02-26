using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Interface
{
    public interface IProduct
    {
        Task<ServiceResponse<List<Product>>> Get(ServiceParameter<Product> sp);
        Task<ServiceResponse<Product>> Create(Product product);
        Task<ServiceResponse<Product>> GetByID(Guid id);
        Task<ServiceResponse<Product>> Update(Product product);
        Task<ServiceResponse<Product>> Delete(Guid id);
        Task<ServiceResponse<List<Product>>> GetByCategory(Guid id);
        Task<ServiceResponse<List<Product>>> GetBySubcategory(Guid id);
        Task<ServiceResponse<List<Product>>> GetFeatured();


    }
}
