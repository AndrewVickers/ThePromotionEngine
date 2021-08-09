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
    public class ScenarioTestsShould : TestBase
    {
        private BasketPromotionTasks _sut;
        private Basket _basket;
        private readonly IList<Product> _productList;
        public List<Promotion> PromotionList;
        public Promotion Promotion1;
        public Promotion Promotion2;
        public Promotion Promotion3;

        public ScenarioTestsShould()
        {
            IList<KeyValuePair<string, decimal>> productModifiers = new List<KeyValuePair<string, decimal>>() { new("A", 1), new("A", 1), new("A", 0.6M) };
            Promotion1 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "Multi A",
                Priority = 1
            };

            productModifiers = new List<KeyValuePair<string, decimal>>() { new("B", 1), new("B", 0.5M) };
            Promotion2 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "Buy One Get One Half Price",
                Priority = 2,

            };

            productModifiers = new List<KeyValuePair<string, decimal>>() { new("C", 1), new("D", 1) };
            Promotion3 = new Promotion
            {
                Id = 1,
                ItemPriceModfier = productModifiers,
                Name = "MultiBuy",
                Priority = 3,
                Total = 30
            };

            PromotionList = new List<Promotion>
            {
                Promotion1, Promotion2, Promotion3
            };
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
        public void CalculateScenarioACorrectly()
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
                    Name = _productList[0].SKU,
                    Price = _productList[0].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[1].SKU,
                    Price = _productList[1].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[2].SKU,
                    Price = _productList[2].Price
                }
            };

            var result = _sut.CalculateTotal(basket.ProductList);

            Assert.Equal(100, result);
        }

        [Fact]
        public void CalculateScenarioBCorrectly()
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
                    Name = _productList[0].SKU,
                    Price = _productList[0].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[0].SKU,
                    Price = _productList[0].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[0].SKU,
                    Price = _productList[0].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[0].SKU,
                    Price = _productList[0].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[0].SKU,
                    Price = _productList[0].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[1].SKU,
                    Price = _productList[1].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[1].SKU,
                    Price = _productList[1].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[1].SKU,
                    Price = _productList[1].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[1].SKU,
                    Price = _productList[1].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[1].SKU,
                    Price = _productList[1].Price
                },
                new PromotedBasketItem.PromotedProduct
                {
                    Matched = false,
                    Modifier = 1,
                    Name = _productList[2].SKU,
                    Price = _productList[2].Price
                }
            };

            var result = _sut.CalculateTotal(basket.ProductList);

            Assert.Equal(370, result);
        }

        [Fact]
        public void CalculateScenarioCCorrectly()
        {
            _basket = new Basket();
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("B");
            _basket.AddProductToBasket("B");
            _basket.AddProductToBasket("B");
            _basket.AddProductToBasket("B");
            _basket.AddProductToBasket("B");
            _basket.AddProductToBasket("C");
            _basket.AddProductToBasket("D");

            var result = _sut.CreatePromotedBasketItems(_basket);

            Assert.Equal(5, result.Count);
            Assert.Equal(130, result[0].Total);
            Assert.Equal(45, result[1].Total);
            Assert.Equal(45, result[2].Total);
            Assert.Equal(30, result[3].Total);
            Assert.Equal(30, result[4].Total);
        }

    }
}
