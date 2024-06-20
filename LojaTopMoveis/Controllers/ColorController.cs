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
    public class ColorController : ControllerBase
    {
        private readonly IColor _colorInterface;

        public ColorController(IColor colorInterface)
        {
            _colorInterface = colorInterface;
        }

        
        [HttpDelete("/cor/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Color>>>> Delete(Guid id)
        {
            return Ok(await _colorInterface.Remove(id));
        }
    }
    
    

}
