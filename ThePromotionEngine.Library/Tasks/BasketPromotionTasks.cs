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

        public BasketPromotionTasks(IPromotionTasks promotionTasks)
        {
            _promotionTasks = promotionTasks;
        }

        public PromotedBasketItem CreatePromotedBasket(Basket basket)
        {

            var promotion = _promotionTasks.GetNextPromotion(0);
            var priority = promotion.Priority;
            _promotedBasketItem = new PromotedBasketItem();

            var basketCount = basket.GetBasket().Count;

            while (promotion != null)
            {
                if (_promotionTasks.IsProductInPromotion())
                    foreach (var product in promotion.ItemPriceModfier)
                    {
                        var currentProduct = basket.GetBasketForItem(product.Key);
                        if (currentProduct != null)
                        {
                            _promotedBasketItem.ProductList.Add(new PromotedBasketItem.PromotedProduct
                                { Name = product.Key, Modifier = product.Value });
                        }
                        else
                        {

                        }
                    }

                promotion = _promotionTasks.GetNextPromotion(++priority);
            }

            return _promotedBasketItem;
        }
    }
}
