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
    public class BasketPromotionTasksShould : TestBase
    {
        private BasketPromotionTasks _sut;
        private Basket _basket;

        public BasketPromotionTasksShould()
        {
            PromotionTasks _promotionTasks = new PromotionTasks(PromotionList);
            _sut = new BasketPromotionTasks(_promotionTasks);
        }

        [Fact]
        public void ApplyPromotionIfBasketContentsExactlyMatchPromotionRequirements()
        {
            _basket = new Basket();
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");

            var result = _sut.CreatePromotedBasket(_basket);

            Assert.NotNull(result);
            Assert.Equal(130, result.Total);
        }
    }
}
