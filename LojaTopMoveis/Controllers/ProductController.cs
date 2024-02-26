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
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productInterface;

        public ProductController(IProduct productInterface)
        {
            _productInterface = productInterface;
        }

        [HttpPost]
        [Route("/produtos")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts(ServiceParameter<Product> sp)
        {
            return Ok(await _productInterface.Get(sp));
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

        [Route("/produtosdestaque")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeatured()
        {
            return Ok(await _productInterface.GetFeatured());
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

        [HttpGet]
        [Route("/categoriaproduto/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetByCategory(Guid id)
        {
            return Ok(await _productInterface.GetByCategory(id));
        }

        [HttpGet]
        [Route("/subcategoriaproduto/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetBySubcategory(Guid id)
        {
            return Ok(await _productInterface.GetBySubcategory(id));
        }

    }
    
    

}
