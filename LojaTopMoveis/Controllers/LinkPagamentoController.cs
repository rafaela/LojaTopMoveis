using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Topmoveis.Model;

namespace LojaTopMoveis.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LinkPagamentoController : Controller
    {

        private readonly ILinkPagamento _linkInterface;

        public LinkPagamentoController(ILinkPagamento linkPagamento)
        {
            _linkInterface = linkPagamento;
        }

        [HttpPost("/link-pagamento")]
        public async Task<ActionResult<ServiceResponse<LinkPagamento>>> Get(Guid id)
        {
            return Ok(await _linkInterface.geraLinkPagamento(id));
        }

    }
}
