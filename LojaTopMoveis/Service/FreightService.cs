using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class FreightService : ILoja<Freight>
    {
        private readonly LojaContext _context;

        public FreightService(LojaContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Freight>> Create(Freight freight)
        {
            ServiceResponse<Freight> serviceResponse = new ServiceResponse<Freight>();

            try
            {
                _context.Add(freight);
                await _context.SaveChangesAsync();
                
                if(freight.Cities != null && freight.Cities.Count > 0)
                {
                    CityService cityService = new CityService(_context);
                    foreach (City city in freight.Cities)
                    {
                        var success = cityService.Create(city);
                        if (!success.Result.Sucess)
                        {
                            serviceResponse.Data = null;
                            serviceResponse.Message = "Erro ao cadastrar cidade";
                            serviceResponse.Sucess = false;
                        }
                    }
                }
                
                serviceResponse.Data = null;
                serviceResponse.Message = "Frete cadastrado";
                serviceResponse.Sucess = true;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public Task<ServiceResponse<Freight>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Freight>> GetByID(Guid id)
        {
            ServiceResponse<Freight> serviceResponse = new ServiceResponse<Freight>();
            try
            {
                Freight? freight = await _context.Freights.Include(a => a.Cities).FirstOrDefaultAsync();

                if (freight == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Frete não encontrado";
                    serviceResponse.Sucess = false;
                }

                serviceResponse.Data = freight;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public Task<ServiceResponse<List<Freight>>> Get(ServiceParameter<Freight> sp)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<Freight>>> GetFilter(Freight category)
        {
            throw new NotImplementedException();

        }

        public Task<ServiceResponse<Freight>> Inactivate(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Freight>> Update(Freight freight)
        {
            ServiceResponse<Freight> serviceResponse = new ServiceResponse<Freight>();

            try
            {
                Freight? freight1 = await _context.Freights.AsNoTracking().FirstOrDefaultAsync(a => a.Id == freight.Id);

                if (freight1 == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Frete não encontrado";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    freight.ChangeDate = DateTime.Now.ToLocalTime();
                    if (freight.Cities != null && freight.Cities.Count > 0)
                    {
                        CityService cityService = new CityService(_context);
                        foreach (City city in freight.Cities)
                        {
                            var success = cityService.Create(city);
                            if (!success.Result.Sucess)
                            {
                                serviceResponse.Data = null;
                                serviceResponse.Message = "Erro ao cadastrar cidade";
                                serviceResponse.Sucess = false;
                            }
                        }
                    }
                    _context.Freights.Update(freight);
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
