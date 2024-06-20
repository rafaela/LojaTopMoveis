using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Interface
{
    public interface IColor
    {
        Task<bool> Create(List<Color> colors);
        Task<ServiceResponse<Color>> Remove(Guid id);


    }
}
