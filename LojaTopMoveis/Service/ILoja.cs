using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public interface ILoja<T>
    {
        Task<ServiceResponse<List<T>>> Get();
        Task<ServiceResponse<List<T>>> GetFilter(T t);
        Task<ServiceResponse<T>> Create(T t);
        Task<ServiceResponse<T>> GetByID(Guid id);
        Task<ServiceResponse<T>> Update(T t);
        Task<ServiceResponse<T>> Delete(Guid id);
        Task<ServiceResponse<T>> Inactivate(Guid id);

    }
}
