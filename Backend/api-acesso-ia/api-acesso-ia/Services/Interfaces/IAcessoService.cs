using api_acesso_ia.Models;
using api_acesso_ia.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using api_acesso_ia.Request;

namespace api_acesso_ia.Services.Interfaces
{
    public interface IAcessoService
    {
        Task BuscarPorEmail(string email);
        Task BuscarPorEmailService(string email);
        Task<IEnumerable<AcessoResponse>> ListarTodos();
        Task<bool> Registrar(int IdUsuario, DateTime DataHoraAcesso);
    }
} 