using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePromotionEngine.Library
{
    public class Product
    {
        public string SKU => _sku;
        public int Price => _price;
        private readonly string _sku;
        private readonly int _price;
        public Product(int price, string sku)
        {
            _price = price;
            _sku = sku;
        }
    }
}
