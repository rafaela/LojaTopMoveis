using Loja.Model;
using LojaTopMoveis.Interface;
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

                SubcategoryService subcategoryService = new SubcategoryService(_context);
                var cadastro = subcategoryService.Create(category.Subcategories, category.Id);

                if (!cadastro.Result)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Erro ao cadastrar/atualizar subcategoria";
                    serviceResponse.Sucess = false;

                }
                else
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria cadastrada";
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
                    var subcategories = await _context.Subcategories.Where(a => a.CategoryId == category.Id).ToListAsync();

                    SubcategoryService subcategoryService = new SubcategoryService(_context);
                    foreach(var sub in subcategories)
                    {
                        var remove = subcategoryService.Remove((Guid)sub.Id);
                        if (!remove.Result.Sucess)
                        {
                            serviceResponse.Message = "Categoria não pode ser removida pois esta sendo utilizada.";
                            serviceResponse.Sucess = false;
                            return serviceResponse;
                        }
                    }
                    
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
                Category? category = await _context.Categories.Include(a => a.Subcategories).FirstOrDefaultAsync(a => a.Id == id);

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

        public async Task<ServiceResponse<List<Category>>> Get(ServiceParameter<Category> sp)
        {
            ServiceResponse<List<Category>> serviceResponse = new ServiceResponse<List<Category>>();

            try
            {
                var query = _context.Categories.Include(a => a.Subcategories).AsQueryable();
                if (sp.Data != null && sp.Data.Name != null)
                {
                    query = query.Where(a => a.Name != null && a.Name.Contains(sp.Data.Name));
                }
                if (sp.Data != null && sp.Data.Inactive == true)
                {
                    query = query.Where(a => a.Inactive);
                }
                else
                {
                    query = query.Where(a => !a.Inactive);
                }
                serviceResponse.Total = query.Count();

                query = query.OrderBy(a => a.Name);
                
                if(sp.Take > 0)
                {
                    query = query.Skip(sp.Skip).Take(sp.Take);
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

                    SubcategoryService subcategoryService = new SubcategoryService(_context);
                    var cadastro = subcategoryService.Create(category.Subcategories, category.Id);

                    if (!cadastro.Result)
                    {
                        serviceResponse.Data = null;
                        serviceResponse.Message = "Erro ao cadastrar/atualizar subcategoria";
                        serviceResponse.Sucess = false;

                    }
                    else
                    {
                        serviceResponse.Message = "Categoria atualizada";
                        serviceResponse.Sucess = true;
                    }

                   
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
