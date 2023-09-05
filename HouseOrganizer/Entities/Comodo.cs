namespace HouseOrganizer.Entities
{
    public class Comodo : Entidade
    {
        public  string Nome { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
