using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Interface
{
    public interface ISale
    {
        Task<ServiceResponse<List<VendasResponse>>> Get(ServiceParameter<VendasResponse> sp);
        Task<ServiceResponse<Sale>> Create(Sale sale);
        Task<ServiceResponse<VendasResponse>> GetByID(Guid id);
        Task<ServiceResponse<Sale>> Update(Sale sale);
        Task<ServiceResponse<Sale>> Delete(Guid id);
        Task<ServiceResponse<Sale>> ChangeStatusSale(Sale sale);
        Task<ServiceResponse<List<VendasResponse>>> GetDataSale(Guid id);
        Task<ServiceResponse<VendasResponse>> ChangeStatusDelivery(Guid id);
        Task<ServiceResponse<VendasResponse>> ChangeStatusPayment(Guid id);


    }
}
