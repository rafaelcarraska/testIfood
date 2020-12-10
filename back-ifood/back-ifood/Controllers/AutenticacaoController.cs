using ifood_back.Dto;
using ifood_back.Interface.IBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ifood_back.Controllers
{
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoBusiness _autenticacaoBusiness;

        public AutenticacaoController(IAutenticacaoBusiness autenticacaoBusiness)
        {
            _autenticacaoBusiness = autenticacaoBusiness;
        }

        [AllowAnonymous]
        [HttpPut("/[controller]/[action]")]
        public async Task<IActionResult> Autentica([FromBody] AutenticacaoDto autenticacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_autenticacaoBusiness.validaLogin(autenticacao));
        }

        [AllowAnonymous]
        [HttpGet("/[controller]/[action]")]
        public async Task<IActionResult> Logoff()
        {
            HttpContext.Session.Clear();
            return Ok("Logoff");
        }
    }
}