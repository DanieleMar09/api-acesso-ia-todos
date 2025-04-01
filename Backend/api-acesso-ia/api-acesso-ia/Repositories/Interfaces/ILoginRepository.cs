using api_acesso_ia.Models;

namespace api_acesso_ia.Repositories.Interfaces
{
    public interface ILoginRepository
    {
 
 
        Task<LoginUsuario> Autenticar(string login, string senha);

     
        Task<LoginUsuario> Cadastrar(LoginUsuario dados);

        Task<bool> CpfJaCadastrado(string cpf);

    }
}
