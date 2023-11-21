using Loja.Model;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class UserService
    {
        private readonly LojaContext _context;

        public UserService(LojaContext context)
        {
            _context = context;
        }

        public string Create(User user)
        {
            try
            {
                if (user != null)
                {
                    user.UserName = user.Email;
                    user.PasswordHash = QuickHash(user.PasswordHash);
                    _context.Usuarios.Add(user);
                    _context.SaveChangesAsync();

                    return user.Id;
                    
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return "";
            }
            return "";

        }

        string QuickHash(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var inputHash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(inputHash);
        }

        public async Task<bool> Update(User user)
        {
            try
            {
                if (user != null)
                {
                    var usuario = _context.Usuarios.Where(a => a.Email == user.Email).FirstOrDefault();
                    if(usuario != null)
                    {
                        user.UserName = user.Email;
                        user.PasswordHash = QuickHash(user.PasswordHash);
                        _context.Usuarios.Add(user);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                    

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
