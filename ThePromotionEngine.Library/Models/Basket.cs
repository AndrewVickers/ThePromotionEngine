using System.Collections.Generic;
using System.Linq;

namespace ThePromotionEngine.Library.Models
{
    public interface IBasket
    {
        void AddProductToBasket(string name);
        Dictionary<string, int> GetBasket();
        KeyValuePair<string, int> GetBasketForItem(string key);
    }

    public class Basket : IBasket
    {
        private Dictionary<string, int> basket { get; set; }

        public Basket()
        {
            basket = new Dictionary<string, int>();
        }

        public void AddProductToBasket(string name)
        {
            if (basket.ContainsKey(name))
            {
                basket[name] += 1;
            }
            else
            {
                basket.Add(name, 1);
            }
        }

        public Dictionary<string, int> GetBasket()
        {
            return basket;
        }

        public KeyValuePair<string, int> GetBasketForItem(string key)
        {
            return basket.SingleOrDefault(x => x.Key == key);
        }

        public void UpdateQuantity(string key, int quantity)
        {
            basket[key] += quantity;
        }
    }
}
