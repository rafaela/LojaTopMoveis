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
    public class HighlightController : ControllerBase
    {
        private readonly ILoja<Highlight> _highlightInterface;

        public HighlightController(ILoja<Highlight> highlightInterface)
        {
            _highlightInterface = highlightInterface;
        }

        [HttpPost]
        [Route("/imagensdestaque")]
        public async Task<ActionResult<ServiceResponse<Highlight>>> Get(ServiceParameter<Highlight> sp)
        {
            return Ok(await _highlightInterface.Get(sp));
        }

        
        [HttpPost("/imagensdestaque/0")]
        public async Task<ActionResult<ServiceResponse<Highlight>>> Create(Highlight category)
        {
            return Ok(await _highlightInterface.Create(category));
        }

        [HttpGet("/imagensdestaque/{id}")]
        public async Task<ActionResult<ServiceResponse<Highlight>>> GetById(Guid id)
        {
            return Ok(await _highlightInterface.GetByID(id));
        }

        [HttpPut("/imagensdestaque/{id}")]
        public async Task<ActionResult<ServiceResponse<Highlight>>> Update(Highlight category)
        {
            return Ok(await _highlightInterface.Update(category));
        }

        [HttpDelete("/imagensdestaque/{id}")]
        public async Task<ActionResult<ServiceResponse<Highlight>>> Delete(Guid id)
        {
            return Ok(await _highlightInterface.Delete(id));
        }
    }
    
    

}
