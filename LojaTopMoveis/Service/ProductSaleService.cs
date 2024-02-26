using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class ProductSaleService : IProductsSale
    {
        private readonly LojaContext _context;

        public ProductSaleService(LojaContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(List<ProductsSale> products, Guid saleId)
        {
            /*try
            {
                if (products != null && products.Count > 0)
                {
                    var lista = products.ToList();
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
            }*/
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<ServiceResponse<List<ProductsSale>>> Search(Guid id)
        {
            ServiceResponse<List<ProductsSale>> serviceResponse = new ServiceResponse<List<ProductsSale>>();
            /*try
            {
                var subs = await _context.Subcategories.Where(a => a.CategoryId == id).ToListAsync();
                if (subs.Count > 0)
                {
                    serviceResponse.Data = subs;
                    serviceResponse.Message = "";
                    serviceResponse.Sucess = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }*/

            return serviceResponse;
        }

        
    }
}
