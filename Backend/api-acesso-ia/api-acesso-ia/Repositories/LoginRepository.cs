using api_acesso_ia.Models;
using api_acesso_ia.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_acesso_ia.Repositories
{
 
    public class LoginRepository : ILoginRepository
    {
   
        private readonly AppDbContext _context;

        public LoginRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoginUsuario> Autenticar(string login, string senha)
        {
            return await _context.LoginUsuarios
                .Where(u => u.Login == login && u.Senha == senha)
                .FirstOrDefaultAsync();
        }
        public async Task<LoginUsuario> Cadastrar(LoginUsuario dados)
        {
            _context.LoginUsuarios.Add(dados);
            await _context.SaveChangesAsync();
            return dados;
        }

        public async Task<bool>CpfJaCadastrado(string cpf)
        {
            return await _context.LoginUsuarios.AnyAsync(u => u.Cpf == cpf);

        }
 
    }
}

