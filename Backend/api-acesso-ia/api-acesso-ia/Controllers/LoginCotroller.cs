using api_acesso_ia.Models;
using api_acesso_ia.Request;
using api_acesso_ia.Services;
using api_acesso_ia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_acesso_ia.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginCotroller : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginCotroller(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] LoginRequest dados)
        {
            var usuario = await _loginService.AutenticarService(dados.Login, dados.Senha);
            if (usuario == null)
            {
                return Unauthorized(new { msg = "Login ou Senha Invalidos" });
            }
            return Ok(new { Nome = usuario.Nome, Email = usuario.Email, Login = usuario.Login });
        }


        [HttpPost("cadastrar")]
        public async Task<ActionResult<LoginUsuario>> Salvar(
                                            [FromBody] LoginUsuario dados)
        {
            {
                if (await _loginService.CpfJaCadastradoService(dados.Cpf))
                {
                    throw new Exception("O CPF informado já possui cadastro.");
                }
dados.Senha = _loginService.CriptografarSenha(dados.Senha); 
                                 
                var usuario = await _loginService.CadastrarService(dados);
                return CreatedAtAction(nameof(Salvar), new { id = dados.Id }, dados);
            }
        }


        [HttpGet("buscar-email")]
        public async Task<IActionResult> BuscarPorEmail([FromQuery] string email)
        {
            var usuario = await _loginService.BuscarPorEmailService(email);
            if (usuario == null)
                return NotFound("Usuário não encontrado");

            return Ok(usuario);
        }

        [HttpPut("resetar-senha/{idUsuario}")]
        public async Task<IActionResult> ResetarSenha(int idUsuario, [FromBody] ResertarSenhaRequest dados)
        {
            var sucesso = await _loginService.ResetarSenhaService(idUsuario, dados.novaSenha);

            if (!sucesso)
                return NotFound("Usuário não encontrado");

            return Ok("Senha redefinida com sucesso");
        }


    }
}

