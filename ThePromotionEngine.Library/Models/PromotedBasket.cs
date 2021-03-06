using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePromotionEngine.Library.Models
{
    public class PromotedBasket
    {
        public struct PromotedProduct
        {
            public string Name;
            public Decimal Modifier;
            public int Price;
        }

        public int Id { get; set; }
        public IList<PromotedProduct> ProductList { get; set; }
        public decimal Total;


    }
}
