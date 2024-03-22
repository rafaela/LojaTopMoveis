using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Interface
{
    public interface IPhoto
    {
        string Save(string image);
        Task<bool> Create(List<Photo> photos);
        bool Update(List<Photo> photos);
        Task<ServiceResponse<Photo>> Remove(Guid id);


    }
}
