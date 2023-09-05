using HouseOrganizer.Entities;
using HouseOrganizer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HouseOrganizer.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : Entidade
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Atualizar(T entidade)
        {
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            _dbSet.Remove(ObterPorId(id));
            _context.SaveChanges();
        }

        public T ObterPorId(int id)
        {
            return _dbSet.FirstOrDefault(p => p.Id == id);
        }

        public IList<T> ObterTodos()
        {
            return _dbSet.ToList();
        }
    }
}
