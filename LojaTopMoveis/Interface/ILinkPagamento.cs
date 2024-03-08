using LojaTopMoveis.Model;

namespace LojaTopMoveis.Interface
{
    public interface ILinkPagamento
    {
        Task<string> buscaAccessToken();
        Task<ServiceResponse<LinkPagamento>> geraLinkPagamento(Guid id);
    }
}
