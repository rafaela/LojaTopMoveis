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

        public bool Remove(Subcategory subcategory)
        {
            try
            {
                if (subcategory != null)
                {
                    var sub = _context.Photos.Where(a => a.ID == subcategory.Id).FirstOrDefault();
                    if (sub == null)
                    {
                        _context.Subcategories.Remove(subcategory);
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
