using api_acesso_ia.Models;

namespace api_acesso_ia.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        //Listar todos Usuarios
        Task<IList<Usuario>> ListarTodos();

        //Buscar dados de um usuario
        Task<Usuario> BuscarPorId(int id);

        //Criar um usuario
        Task<Usuario> Criar(Usuario dados);

        //Editar um usuario
        Task<bool> Atualizar(Usuario dados);

        //Excluir Usuario
        Task<bool> Deletar(int id);

        //Valida se Já existe CPF cadastrado
        Task<bool> CpfJaCadastrado(string cpf);
    }
}
