using HouseOrganizer.Entities;

namespace HouseOrganizer.Services
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}
