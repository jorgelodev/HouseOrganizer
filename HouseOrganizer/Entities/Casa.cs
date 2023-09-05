namespace HouseOrganizer.Entities
{
    public class Casa : Entidade
    {
        public string Descricao { get; set; }

        public ICollection<Comodo> Comodos { get; set; }
    }
}
