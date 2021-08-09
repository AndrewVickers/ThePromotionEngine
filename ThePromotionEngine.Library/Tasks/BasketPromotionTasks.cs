using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePromotionEngine.Library.Models;

namespace ThePromotionEngine.Library.Tasks
{
    public interface IBasketPromotionTasks
    {
        IList<PromotedBasketItem> CreatePromotedBasketItems(Basket basket);
        decimal CalculateTotal(IList<PromotedBasketItem.PromotedProduct> promotedProductList);

        bool MatchPromotionItemToBasketItem(PromotedBasketItem promotedBasketItem,
            Basket basket);
    }

    public class BasketPromotionTasks : IBasketPromotionTasks
    {
        private readonly IPromotionTasks _promotionTasks;
        private IList<Product> _products;

        public BasketPromotionTasks(IPromotionTasks promotionTasks, IList<Product> products)
        {
            _promotionTasks = promotionTasks;
            _products = products;
        }

        public IList<PromotedBasketItem> CreatePromotedBasketItems(Basket basket)
        {
            var Id = 1;
            var promotion = _promotionTasks.GetNextPromotion(0);
            var priority = promotion.Priority;

            var basketCount = basket.GetBasket().Count;
            IList<PromotedBasketItem> _promotedBasketItemList = new List<PromotedBasketItem>();

            while (promotion != null)
            {
                var promotedBasketItem = new PromotedBasketItem { Id = Id++, Total = promotion.Total };
                foreach (var product in promotion.ItemPriceModfier)
                {
                    promotedBasketItem.ProductList.Add(new PromotedBasketItem.PromotedProduct
                    {
                        Matched = false,
                        Modifier = product.Value,
                        Name = product.Key,
                        Price = _products.Single(x => x.SKU == product.Key).Price
                    });
                }

                while (MatchPromotionItemToBasketItem(promotedBasketItem, basket))
                {
                    promotedBasketItem.Total = promotion.Total == 0 ? CalculateTotal(promotedBasketItem.ProductList) : promotion.Total;
                    _promotedBasketItemList.Add(promotedBasketItem);
                }

                promotion = _promotionTasks.GetNextPromotion(++priority);
            }

            foreach (var item in basket.GetBasket())
            {
                var product = _products.Single(x => x.SKU == item.Key);
                var basicBasketItem = new PromotedBasketItem { Id = Id++, Total = item.Value * product.Price };

                for (int i = 0; i < item.Value; i++)
                {
                    basicBasketItem.ProductList.Add(new PromotedBasketItem.PromotedProduct
                    { Matched = false, Modifier = 1, Name = item.Key, Price = product.Price });
                }

                _promotedBasketItemList.Add(basicBasketItem);
            }


            return _promotedBasketItemList;
        }

        public decimal CalculateTotal(IList<PromotedBasketItem.PromotedProduct> promotedProductList)
        {
            decimal total = 0;
            foreach (var promotedProduct in promotedProductList)
            {
                total += promotedProduct.Price * promotedProduct.Modifier;
            }

            return total;
        }

        public bool MatchPromotionItemToBasketItem(PromotedBasketItem promotedBasketItem,
            Basket basket)
        {
            Dictionary<string, int> hasRequiredQuantity = new Dictionary<string, int>();

            var matchedBasket = new Basket();
            foreach (var promotedProduct in promotedBasketItem.ProductList)
            {
                matchedBasket.AddProductToBasket(promotedProduct.Name);
            }

            foreach (var promotionItem in matchedBasket.GetBasket())
            {
                if (basket.GetBasketForItem(promotionItem.Key).Value < promotionItem.Value)
                {
                    return false;
                }
            }

            foreach (var product in matchedBasket.GetBasket())
            {
                basket.UpdateQuantity(product.Key, -product.Value);
            }

            return true;
        }
    }
}
