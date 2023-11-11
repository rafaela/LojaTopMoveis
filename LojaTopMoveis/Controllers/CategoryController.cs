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
    public class CategoryController : ControllerBase
    {
        private readonly ILoja<Category> _categoryInterface;

        public CategoryController(ILoja<Category> categoryInterface)
        {
            _categoryInterface = categoryInterface;
        }

        [HttpGet]
        [Route("/categorias")]
        public async Task<ActionResult<ServiceResponse<Category>>> GetCategories()
        {
            return Ok(await _categoryInterface.Get());
        }

        [HttpGet]
        [Route("/categorias/buscas")]
        public async Task<ActionResult<ServiceResponse<Category>>> GetCategoriesFilter(Category category)
        {
            return Ok(await _categoryInterface.GetFilter(category));
        }

        [HttpPost("/categorias/0")]
        public async Task<ActionResult<ServiceResponse<Category>>> CreateCategories(Category category)
        {
            return Ok(await _categoryInterface.Create(category));
        }

        [HttpGet("/categorias/{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetCategoryById(Guid id)
        {
            return Ok(await _categoryInterface.GetByID(id));
        }

        
        /*[HttpPut("/categorias/id")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> InactivateProduct(Guid id)
        {
            return Ok(await _categoryInterface.InactivateProduct(id));
        }*/

        [HttpPut("/categorias/{id}")]
        public async Task<ActionResult<ServiceResponse<Category>>> UpdateCategories(Category category)
        {
            return Ok(await _categoryInterface.Update(category));
        }

        [HttpDelete("/categorias/{id}")]
        public async Task<ActionResult<ServiceResponse<Category>>> DeleteCategories(Guid id)
        {
            return Ok(await _categoryInterface.Delete(id));
        }
    }
    
    

}
