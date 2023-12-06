using LojaTopMoveis.Model;
using Topmoveis.Model;

namespace LojaTopMoveis.Interface
{
    public interface ISubcategory
    {
        Task<bool> Create(List<Subcategory> subcategories, Guid categoryId);
        bool Remove(Subcategory subcategory);
        Task<ServiceResponse<List<Subcategory>>> SearchSubcategories(Guid id);

    }
}
