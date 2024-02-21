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
    public class PaymentController : ControllerBase
    {
        private readonly ILoja<Payment> _paymentInterface;

        public PaymentController(ILoja<Payment> paymentInterface)
        {
            _paymentInterface = paymentInterface;
        }

        [HttpPost]
        [Route("/pagamentos")]
        public async Task<ActionResult<ServiceResponse<Payment>>> Get(ServiceParameter<Payment> sp)
        {
            return Ok(await _paymentInterface.Get(sp));
        }

        
        [HttpPost("/pagamentos/0")]
        public async Task<ActionResult<ServiceResponse<Payment>>> Create(Payment payment)
        {
            return Ok(await _paymentInterface.Create(payment));
        }

        [HttpGet("/pagamentos/{id}")]
        public async Task<ActionResult<ServiceResponse<Payment>>> GetById(Guid id)
        {
            return Ok(await _paymentInterface.GetByID(id));
        }

        [HttpPut("/pagamentos/{id}")]
        public async Task<ActionResult<ServiceResponse<Payment>>> Update(Payment payment)
        {
            return Ok(await _paymentInterface.Update(payment));
        }

        [HttpDelete("/pagamentos/{id}")]
        public async Task<ActionResult<ServiceResponse<Payment>>> Delete(Guid id)
        {
            return Ok(await _paymentInterface.Delete(id));
        }
    }
    
    

}
