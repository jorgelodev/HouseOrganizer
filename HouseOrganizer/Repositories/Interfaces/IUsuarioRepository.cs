using HouseOrganizer.Entities;

namespace HouseOrganizer.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
       Usuario ObterPorNomeUsuarioESenha(string momeUsuario, string senha);
    }
}
