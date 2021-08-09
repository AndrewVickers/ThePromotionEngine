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
    public class PromotionTasksShould : TestBase
    {
        private PromotionTasks _sut;

        [Fact]
        public void ReturnAListOfAllPromotions()
        {
            _sut = new PromotionTasks(PromotionList);

            var result = _sut.GetAllPromotions();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void ReturnAPromotionOfAGivenPriority()
        {
            _sut = new PromotionTasks(PromotionList);

            var result = _sut.GetPromotionByPriority(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Priority);
        }

        [Fact]
        public void ReturnThePromotionsInPriorityOrder()
        {

            _sut = new PromotionTasks(PromotionList);

            AddExtraPromotions();

            var result = _sut.GetNextPromotion(0);
            Assert.Equal(Promotion1Priority, result.Priority);

            result = _sut.GetNextPromotion(result.Priority + 1);
            Assert.Equal(Promotion3Priority, result.Priority);

            result = _sut.GetNextPromotion(result.Priority + 1);
            Assert.Equal(Promotion2Priority, result.Priority);

            result = _sut.GetNextPromotion(result.Priority + 1);
            Assert.Equal(Promotion5Priority, result.Priority);

            result = _sut.GetNextPromotion(result.Priority + 1);
            Assert.Equal(Promotion4Priority, result.Priority);

            result = _sut.GetNextPromotion(result.Priority + 1);
            Assert.Null(result);
        }

        [Fact]
        public void ReturnNullIfPriorityNotFound()
        {
            _sut = new PromotionTasks(PromotionList);

            var result = _sut.GetPromotionByPriority(25);

            Assert.Null(result);
        }

        [Fact]
        public void ReturnTrueIfProductIsInPromotion()
        {
            _sut = new PromotionTasks(PromotionList);

            var result = _sut.IsProductInPromotion("A", Promotion1);
            Assert.True(result);
        }

        [Fact]
        public void ReturnFalseIfProductIsNotInPromotion()
        {
            _sut = new PromotionTasks(PromotionList);

            var result = _sut.IsProductInPromotion("B", Promotion1);
            Assert.False(result);
        }
    }
}

