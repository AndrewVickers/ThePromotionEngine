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
    public class PromotionTasksShould
    {
        private PromotionTasks _sut;

        private List<Promotion> _promotionList;

        private Promotion _promotion1;
        private Promotion _promotion2;
        private Promotion _promotion3;

        private Basket _basket;

        public PromotionTasksShould()
        {
            KeyValuePair<string, decimal>[] productModifiers = { new("A", 1), new("A", 1), new("A", 0.6M) };
            _promotion1 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "Multi A",
                Priority = 1
            };

            productModifiers = new KeyValuePair<string, decimal>[] { new("B", 1), new("B", 0.5M) };
            _promotion2 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "Buy One Get One Half Price",
                Priority = 2,

            };

            productModifiers = new KeyValuePair<string, decimal>[] { new("C", 1), new("D", 1) };
            _promotion3 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "MultiBuy",
                Priority = 3,
                Total = 30
            };

            _promotionList = new List<Promotion>
            {
                _promotion1, _promotion2, _promotion3
            };
        }

        [Fact]
        public void ReturnAListOfAllPromotions()
        {
            _sut = new PromotionTasks(_promotionList);

            var result = _sut.GetAllPromotions();

            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void ReturnAPromotionOfAGivenPriority()
        {
            _sut = new PromotionTasks(_promotionList);

            var result = _sut.GetPromotionWithPriority(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Priority);
        }

        [Fact]
        public void ReturnThePromotionsInPriorityOrder()
        {

            var promotion10 = new Promotion
            {
                Id = 10,
                ItemPriceModfier = new KeyValuePair<string, decimal>[] { },
                Name = "Should be last",
                Priority = 10,
                Total = 20
            };

            var promotion5 = new Promotion
            {
                Id = 5,
                ItemPriceModfier = new KeyValuePair<string, decimal>[] { },
                Name = "Should be 4 in list",
                Priority = 5,
                Total = 40
            };
        }
    }
}

