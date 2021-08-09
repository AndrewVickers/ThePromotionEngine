using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePromotionEngine.Library
{
    public class Promotion
    {
        public int Priority { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int PrimaryItemId { get; set; }

        public Promotion GetHighestPriorityPromotionForItem(string item)
        {
            return null;
        }
    }
}
