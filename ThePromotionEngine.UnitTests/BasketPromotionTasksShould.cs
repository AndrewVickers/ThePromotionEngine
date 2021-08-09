using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePromotionEngine.Library.Models;
using ThePromotionEngine.Library.Tasks;
using Xunit;

namespace ThePromotionEngine.UnitTests
{
    public class BasketPromotionTasksShould
    {
        private BasketPromotionTasks _sut;
        private Promotion[] _promotionList;

        public BasketPromotionTasksShould()
        {
            _sut = new BasketPromotionTasks();
        }

        [Fact]
        public void ApplyPromotionIfBasketContentsExactlyMatchPromotionRequirements()
        {

        }
    }
}
