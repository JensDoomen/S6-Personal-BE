using System.ComponentModel.DataAnnotations;


namespace CardCollector.Net6.Database.Models
{
    public class Card
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int ManaCost { get; set; }
        public string Element { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public string Rarity { get; set; }
        public string Picture { get; set; }
    }
}
