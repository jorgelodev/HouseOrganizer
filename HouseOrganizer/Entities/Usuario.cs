using HouseOrganizer.Entities.Enums;

namespace HouseOrganizer.Entities
{
    public class Usuario : Entidade
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public TipoPermissao Permissao { get; set; }
    }
}
