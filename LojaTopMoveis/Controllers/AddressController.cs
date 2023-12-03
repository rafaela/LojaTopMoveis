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
    public class AddressController : ControllerBase
    {
        private readonly IAddress _addressInterface;

        public AddressController(IAddress categoryInterface)
        {
            _addressInterface = categoryInterface;
        }

        [HttpGet]
        [Route("/enderecoscliente/{id}")]
        public async Task<ActionResult<ServiceResponse<Address>>> Get(Guid id)
        {
            return Ok(await _addressInterface.Get(id));
        }

        /*[HttpGet]
        [Route("/endererecos/buscas")]
        public async Task<ActionResult<ServiceResponse<Address>>> GetCategoriesFilter(Category category)
        {
            return Ok(await _addressInterface.GetFilter(category));
        }*/

        [HttpPost("/enderecos/0")]
        public async Task<ActionResult<ServiceResponse<Address>>> Create(Address address)
        {
            return Ok(await _addressInterface.Create(address));
        }

        [HttpGet("/enderecos/{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetById(Guid id)
        {
            return Ok(await _addressInterface.GetByID(id));
        }

        
        /*[HttpPut("/categorias/id")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> InactivateProduct(Guid id)
        {
            return Ok(await _addressInterface.InactivateProduct(id));
        }*/

        [HttpPut("/enderecos/{id}")]
        public async Task<ActionResult<ServiceResponse<Address>>> Update(Address address)
        {
            return Ok(await _addressInterface.Update(address));
        }

        [HttpDelete("/enderecos/{id}")]
        public async Task<ActionResult<ServiceResponse<Address>>> Delete(Guid id)
        {
            return Ok(await _addressInterface.Delete(id));
        }
    }
    
    

}
