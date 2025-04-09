using api_acesso_ia.Models;
using api_acesso_ia.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace api_acesso_ia.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _context;

        public LoginRepository(AppDbContext context)
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

        public async Task<bool> CpfJaCadastrado(string cpf)
        {
            return await _context.LoginUsuarios.AnyAsync(u => u.Cpf == cpf);
        }

        public async Task<LoginUsuario> BuscarPorEmail(string email)
        {
            return await _context.LoginUsuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task Atualizar(LoginUsuario dados)
        {
            _context.LoginUsuarios.Update(dados);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Resertar(LoginUsuario dados)
        {
            var usuarioExists = await _context.LoginUsuarios.FindAsync(dados.IdUsuario);
            if (usuarioExists == null)
            {
                return false;
            }

            usuarioExists.Login = dados.Login;
            usuarioExists.Senha = dados.Senha;
            usuarioExists.Email = dados.Email;
            usuarioExists.Cpf = dados.Cpf;
            usuarioExists.Nome = dados.Nome;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LoginUsuario> BuscarPorId(int id)
        {
            return await _context.LoginUsuarios.FindAsync(id);
        }

        
        public async Task Atualizar(object usuarioLogin)
        {
            if (usuarioLogin is LoginUsuario dados)
            {
                _context.LoginUsuarios.Update(dados);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("O tipo do usuário não é válido.");
            }
        }
    }
}
