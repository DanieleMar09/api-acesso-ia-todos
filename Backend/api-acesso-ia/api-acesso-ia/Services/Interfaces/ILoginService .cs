using api_acesso_ia.Models;

namespace api_acesso_ia.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginUsuario> AutenticarService(string login, string senha);

        Task<LoginUsuario> CadastrarService(LoginUsuario dados);

        Task<bool> CpfJaCadastradoService(string cpf);

        string CriptografarSenha(string senha);

    }
}
