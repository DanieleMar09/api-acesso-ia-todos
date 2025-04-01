using api_acesso_ia.Models;
using Microsoft.EntityFrameworkCore;

namespace api_acesso_ia.Repositories
{
    public class RegistrarRepository
    {
        public async Task<Registrar> Cadastrar(Registrar dados)
        {
            _context.Registrar.Add(dados);
            await _context.SaveChangesAsync();
            return dados;
        }
    }
}
