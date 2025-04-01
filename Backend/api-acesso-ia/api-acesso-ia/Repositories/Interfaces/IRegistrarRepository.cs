using api_acesso_ia.Models;

namespace api_acesso_ia.Repositories.Interfaces
{
    public class IRegistrarRepository
    {
        Task<Registrar> Registrar(Registrar dados);
    }
}
