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
        private PromotedBasket _promotedBasket;

        public BasketPromotionTasks(IPromotionTasks promotionTasks)
        {
            _promotionTasks = promotionTasks;
        }

        public PromotedBasket CreatePromotedBasket(Basket basket)
        {
            var promotion = _promotionTasks.GetNextPromotion();
            _promotedBasket = new PromotedBasket();

            while (promotion != null)
            {
                foreach (var product in promotion.ItemPriceModfier)
                {
                    var currentProduct = basket.GetBasketForItem(product.Key);
                    if (currentProduct != null)
                    {
                        _promotedBasket.ProductList.Add(new PromotedBasket.PromotedProduct
                            { Name = product.Key, Modifier = product.Value });
                    }
                    else
                    {

                    }
                }

                promotion = _promotionTasks.GetNextPromotion();
            }

            return _promotedBasket;
        }
    }
}
