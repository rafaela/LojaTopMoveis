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
        private readonly IProductInterface _productInterface;

        public ProductController(IProductInterface productInterface)
        {
            _productInterface = productInterface;
        }

        [HttpGet]
        [Route("/produtos")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            return Ok(await _productInterface.GetProducts());
        }

        [HttpPost("/produtos")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> CreateProducts(Product product)
        {
            return Ok(await _productInterface.CreateProducts(product));
        }

        /*[HttpGet("/produto")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductById(Guid id)
        {
            return Ok(await _productInterface.GetProductByID(id));
        }*/

        
        /*[HttpPut("/produtos/id")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> InactivateProduct(Guid id)
        {
            return Ok(await _productInterface.InactivateProduct(id));
        }*/

        [HttpPut("/produtos")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> UpdateProducts(Product product)
        {
            return Ok(await _productInterface.UpdateProduct(product));
        }

        [HttpDelete("/produtos")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> DeleteProducts(Guid id)
        {
            return Ok(await _productInterface.DeleteProduct(id));
        }
    }
    
    

}
