using Loja.Model;
using LojaTopMoveis.Model;
using LojaTopMoveis.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Model;

namespace LojaTopMoveis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly ILoja<Photo> _productInterface;

        public PhotosController(ILoja<Photo> productInterface)
        {
            _productInterface = productInterface;
        }

        [HttpGet]
        [Route("/produtos")]
        public async Task<ActionResult<ServiceResponse<List<Photo>>>> GetProducts()
        {
            return Ok(await _productInterface.Get());
        }

        [HttpPost("/produtos/0")]
        public async Task<ActionResult<ServiceResponse<List<Photo>>>> CreateProducts(Photo product)
        {
            return Ok(await _productInterface.Create(product));
        }


        [HttpGet("/produtos/{id}")]
        public async Task<ActionResult<ServiceResponse<Photo>>> GetById(Guid id)
        {
            return Ok(await _productInterface.GetByID(id));
        }


        /*[HttpPut("/produtos/id")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> InactivateProduct(Guid id)
        {
            return Ok(await _productInterface.InactivateProduct(id));
        }*/

        [HttpPut("/produtos/{id}")]
        public async Task<ActionResult<ServiceResponse<Photo>>> Update(Photo product)
        {
            return Ok(await _productInterface.Update(product));
        }

        [HttpDelete("/produtos/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Photo>>>> DeleteProducts(Guid id)
        {
            return Ok(await _productInterface.Delete(id));
        }
    }
    
    

}
