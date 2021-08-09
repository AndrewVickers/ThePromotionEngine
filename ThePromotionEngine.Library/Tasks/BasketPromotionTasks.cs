using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePromotionEngine.Library.Models;

namespace ThePromotionEngine.Library.Tasks
{
    public class BasketPromotionTasks
    {
        private readonly IPromotionTasks _promotionTasks;
        private PromotedBasketItem _promotedBasketItem;
        private IList<Product> _products;

        public BasketPromotionTasks(IPromotionTasks promotionTasks, IList<Product> products)
        {
            _promotionTasks = promotionTasks;
            _products = products;
        }

        public PromotedBasketItem CreatePromotedBasket(Basket basket)
        {
            var Id = 1;
            var promotion = _promotionTasks.GetNextPromotion(0);
            var priority = promotion.Priority;

            var basketCount = basket.GetBasket().Count;

            while (promotion != null)
            {
                _promotedBasketItem = new PromotedBasketItem { Id = Id++, Total = promotion.Total };
                foreach (var product in promotion.ItemPriceModfier)
                {
                    _promotedBasketItem.ProductList.Add(new PromotedBasketItem.PromotedProduct
                    {
                        Matched = false,
                        Modifier = product.Value,
                        Name = product.Key,
                        Price = _products.Single(x => x.SKU == product.Key).Price
                    });
                }

                if (MatchPromotionItemToBasketItem(_promotedBasketItem.ProductList, basket))
                {

                }
                //var currentProduct = basket.GetBasketForItem(product.Key);
                //    if (currentProduct != null)
                //    {
                //        _promotedBasketItem.ProductList.Add(new PromotedBasketItem.PromotedProduct
                //            {Name = product.Key, Modifier = product.Value});
                //    }
                //    else
                //    {

                //    }


                promotion = _promotionTasks.GetNextPromotion(++priority);
            }


            return _promotedBasketItem;
        }

        public bool MatchPromotionItemToBasketItem(IList<PromotedBasketItem.PromotedProduct> promotionItems,
            Basket basket)
        {
            var allMatched = false;
            var localPromotionItems = promotionItems;

            Dictionary<string, int> hasRequiredQuantity = new Dictionary<string, int>();

            var matchedBasket = new Basket();
            foreach (var promotedProduct in promotionItems)
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

            return allMatched;
        }
    }
}
