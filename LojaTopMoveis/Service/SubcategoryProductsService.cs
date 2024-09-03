using Loja.Model;
using LojaTopMoveis.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Linq;
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
                     foreach (var sub in subcategories.ToList())
                     {

                        
                        if (sub.Id == null)
                        {
                            SubcategoriesProduct subcategory = new SubcategoriesProduct();
                            subcategory.SubcategoryId = sub.SubcategoryId;
                            var searchsub = _context.Subcategories.Where(a => a.Id == sub.SubcategoryId).FirstOrDefault();
                            subcategory.ProductId = sub.ProductId;

                            if (searchsub != null)
                            {
                                subcategory.Name = searchsub.Name;
                            }

                            _context.SubcategoriesProducts.Add(subcategory);
                        }
                        else{
                            var subcategory =  _context.SubcategoriesProducts.FirstOrDefault(a => a.Id == sub.Id);

                            subcategory.SubcategoryId = sub.SubcategoryId;
                            var searchsub = _context.Subcategories.Where(a => a.Id == sub.SubcategoryId).FirstOrDefault();
                            subcategory.ProductId = sub.ProductId;

                            if (searchsub != null)
                            {
                                subcategory.Name = searchsub.Name;
                            }
                            _context.SubcategoriesProducts.Update(subcategory);
                        }
                        
                     }
                }
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                ///var message = ex.Message;
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
