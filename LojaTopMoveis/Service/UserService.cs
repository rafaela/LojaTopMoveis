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

                    var search = _context.Usuarios.Where(a => a.Email == user.Email).FirstOrDefault();
                    if(search != null)
                    {
                        _context.Usuarios.Update(user);
                    }
                    else
                    {
                        _context.Usuarios.Add(user);
                    }
                    
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

        public string QuickHash(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var inputHash = MD5.HashData(inputBytes);
            return Convert.ToHexString(inputHash);
        }

        public bool Update(User user)
        {
            try
            {
                if (user != null)
                {
                    var usuario = _context.Usuarios.Where(a => a.Email == user.Email).FirstOrDefault();
                    if(usuario != null)
                    {
                        usuario.UserName = user.Email;
                        usuario.PasswordHash = QuickHash(user.PasswordHash);
                        _context.SaveChanges();
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

        
    }
}
