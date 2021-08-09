using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePromotionEngine.Library.Models;
using Xunit;

namespace ThePromotionEngine.UnitTests
{
    public class BasketTestsShould
    {
        private IBasket _sut;
        private string Item1 = "Item 1";
        private string Item2 = "Item 2";

        [Fact]
        public void CreateAnEmptyBasket()
        {
            _sut = new Basket();
            var result = _sut.GetBasket();
            Assert.Empty(result);
        }

        [Fact]
        public void AddAnItemToAnEmptyBasket()
        {
            _sut = new Basket();

            _sut.AddProductToBasket(Item1);
            var result = _sut.GetBasketForItem(Item1);
            Assert.Single(result);

        }

        [Fact]
        public void IncrementItemCountForMultipleItemsInABasket()
        {
            var itemCount = 4;
            _sut = new Basket();

            for (int addItemCount = 0; addItemCount < itemCount; addItemCount++)
            {
                _sut.AddProductToBasket(Item1);
            }

            var result = _sut.GetBasketForItem(Item1);

            Assert.Single(result);
            Assert.Equal(itemCount, result.First().Value);
        }

        [Fact]
        public void AddMultipleDifferentItemsToBasket()
        {
            var item1Count = 3;
            var item2Count = 0;
            _sut = new Basket();

            for (int addItemCount = 0; addItemCount < item1Count; addItemCount++)
            {
                _sut.AddProductToBasket(Item1);
                if (addItemCount % 2 == 0)
                {
                    _sut.AddProductToBasket(Item2);
                    item2Count++;
                }
            }

            var result = _sut.GetBasket();

            Assert.Equal(2, result.Count);
            Assert.Equal(item1Count, result[Item1]);
            Assert.Equal(item2Count, result[Item2]);

        }
    }
}
