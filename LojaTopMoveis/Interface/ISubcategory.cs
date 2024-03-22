using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Interface
{
    public interface ISubcategory
    {
        Task<bool> Create(List<Subcategory> subcategories, Guid categoryId);
        Task<ServiceResponse<Subcategory>> Remove(Guid id);
        Task<ServiceResponse<List<Subcategory>>> SearchSubcategories(Guid id);

    }
}
