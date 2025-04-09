using api_acesso_ia.Models;

namespace api_acesso_ia.Repositories.Interfaces
{
    public interface ILoginRepository
    {
 
        Task<LoginUsuario> Autenticar(string login, string senha);
        Task<LoginUsuario> Cadastrar(LoginUsuario dados);
        Task<bool> CpfJaCadastrado(string cpf);

        Task<bool> Resertar(LoginUsuario dados);
        Task Atualizar(object usuarioLogin);
        Task<LoginUsuario> BuscarPorEmail(string email);
        Task<LoginUsuario> BuscarPorId(int id);
        Task Atualizar(LoginUsuario dados);
    }
}
