using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePromotionEngine.Library.Models;

namespace ThePromotionEngine.Library.Tasks
{
    public class PromotionTasks
    {
        private Promotion[] _promotionList;

        public PromotionTasks(Promotion[] promotionList)
        {
            _promotionList = promotionList;
        }

        public void AddPromotion(Promotion promotion)
        {
            _promotionList.Append(promotion);
        }

        public Promotion GetPromotionWithPriority(int priority)
        {
            return null;
        }

        public object GetAllPromotions()
        {
            throw new NotImplementedException();
        }
    }
}
