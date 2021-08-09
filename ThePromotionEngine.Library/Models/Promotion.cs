using System.Collections.Generic;

namespace ThePromotionEngine.Library.Models
{
    public class Promotion
    {
        public int Priority { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<KeyValuePair<string, decimal>> ItemPriceModfier { get; set; }
        public int Total = 0;
    }
}
