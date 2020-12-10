using back_ifood.Interface.IBusiness;
using back_ifood.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ifood_back.Controllers
{
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoBusiness _produtoBusiness;

        public ProdutoController(IProdutoBusiness produtoBusiness)
        {
            _produtoBusiness = produtoBusiness;
        }

        [HttpGet("/[controller]/[action]")]
        public async Task<IActionResult> Lista()
        {
            return Ok(_produtoBusiness.Listar());
        }

        [HttpDelete("/[controller]/[action]/{Id}")]
        public async Task<IActionResult> Deleta(string Id)
        {
            return Ok(_produtoBusiness.Delete(Id));
        }

        [HttpPut("/[controller]/[action]")]
        public async Task<IActionResult> Salva([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_produtoBusiness.Salvar(produto));
        }
    }
}