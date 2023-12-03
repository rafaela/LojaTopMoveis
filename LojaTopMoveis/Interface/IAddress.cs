using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Interface
{
    public interface IAddress
    {
        Task<ServiceResponse<List<Address>>> Get(Guid id);
        Task<ServiceResponse<Address>> Create(Address address);
        Task<ServiceResponse<Address>> GetByID(Guid id);
        Task<ServiceResponse<Address>> Update(Address address);
        Task<ServiceResponse<Address>> Delete(Guid id);

    }
}
