using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class SaleService : ILoja<Sale>
    {
        private readonly LojaContext _context;

        public SaleService(LojaContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Sale>> Create(Sale sale)
        {
            ServiceResponse<Sale> serviceResponse = new ServiceResponse<Sale>();

            try
            {
                _context.Add(sale);
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

        public async Task<ServiceResponse<Sale>> Delete(Guid id)
        {
            ServiceResponse<Sale> serviceResponse = new ServiceResponse<Sale>();

            try
            {
                Sale? sale = await _context.Sales.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

               
                    _context.Sales.Remove(sale);

                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Categoria removida";
                    serviceResponse.Sucess = true;
                
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Sale>> GetByID(Guid id)
        {
            ServiceResponse<Sale> serviceResponse = new ServiceResponse<Sale>();
            try
            {
                Sale? sale = await _context.Sales.FirstOrDefaultAsync(a => a.Id == id);

                if (sale == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria não encontrada";
                    serviceResponse.Sucess = false;
                }

                serviceResponse.Data = sale;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Sale>>> Get(ServiceParameter<Sale> sp)
        {
            ServiceResponse<List<Sale>> serviceResponse = new ServiceResponse<List<Sale>>();

            try
            {
                var query = _context.Sales.AsQueryable();
                if (sp.Data != null && sp.Data.Name != null)
                {
                    query = query.Where(a => a.Name != null && a.Name.Contains(sp.Data.Name));
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

        public async Task<ServiceResponse<Sale>> Update(Sale sale)
        {
            ServiceResponse<Sale> serviceResponse = new ServiceResponse<Sale>();

            try
            {
                Sale? sale1 = await _context.Sales.AsNoTracking().FirstOrDefaultAsync(a => a.Id == sale.Id);

                

                if (sale1 == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    sale.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Sales.Update(sale);
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
