using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePromotionEngine.Library.Models;
using Xunit;

namespace ThePromotionEngine.UnitTests
{
    [Collection("Context collection")]
    public class PromotionsTestBase
    {
        public List<Promotion> PromotionList;
        public Promotion Promotion1;
        public Promotion Promotion2;
        public Promotion Promotion3;

        public int Promotion1Priority = 1;
        public int Promotion2Priority = 3;
        public int Promotion3Priority = 2;
        public int Promotion4Priority = 10;
        public int Promotion5Priority = 7;

        public PromotionsTestBase()
        {
            KeyValuePair<string, decimal>[] productModifiers = { new("A", 1), new("A", 1), new("A", 0.6M) };
            Promotion1 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "Multi A",
                Priority = Promotion1Priority
            };

            productModifiers = new KeyValuePair<string, decimal>[] { new("B", 1), new("B", 0.5M) };
            Promotion2 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "Buy One Get One Half Price",
                Priority = Promotion2Priority,

            };

            productModifiers = new KeyValuePair<string, decimal>[] { new("C", 1), new("D", 1) };
            Promotion3 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "MultiBuy",
                Priority = Promotion3Priority,
                Total = 30
            };

            PromotionList = new List<Promotion>
            {
                Promotion1, Promotion2, Promotion3
            };
        }

        public void AddExtraPromotions()
        {
            PromotionList.Add(new Promotion
            {
                Id = 4,
                ItemPriceModfier = new KeyValuePair<string, decimal>[] { },
                Name = "Should be last",
                Priority = Promotion4Priority,
                Total = 20
            });

            PromotionList.Add(new Promotion
            {
                Id = 5,
                ItemPriceModfier = new KeyValuePair<string, decimal>[] { },
                Name = "Should be 4 in list",
                Priority = Promotion5Priority,
                Total = 40
            });
        }
    }
}
