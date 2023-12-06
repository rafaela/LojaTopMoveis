using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class CityService : ILoja<City>
    {
        private readonly LojaContext _context;

        public CityService(LojaContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<City>> Create(City city)
        {
            ServiceResponse<City> serviceResponse = new ServiceResponse<City>();

            try
            {
                if(city?.Id == null)
                {
                    _context.Cities.Add(city);
                }
                else
                {
                    City? city1 = await _context.Cities.AsNoTracking().FirstOrDefaultAsync(a => a.Id == city.Id);
                    city.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Cities.Update(city);
                }
                
                await _context.SaveChangesAsync();

                serviceResponse.Data = null;
                serviceResponse.Message = "";
                serviceResponse.Sucess = true;
                

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<City>> Delete(Guid id)
        {
            ServiceResponse<City> serviceResponse = new ServiceResponse<City>();

            try
            {
                City? city = await _context.Cities.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

                if (city == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Cidade não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    _context.Cities.Remove(city);

                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Cidade removida";
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

        public Task<ServiceResponse<City>> GetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<City>>> Get()
        {
            ServiceResponse<List<City>> serviceResponse = new ServiceResponse<List<City>>();

            try
            {
                serviceResponse.Data = await _context.Cities.ToListAsync();
                serviceResponse.Sucess = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

        public  Task<ServiceResponse<List<City>>> GetFilter(City city)
        {
            throw new NotImplementedException();

        }

        public Task<ServiceResponse<City>> Inactivate(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<City>> Update(City city)
        {
            ServiceResponse<City> serviceResponse = new ServiceResponse<City>();

            try
            {
                City? city1 = await _context.Cities.AsNoTracking().FirstOrDefaultAsync(a => a.Id == city.Id);

                

                if (city1 == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Cidade não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    city.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Cities.Update(city);
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
