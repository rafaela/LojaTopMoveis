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
    public class CityController : ControllerBase
    {
        private readonly ILoja<City> _cityInterface;

        public CityController(ILoja<City> cityInterface)
        {
            _cityInterface = cityInterface;
        }

        [HttpDelete("/removecidade/{id}")]
        public async Task<ActionResult<ServiceResponse<City>>> Delete(Guid id)
        {
            return Ok(await _cityInterface.Delete(id));
        }

    }
    
    

}
