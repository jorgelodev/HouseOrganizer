using HouseOrganizer.Entities;
using HouseOrganizer.Repositories.Interfaces;

namespace HouseOrganizer.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public Usuario ObterPorNomeUsuarioESenha(string nomeUsuario, string senha)
        {
            return _context.Set<Usuario>().FirstOrDefault(u => u.NomeUsuario.Equals(nomeUsuario) && u.Senha.Equals(senha));
        }
    }
}
