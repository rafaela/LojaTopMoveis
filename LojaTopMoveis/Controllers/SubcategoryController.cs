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
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategory _subcategoryInterface;

        public SubcategoryController(ISubcategory subcategoryInterface)
        {
            _subcategoryInterface = subcategoryInterface;
        }

        
        [HttpGet("/subcategorias/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Subcategory>>>> Search(Guid id)
        {
            return Ok(await _subcategoryInterface.SearchSubcategories(id));
        }

        [HttpDelete("/subcategorias/{id}")]
        public async Task<ActionResult<ServiceResponse<Subcategory>>> Remove(Guid id)
        {
            return Ok(await _subcategoryInterface.Remove(id));
        }
    }
    
    

}
