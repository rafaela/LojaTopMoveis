using Loja.Model;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class CategoryService : ILoja<Category>
    {
        private readonly LojaContext _context;

        public CategoryService(LojaContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Category>> Create(Category category)
        {
            ServiceResponse<Category> serviceResponse = new ServiceResponse<Category>();

            try
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                serviceResponse.Data = null;
                serviceResponse.Message = "Categoria cadastrada";
                serviceResponse.Sucess = true;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Category>> Delete(Guid id)
        {
            ServiceResponse<Category> serviceResponse = new ServiceResponse<Category>();

            try
            {
                Category? category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

                if (category == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    _context.Categories.Remove(category);

                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Categoria removida";
                    serviceResponse.Sucess = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Category>> GetByID(Guid id)
        {
            ServiceResponse<Category> serviceResponse = new ServiceResponse<Category>();
            try
            {
                Category? category = await _context.Categories.FirstOrDefaultAsync(a => a.Id == id);

                if (category == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria não encontrada";
                    serviceResponse.Sucess = false;
                }

                serviceResponse.Data = category;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Category>>> Get()
        {
            ServiceResponse<List<Category>> serviceResponse = new ServiceResponse<List<Category>>();

            try
            {
                serviceResponse.Data = await _context.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<Category>>> GetFilter(Category category)
        {
            ServiceResponse<List<Category>> serviceResponse = new ServiceResponse<List<Category>>();

            try
            {
                var query = _context.Categories.AsQueryable();
                if (category.Name != null)
                {
                    query = query.Where(a => a.Name != null && a.Name.Equals(category.Name));
                }
                if (category.Inactive == true)
                {
                    query = query.Where(a => !a.Inactive);
                }
                else
                {
                    query = query.Where(a => a.Inactive);
                }
                serviceResponse.Data = await query.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<Category>> Inactivate(Guid id)
        {
            ServiceResponse<Category> serviceResponse = new ServiceResponse<Category>();

            try
            {
                Category? category = await _context.Categories.FirstOrDefaultAsync(a => a.Id == id);

                if (category == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    category.Inactive = true;
                    category.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Categories.Update(category);
                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Produto inativado";
                    serviceResponse.Sucess = true;

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Category>> Update(Category category)
        {
            ServiceResponse<Category> serviceResponse = new ServiceResponse<Category>();

            try
            {
                Category? category1 = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(a => a.Id == category.Id);

                if (category1 == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    category.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Categories.Update(category);

                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Categoria atualizada";
                    serviceResponse.Sucess = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }
    }
}
