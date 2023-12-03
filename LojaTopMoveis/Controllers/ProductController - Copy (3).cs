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
    public class ProductController : ControllerBase
    {
        private readonly ILoja<Product> _productInterface;

        public ProductController(ILoja<Product> productInterface)
        {
            _productInterface = productInterface;
        }

        [HttpGet]
        [Route("/produtos")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            return Ok(await _productInterface.Get());
        }

        [HttpPost("/produtos/0")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> CreateProducts(Product product)
        {
            return Ok(await _productInterface.Create(product));
        }


        [HttpGet("/produtos/{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetById(Guid id)
        {
            return Ok(await _productInterface.GetByID(id));
        }


        /*[HttpPut("/produtos/id")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> InactivateProduct(Guid id)
        {
            return Ok(await _productInterface.InactivateProduct(id));
        }*/

        [HttpPut("/produtos/{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> Update(Product product)
        {
            return Ok(await _productInterface.Update(product));
        }

        [HttpDelete("/produtos/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> DeleteProducts(Guid id)
        {
            return Ok(await _productInterface.Delete(id));
        }
    }
    
    

}
