using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class ColorService : IColor
    {
        private readonly LojaContext _context;

        public ColorService(LojaContext context)
        {
            _context = context;
          
        }

        

        public async Task<bool> Create(List<Color> colors)
        {
            try
            {
                if (colors != null && colors.Count > 0)
                {
                    var lista = colors.ToList();
                    foreach(var cor in lista) {
                        var image = _context.Colors.Where(a => a.Id == cor.Id).FirstOrDefault();

                        if (image == null)
                        {
                            _context.Colors.Add(cor);
                        }
                        else
                        {
                            image = cor;
                        }
                    }

                    _context.SaveChanges();
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

        

        public async Task<ServiceResponse<Color>> Remove(Guid id)
        {
            ServiceResponse<Color> serviceResponse = new ServiceResponse<Color>();
            try
            {
                var cor = _context.Colors.Where(a => a.Id == id).FirstOrDefault();
                if (cor == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Cor não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    _context.Colors.Remove(cor);

                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Cor removida";
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
