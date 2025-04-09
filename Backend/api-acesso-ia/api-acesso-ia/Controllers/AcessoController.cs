using Microsoft.AspNetCore.Mvc;
using api_acesso_ia.Models;
using api_acesso_ia.Services.Interfaces;
using System.Threading.Tasks;

namespace api_acesso_ia.Controllers
{
    [Route("api/v1/acessos")]
    [ApiController]
    public class AcessoController : ControllerBase
    {
        private readonly IAessoService _acessoService;

        public AcessoController(IAessoService acessoService)
        {
            _acessoService = acessoService;
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> Listar()
        {
            var acessos = await _acessoService.ListarTodos();
            return Ok(acessos);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] Acesso dados)
        {
            var resultado = await _acessoService.Registrar(dados);
            if (!resultado)
                return BadRequest(new { msg = "Erro ao registrar acesso" });

            return Ok(new { msg = "Registro de acesso salvo com sucesso." });
        }

    

    }
}
