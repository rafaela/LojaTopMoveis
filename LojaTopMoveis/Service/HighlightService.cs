using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class HighlightService : ILoja<Highlight>
    {
        private readonly LojaContext _context;
        private readonly string _filePath;

        public HighlightService(LojaContext context)
        {
            _context = context;
            _filePath = "wwwroot/images";
        }

        public string Save(string image)
        {
            var fileExt = image.Substring(image.IndexOf("/") + 1, image.IndexOf(";") - image.IndexOf("/") - 1); //png jpg

            var base64Code = image.Substring(image.IndexOf(",") + 1);

            var imgbytes = Convert.FromBase64String(base64Code);

            var fileName = Guid.NewGuid().ToString() + "." + fileExt;

            using (var imageFile = new FileStream(_filePath + "/" + fileName, FileMode.Create))
            {
                imageFile.Write(imgbytes, 0, imgbytes.Length);
                imageFile.Flush();

            }

            return _filePath + "/" + fileName;
        }

        public async Task<ServiceResponse<Highlight>> Create(Highlight highlight)
        {
            ServiceResponse<Highlight> serviceResponse = new ServiceResponse<Highlight>();

            try
            {
                _context.Highlights.Add(highlight);
                _context.SaveChanges();


                serviceResponse.Data = null;
                serviceResponse.Message = "Imagem cadastrada";
                serviceResponse.Sucess = true;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Highlight>> Delete(Guid id)
        {
            ServiceResponse<Highlight> serviceResponse = new ServiceResponse<Highlight>();

            try
            {
                Highlight? highlight = await _context.Highlights.AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();

                if (highlight == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Imagem não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    _context.Highlights.Remove(highlight);

                    _context.SaveChanges();
                    serviceResponse.Message = "Imagem removida";
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

        public async Task<ServiceResponse<Highlight>> GetByID(Guid id)
        {
            ServiceResponse<Highlight> serviceResponse = new ServiceResponse<Highlight>();
            try
            {
                Highlight? highlight = _context.Highlights.Where(a => a.Id == id).FirstOrDefault();

                if (highlight == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Imagem não encontrada";
                    serviceResponse.Sucess = false;
                }

                serviceResponse.Data = highlight;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Highlight>>> Get(ServiceParameter<Highlight> sp)
        {
            ServiceResponse<List<Highlight>> serviceResponse = new ServiceResponse<List<Highlight>>();

            try
            {
                var query = _context.Highlights.AsQueryable();
                if (sp.Data != null && sp.Data.Name != null)
                {
                    query = query.Where(a => a.Name != null && a.Name.Contains(sp.Data.Name));
                }
                if (sp.Data != null && sp.Data.Inactive == true)
                {
                    query = query.Where(a => a.Inactive.HasValue && (bool)a.Inactive);
                }
                else
                {
                    query = query.Where(a => !a.Inactive.HasValue || !(bool)a.Inactive);
                }
                
                serviceResponse.Total = query.Count();

                query = query.OrderBy(a => a.Name);
                
                if(sp.Take > 0)
                {
                    query = query.Skip(sp.Skip).Take(sp.Take);
                }
                var lista = query.ToList();
                
                serviceResponse.Data = lista;
                
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

       
        public async Task<ServiceResponse<Highlight>> Update(Highlight highlight)
        {
            ServiceResponse<Highlight> serviceResponse = new ServiceResponse<Highlight>();

            try
            {
                Highlight? highlight1 = _context.Highlights.AsNoTracking().Where(a => a.Id == highlight.Id).FirstOrDefault();

                

                if (highlight1 == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Imagem não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    highlight.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Highlights.Update(highlight);
                    _context.SaveChanges();
                    
                    serviceResponse.Message = "Imagem atualizada";
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
