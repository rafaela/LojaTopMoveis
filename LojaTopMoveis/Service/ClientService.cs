using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class ClientService : ILoja<Client>
    {
        private readonly LojaContext _context;

        public ClientService(LojaContext context)
        {
            _context = context;
        }

        public string QuickHash(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var inputHash = MD5.HashData(inputBytes);
            return Convert.ToHexString(inputHash);
        }

        public async Task<ServiceResponse<Client>> Create(Client client)
        {
            ServiceResponse<Client> serviceResponse = new ServiceResponse<Client>();

            try
            {
                var searchEmail = _context.Clients.Where(a => a.Email == client.Email).FirstOrDefault();
                if (searchEmail != null)
                {
                    serviceResponse.Message = "E-mail já cadastrado";
                    serviceResponse.Sucess = false;
                    return serviceResponse;
                }
                var searchCPF = _context.Clients.Where(a => a.CPF == client.CPF).FirstOrDefault();
                if (searchCPF != null)
                {
                    serviceResponse.Message = "Cliente já cadastrado";
                    serviceResponse.Sucess = false;
                    return serviceResponse;
                }

                client.Login!.PasswordHash = QuickHash(client.Login.PasswordHash).ToLower();

                _context.Add(client);
                await _context.SaveChangesAsync();
                
                serviceResponse.Data = client;
                serviceResponse.Sucess = true;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public Task<ServiceResponse<Client>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Client>> GetByID(Guid id)
        {
            ServiceResponse<Client> serviceResponse = new ServiceResponse<Client>();
            try
            {
                Client? client = await _context.Clients.Include(a => a.Login).FirstOrDefaultAsync(a => a.Id == id);

                if (client == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria não encontrada";
                    serviceResponse.Sucess = false;

                    return serviceResponse;
                }

                serviceResponse.Data = client;
                serviceResponse.Sucess = true;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Client>>> Get(ServiceParameter<Client> sp)
        {
            ServiceResponse<List<Client>> serviceResponse = new ServiceResponse<List<Client>>();

            try
            {
                serviceResponse.Data = await _context.Clients.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

        public Task<ServiceResponse<List<Client>>> GetFilter(Client client)
        {
            throw new NotImplementedException();

        }

        public Task<ServiceResponse<Client>> Inactivate(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Client>> Update(Client client)
        {
            ServiceResponse<Client> serviceResponse = new ServiceResponse<Client>();

            try
            {
                Client? client1 = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(a => a.Id == client.Id);

                

                if (client1 == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Cliente não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    client.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Clients.Update(client);
                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Cliente atualizado";
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
