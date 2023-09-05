using System.ComponentModel;

namespace HouseOrganizer.Entities.Enums
{

    public enum TipoPermissao
    {
        [Description("Administrador")]
        Administrador = 1,
        [Description("Dono")]
        Dono = 2 
    }

    public static class Permissoes
    {
        public const string Administrador = "Administrador";
        public const string Dono = "Dono";
    }

}
