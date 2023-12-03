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
    public class PhotoController : ControllerBase
    {
        private readonly IPhoto _photoInterface;

        public PhotoController(IPhoto photoInterface)
        {
            _photoInterface = photoInterface;
        }

        
        [HttpDelete("/imagem/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Photo>>>> Delete(Guid id)
        {
            return Ok(await _photoInterface.Remove(id));
        }
    }
    
    

}
