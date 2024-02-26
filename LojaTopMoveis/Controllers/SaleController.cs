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
    public class SaleController : ControllerBase
    {
        private readonly ILoja<Sale> _saleInterface;

        public SaleController(ILoja<Sale> categoryInterface)
        {
            _saleInterface = categoryInterface;
        }

        [HttpPost]
        [Route("/vendas")]
        public async Task<ActionResult<ServiceResponse<Sale>>> Get(ServiceParameter<Sale> sp)
        {
            return Ok(await _saleInterface.Get(sp));
        }

        
        [HttpPost("/vendas/0")]
        public async Task<ActionResult<ServiceResponse<Sale>>> Create(Sale sale)
        {
            return Ok(await _saleInterface.Create(sale));
        }

        [HttpGet("/vendas/{id}")]
        public async Task<ActionResult<ServiceResponse<Sale>>> GetById(Guid id)
        {
            return Ok(await _saleInterface.GetByID(id));
        }

        
        /*[HttpPut("/categorias/id")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> InactivateProduct(Guid id)
        {
            return Ok(await _saleInterface.InactivateProduct(id));
        }*/

        [HttpPut("/vendas/{id}")]
        public async Task<ActionResult<ServiceResponse<Sale>>> Update(Sale sale)
        {
            return Ok(await _saleInterface.Update(sale));
        }

        [HttpDelete("/vendas/{id}")]
        public async Task<ActionResult<ServiceResponse<Sale>>> Delete(Guid id)
        {
            return Ok(await _saleInterface.Delete(id));
        }
    }
    
    

}
