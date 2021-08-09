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

        private int _promotion1Priority = 1;
        private int _promotion2Priority = 3;
        private int _promotion3Priority = 2;
        private int _promotion4Priority = 10;
        private int _promotion5Priority = 7;

        public PromotionTasksShould()
        {
            KeyValuePair<string, decimal>[] productModifiers = { new("A", 1), new("A", 1), new("A", 0.6M) };
            _promotion1 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "Multi A",
                Priority = _promotion1Priority
            };

            productModifiers = new KeyValuePair<string, decimal>[] { new("B", 1), new("B", 0.5M) };
            _promotion2 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "Buy One Get One Half Price",
                Priority = _promotion2Priority,

            };

            productModifiers = new KeyValuePair<string, decimal>[] { new("C", 1), new("D", 1) };
            _promotion3 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "MultiBuy",
                Priority = _promotion3Priority,
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

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void ReturnAPromotionOfAGivenPriority()
        {
            _sut = new PromotionTasks(_promotionList);

            var result = _sut.GetPromotionByPriority(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Priority);
        }

        [Fact]
        public void ReturnThePromotionsInPriorityOrder()
        {
            _promotionList.Add(new Promotion
            {
                Id = 4,
                ItemPriceModfier = new KeyValuePair<string, decimal>[] { },
                Name = "Should be last",
                Priority = _promotion4Priority,
                Total = 20
            });

            _promotionList.Add(new Promotion
            {
                Id = 5,
                ItemPriceModfier = new KeyValuePair<string, decimal>[] { },
                Name = "Should be 4 in list",
                Priority = _promotion5Priority,
                Total = 40
            });

            _sut = new PromotionTasks(_promotionList);

            var result = _sut.GetNextPromotion();
            Assert.Equal(_promotion1Priority, result.Priority);

            result = _sut.GetNextPromotion();
            Assert.Equal(_promotion3Priority, result.Priority);

            result = _sut.GetNextPromotion();
            Assert.Equal(_promotion2Priority, result.Priority);

            result = _sut.GetNextPromotion();
            Assert.Equal(_promotion5Priority, result.Priority);

            result = _sut.GetNextPromotion();
            Assert.Equal(_promotion4Priority, result.Priority);

            result = _sut.GetNextPromotion();
            Assert.Null(result);
        }

        [Fact]
        public void ReturnNullIfPriorityNotFound()
        {
            _sut = new PromotionTasks(_promotionList);

            var result = _sut.GetPromotionByPriority(25);

            Assert.Null(result);
        }
    }
}

