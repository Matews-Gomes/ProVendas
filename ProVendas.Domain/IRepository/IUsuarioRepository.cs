using ProVendas.Domain.Models;

namespace ProVendas.Domain.IRepository
{
    public interface IUsuarioRepository : IGenericRepository<UsuarioModel>
    {
       bool Login(string usuario, string senha);
    }
}
