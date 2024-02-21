using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Interface
{
    public interface ILoja<T>
    {
        Task<ServiceResponse<List<T>>> Get(ServiceParameter<T> sp);
        Task<ServiceResponse<T>> Create(T t);
        Task<ServiceResponse<T>> GetByID(Guid id);
        Task<ServiceResponse<T>> Update(T t);
        Task<ServiceResponse<T>> Delete(Guid id);

    }
}
