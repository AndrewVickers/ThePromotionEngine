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
        Promotion GetNextPromotion();
    }

    public class PromotionTasks : IPromotionTasks
    {
        private IOrderedEnumerable<Promotion> _promotionList;
        private int _currentPriority = 0;

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

        public Promotion GetNextPromotion()
        {
            Promotion promotion = null;
            var maxPriority = _promotionList.Last().Priority;

            while (promotion == null && _currentPriority <= maxPriority)
            {
                promotion = _promotionList.FirstOrDefault(x => x.Priority == _currentPriority);
                _currentPriority++;
            }
            _currentPriority = 0;
            return promotion;
        }

        public bool IsProductInPromotion(string s, Promotion promotion1)
        {
            throw new System.NotImplementedException();
        }
    }
}
