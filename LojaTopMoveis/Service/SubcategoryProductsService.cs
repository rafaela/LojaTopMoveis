using Loja.Model;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class SubcategoryProductsService
    {
        private readonly LojaContext _context;

        public SubcategoryProductsService(LojaContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(List<SubcategoriesProduct> subcategories)
        {
            try
            {
                if (subcategories != null && subcategories.Count > 0)
                {
                    var lista = subcategories.ToList();
                    foreach (var sub in lista)
                    {
                        SubcategoriesProduct subcategory = new SubcategoriesProduct();

                        if(sub.Id == null)
                        {
                            subcategory.SubcategoryId = sub.SubcategoryId;
                            subcategory.ProductId = sub.ProductId;
                            subcategory.Name = sub.Name;

                            _context.SubcategoriesProducts.Add(subcategory);
                        }
                        else
                        {
                            var data =  _context.Subcategories.Where(a => a.Id == sub.Id).FirstOrDefault();
                            if(data != null)
                            {
                                sub.ChangeDate = DateTime.Now.ToLocalTime();
                                _context.SubcategoriesProducts.Update(sub);
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

        public bool Remove(SubcategoriesProduct subcategory)
        {
            try
            {
                if (subcategory != null)
                {
                    var sub = _context.SubcategoriesProducts.Where(a => a.Id == subcategory.Id).FirstOrDefault();
                    if (sub == null)
                    {
                        _context.SubcategoriesProducts.Remove(subcategory);
                    }
                    else
                    {
                        return false;
                    }
                    _context.SaveChangesAsync();
                    return true;

                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }
            return true;

        }
    }
}
