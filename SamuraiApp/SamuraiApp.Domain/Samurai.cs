using System.Collections.Generic;

namespace SamuraiApp.Domain
{
    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        //public int BattleId { get; set; }
        public List<Samurai> SamuraiBattle { get; set; }
        public SecretIdentity SecretIdentity { get; set; }

        public Samurai()
        {
            Quotes = new List<Quote>();
        }
    }
}
