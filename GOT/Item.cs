namespace GOT
{
    public class Item
    {
        public Item(int id, int quantite)
        {
            Id = id;
            Quantite = quantite;
        }

        public int Id { get; set; }
        public int Quantite { get; set; }
    }
}