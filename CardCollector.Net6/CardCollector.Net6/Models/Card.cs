namespace Collection.Models
{
    public class Card
    {
        public Card()
        {
        }

        public Card(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
