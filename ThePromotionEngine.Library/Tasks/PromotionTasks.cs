using System.Collections.Generic;
using System.Linq;
using ThePromotionEngine.Library.Models;

namespace ThePromotionEngine.Library.Tasks
{
    public interface IPromotionTasks
    {
        void AddPromotion(Promotion promotion);
        IOrderedEnumerable<Promotion> GetAllPromotions();
        Promotion GetPromotionByPriority(int priority);
        Promotion GetNextPromotion(int priority);
        bool IsProductInPromotion(string key, Promotion promotion);
    }

    public class PromotionTasks : IPromotionTasks
    {
        private IOrderedEnumerable<Promotion> _promotionList;

        public PromotionTasks(IList<Promotion> promotionList)
        {
            _promotionList = promotionList.OrderBy(p => p.Priority);
        }

        public void AddPromotion(Promotion promotion)
        {
            _promotionList = _promotionList.Append(promotion).OrderBy(p => p.Priority);
        }

        public IOrderedEnumerable<Promotion> GetAllPromotions()
        {
            return _promotionList;
        }

        public Promotion GetPromotionByPriority(int priority)
        {

            return _promotionList.FirstOrDefault(x => x.Priority == priority);
        }

        public Promotion GetNextPromotion(int priority)
        {
            Promotion promotion = null;
            var maxPriority = _promotionList.Last().Priority;

            while (promotion == null && priority <= maxPriority)
            {
                promotion = _promotionList.FirstOrDefault(x => x.Priority == priority);
                priority++;
            }
            return promotion;
        }

        public bool IsProductInPromotion(string key, Promotion promotion)
        {
            foreach (var dictionary in promotion.ItemPriceModfier)
            {
                if (dictionary.Key == key) return true;
            }

            return false;
        }
    }
}
