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
        private string _item1 = "Item 1";
        private string _item2 = "Item 2";

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

            _sut.AddProductToBasket(_item1);
            var result = _sut.GetBasketForItem(_item1);
            Assert.Equal(1, result.Value);
            Assert.Equal(_item1, result.Key);

        }

        [Fact]
        public void UpdateAnExistingItemWithANewQuantity()
        {
            _sut = new Basket();
            _sut.AddProductToBasket(_item1);
        }

        [Fact]
        public void IncrementItemCountForMultipleItemsInABasket()
        {
            var itemCount = 4;
            _sut = new Basket();

            for (int addItemCount = 0; addItemCount < itemCount; addItemCount++)
            {
                _sut.AddProductToBasket(_item1);
            }

            var result = _sut.GetBasketForItem(_item1);

            Assert.Equal(_item1, result.Key);
            Assert.Equal(itemCount, result.Value);
        }

        [Fact]
        public void AddMultipleDifferentItemsToBasket()
        {
            var item1Count = 3;
            var item2Count = 0;
            _sut = new Basket();

            for (int addItemCount = 0; addItemCount < item1Count; addItemCount++)
            {
                _sut.AddProductToBasket(_item1);
                if (addItemCount % 2 == 0)
                {
                    _sut.AddProductToBasket(_item2);
                    item2Count++;
                }
            }

            var result = _sut.GetBasket();

            Assert.Equal(2, result.Count);
            Assert.Equal(item1Count, result[_item1]);
            Assert.Equal(item2Count, result[_item2]);

        }
    }
}
