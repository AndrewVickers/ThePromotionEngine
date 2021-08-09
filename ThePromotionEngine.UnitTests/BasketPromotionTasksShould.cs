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
        private readonly IList<Product> _productList;

        public BasketPromotionTasksShould()
        {
            _productList = new List<Product>
            {
                new(50, "A"),
                new(30, "B"),
                new(20, "C"),
                new(15, "D")
            };
            PromotionTasks _promotionTasks = new PromotionTasks(PromotionList);
            _sut = new BasketPromotionTasks(_promotionTasks, _productList);
        }

        [Fact]
        public void ApplyPromotionIfBasketContentsExactlyMatchPromotionRequirements()
        {
            _basket = new Basket();
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");

            var result = _sut.CreatePromotedBasketItems(_basket);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
        }
    }
}
