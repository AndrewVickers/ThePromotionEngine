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

        public BasketPromotionTasks(IPromotionTasks promotionTasks, PromotedBasket promotedBasket)
        {
            _promotionTasks = promotionTasks;
            _promotedBasket = promotedBasket;
        }

        public PromotedBasket CreatePromotedBasket(Basket basket)
        {
            var promotion = _promotionTasks.GetNextPromotion();
            while (promotion != null)
            {

                foreach (var product in basket.GetBasket())
                {
                }

                promotion = _promotionTasks.GetNextPromotion();
            }

            return _promotedBasket;
        }
    }
}
