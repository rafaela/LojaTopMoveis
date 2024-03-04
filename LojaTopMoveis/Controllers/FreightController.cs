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
    public class FreightController : ControllerBase
    {
        private readonly ILoja<Freight> _freightInterface;

        public FreightController(ILoja<Freight> categoryInterface)
        {
            _freightInterface = categoryInterface;
        }

        

        [HttpPost("/frete")]
        public async Task<ActionResult<ServiceResponse<Category>>> Create(Freight freight)
        {
            return Ok(await _freightInterface.Create(freight));
        }

        [HttpGet("/frete/0")]
        public async Task<ActionResult<ServiceResponse<Freight>>> Get(Guid id)
        {
            return Ok(await _freightInterface.GetByID(id));
        }

        
        

        [HttpPut("/frete/{id}")]
        public async Task<ActionResult<ServiceResponse<Category>>> UpdateCategories(Freight freight)
        {
            return Ok(await _freightInterface.Update(freight));
        }
    }
    
    

}
