using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class AddressService : IAddress
    {
        private readonly LojaContext _context;

        public AddressService(LojaContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Address>> Create(Address address)
        {
            ServiceResponse<Address> serviceResponse = new ServiceResponse<Address>();

            try
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                
                serviceResponse.Data = address;
                serviceResponse.Message = "Endereço cadastrado";
                serviceResponse.Sucess = true;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Address>> Delete(Guid id)
        {
            ServiceResponse<Address> serviceResponse = new ServiceResponse<Address>();

            try
            {
                Address? address = await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

                if (address == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Endereço não encontrado";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Endereço removido";
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

        public async Task<ServiceResponse<Address>> GetByID(Guid id)
        {
            ServiceResponse<Address> serviceResponse = new ServiceResponse<Address>();
            try
            {
                Address? address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);

                if (address == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Endereço não encontrado";
                    serviceResponse.Sucess = false;
                }

                serviceResponse.Data = address;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Address>>> Get()
        {
            ServiceResponse<List<Address>> serviceResponse = new ServiceResponse<List<Address>>();

            try
            {
                serviceResponse.Data = await _context.Addresses.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<Address>>> Get(Guid id)
        {
            ServiceResponse<List<Address>> serviceResponse = new ServiceResponse<List<Address>>();

            try
            {
                serviceResponse.Data = await _context.Addresses.Where(a => a.ClientId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Address>> Update(Address address)
        {
            ServiceResponse<Address> serviceResponse = new ServiceResponse<Address>();

            try
            {
                Address? address1 = await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.Id == address.Id);

                

                if (address1 == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Endereço não encontrado";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    address.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Addresses.Update(address);
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
