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
        private readonly ISale _saleInterface;

        public SaleController(ISale saleInterface)
        {
            _saleInterface = saleInterface;
        }

        [HttpPost]
        [Route("/vendas")]
        public async Task<ActionResult<ServiceResponse<List<Sale>>>> Get(ServiceParameter<Sale> sp)
        {
            return Ok(await _saleInterface.Get(sp));
        }

        [HttpPost("/vendas/0")]
        public async Task<ActionResult<ServiceResponse<Sale>>> Create(Sale sale)
        {
            return Ok(await _saleInterface.Create(sale));
        }


        [HttpGet]
        [Route("/vendas/{id}")]
        public async Task<ActionResult<ServiceResponse<Sale>>> GetById(Guid id)
        {
            return Ok(await _saleInterface.GetByID(id));
        }

        /*[HttpPut("/produtos/id")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> InactivateProduct(Guid id)
        {
            return Ok(await _productInterface.InactivateProduct(id));
        }*/

        [HttpPut]
        [Route("/vendas/{id}")]
        public async Task<ActionResult<ServiceResponse<Sale>>> Update(Sale sale)
        {
            return Ok(await _saleInterface.Update(sale));
        }

        [HttpDelete]
        [Route("/vendas/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Sale>>>> Delete(Guid id)
        {
            return Ok(await _saleInterface.Delete(id));
        }

        [HttpPost]
        [Route("/mudastatus/0")]
        public async Task<ActionResult<ServiceResponse<List<Sale>>>> change(Sale sale)
        {
            return Ok(await _saleInterface.ChangeStatusSale(sale));
        }

        [HttpGet]
        [Route("/meuspedidos/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Sale>>>> GetDataSale(Guid id)
        {
            return Ok(await _saleInterface.GetDataSale(id));
        }



    }
    
    

}
