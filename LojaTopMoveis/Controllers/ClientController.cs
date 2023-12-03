using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Model;

namespace LojaTopMoveis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILoja<Client> _clientInterface;

        public ClientController(ILoja<Client> categoryInterface)
        {
            _clientInterface = categoryInterface;
        }

        [HttpGet]
        [Route("/clientes")]
        public async Task<ActionResult<ServiceResponse<Client>>> Get()
        {
            return Ok(await _clientInterface.Get());
        }

        /*[HttpGet]
        [Route("/categorias/buscas")]
        public async Task<ActionResult<ServiceResponse<Client>>> GetCategoriesFilter(Category category)
        {
            return Ok(await _clientInterface.GetFilter(category));
        }*/

        [HttpPost("/clientes/0")]
        public async Task<ActionResult<ServiceResponse<Client>>> Create(Client client)
        {
            return Ok(await _clientInterface.Create(client));
        }

        [HttpGet("/clientes/{id}")]
        public async Task<ActionResult<ServiceResponse<Client>>> GetCategoryById(Guid id)
        {
            return Ok(await _clientInterface.GetByID(id));
        }

        
        /*[HttpPut("/categorias/id")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> InactivateProduct(Guid id)
        {
            return Ok(await _clientInterface.InactivateProduct(id));
        }*/

        [HttpPut("/clientes/{id}")]
        public async Task<ActionResult<ServiceResponse<Client>>> Update(Client client)
        {
            return Ok(await _clientInterface.Update(client));
        }

        /*[HttpDelete("/clientes/{id}")]
        public async Task<ActionResult<ServiceResponse<Client>>> Delete(Guid id)
        {
            return Ok(await _clientInterface.Delete(id));
        }*/
    }
    
    

}
