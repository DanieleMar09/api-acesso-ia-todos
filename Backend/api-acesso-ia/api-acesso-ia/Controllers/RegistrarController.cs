using api_acesso_ia.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_acesso_ia.Controllers
{
    public class RegistrarController
    {
        [HttpPost("registrar")]
        public async Task<ActionResult<Registrar>> Salvar(
                                            [FromBody] Registrar dados)
        {
            {

                var registrar = await _registrarService.RegistrarService(dados);
                return CreatedAtAction(nameof(Salvar), new { id = dados.Id }, dados);
            }
        }
    }
}
