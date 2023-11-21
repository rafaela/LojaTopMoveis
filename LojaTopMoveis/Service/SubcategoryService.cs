using Loja.Model;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class SubcategoryService
    {
        private readonly LojaContext _context;

        public SubcategoryService(LojaContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(List<Subcategory> subcategories, Guid categoryId)
        {
            try
            {
                if (subcategories != null && subcategories.Count > 0)
                {
                    var lista = subcategories.ToList();
                    foreach (var sub in lista)
                    {
                        Subcategory subcategory = new Subcategory();

                        if(sub.Id == null)
                        {
                            subcategory.Name = sub.Name;
                            subcategory.CategoryId = categoryId;

                            _context.Subcategories.Add(subcategory);
                        }
                        else
                        {
                            var data =  _context.Subcategories.Where(a => a.Id == sub.Id).FirstOrDefault();
                            if(data != null)
                            {
                                sub.ChangeDate = DateTime.Now.ToLocalTime();
                                _context.Subcategories.Update(sub);
                            }
                        }
                        
                    }

                    await _context.SaveChangesAsync();
                    
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }
            return true;

        }

        /*public async Task<ServiceResponse<Subcategory>> Delete(Guid id)
        {
            ServiceResponse<Subcategory> serviceResponse = new ServiceResponse<Subcategory>();

            try
            {
                Subcategory? subcategory = await _context.Subcategories.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

                if (subcategory == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    var subcategories = await _context.Subcategories.Where(a => a.CategoryId == subcategory.Id).ToListAsync();
                    _context.Subcategories.RemoveRange(subcategories);
                    _context.Subcategories.Remove(subcategory);

                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Subcategoria removida";
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

        public async Task<ServiceResponse<Subcategory>> GetByID(Guid id)
        {
            ServiceResponse<Subcategory> serviceResponse = new ServiceResponse<Subcategory>();
            /*try
            {
                Subcategory? subcategory = await _context.Subcategories.FirstOrDefaultAsync(a => a.Id == id);

                if (categsubcategoryory == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria não encontrada";
                    serviceResponse.Sucess = false;
                }

                serviceResponse.Data = subcategory;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Subcategory>>> Get()
        {
            ServiceResponse<List<Subcategory>> serviceResponse = new ServiceResponse<List<Subcategory>>();

            try
            {
                serviceResponse.Data = await _context.Subcategories.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<Subcategory>> Update(Subcategory subcategory)
        {
            ServiceResponse<Subcategory> serviceResponse = new ServiceResponse<Subcategory>();

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

                    if (category.Subcategories != null && category.Subcategories.Count > 0)
                    {
                        var lista = category.Subcategories.ToList();
                        foreach (var sub in lista)
                        {
                            Subcategory subcategory = new Subcategory();
                            subcategory.Name = sub.Name;
                            subcategory.CategoryId = category.Id;

                            _context.Subcategories.Add(subcategory);
                        }
                    }

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
        }*/
    }
}
