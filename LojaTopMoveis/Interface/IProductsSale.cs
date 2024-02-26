using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Interface
{
    public interface IProductsSale
    {
        Task<bool> Create(List<ProductsSale> products, Guid sale);
        Task<ServiceResponse<List<ProductsSale>>> Search(Guid id);

    }
}
