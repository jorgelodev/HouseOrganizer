using HouseOrganizer.Entities;
using HouseOrganizer.Repositories.Interfaces;

namespace HouseOrganizer.Repositories
{
    public class CasaRepository : RepositoryBase<Casa>, ICasaRepository
    {
        public CasaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
