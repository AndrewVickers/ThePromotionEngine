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

        [Fact]
        public void AppplyPromotionOnlyToItemsThatMatchPromotionRequirements()
        {
            _basket = new Basket();
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");

            var result = _sut.CreatePromotedBasketItems(_basket);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(230, result[0].Total + result[1].Total);
        }

        [Fact]
        public void CalculateThePromotionTotalManuallyForASinglePromotion()
        {
            var basket = new PromotedBasketItem();
            basket.Total = 0;
            basket.Id = 1;
            basket.ProductList = new List<PromotedBasketItem.PromotedProduct>
            {
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 0.6M,
                    Name = "A",
                    Price = 50
                },
            };

            var result = _sut.CalculateTotal(basket.ProductList);

            Assert.Equal(130, result);
        }

        [Fact]
        public void CalculateThePromotionTotalManuallyForTwoPromotions()
        {
            var basket = new PromotedBasketItem();
            basket.Total = 0;
            basket.Id = 1;
            basket.ProductList = new List<PromotedBasketItem.PromotedProduct>
            {
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 0.6M,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 0.6M,
                    Name = "A",
                    Price = 50
                },
            };

            var result = _sut.CalculateTotal(basket.ProductList);

            Assert.Equal(260, result);
        }

        [Fact]
        public void CalculateThePromotionTotalManuallyForMultiplePromotions()
        {
            var basket = new PromotedBasketItem();
            basket.Total = 0;
            basket.Id = 1;
            basket.ProductList = new List<PromotedBasketItem.PromotedProduct>
            {
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 0.6M,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 0.6M,
                    Name = "A",
                    Price = 50
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = "B",
                    Price = 30
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 0.5M,
                    Name = "B",
                    Price = 30
                },
            };

            var result = _sut.CalculateTotal(basket.ProductList);

            Assert.Equal(305, result);
        }
    }
}
