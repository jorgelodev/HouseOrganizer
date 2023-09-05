using HouseOrganizer.Entities;

namespace HouseOrganizer.Repositories.Interfaces
{
    public interface IRepository<T> where T : Entidade
    {
        IList<T> ObterTodos();
        T ObterPorId(int id);
        void Cadastrar(T entidade);
        void Atualizar(T entidade);
        void Deletar(int id);
    }
}
