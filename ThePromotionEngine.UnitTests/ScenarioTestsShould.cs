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
        public List<Promotion> ScenarioPromotionList;
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

            ScenarioPromotionList = new List<Promotion>
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
            PromotionTasks _promotionTasks = new PromotionTasks(ScenarioPromotionList);
            _sut = new BasketPromotionTasks(_promotionTasks, _productList);
        }

        [Fact]
        public void CalculateScenarioACorrectly()
        {
            _basket = new Basket();
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("B");
            _basket.AddProductToBasket("C");

            var result = _sut.CreatePromotedBasketItems(_basket);

            var total = 0M;
            foreach (var promotedBasketItem in result)
            {
                total += promotedBasketItem.Total;
            }
            Assert.Equal(3, result.Count);
            Assert.Equal(50, result[0].Total);
            Assert.Equal(30, result[1].Total);
            Assert.Equal(20, result[2].Total);
            Assert.Equal(100, total);
        }


        [Fact]
        public void CalculateScenarioBCorrectly()
        {
            _basket = new Basket();
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("A");
            _basket.AddProductToBasket("B");
            _basket.AddProductToBasket("B");
            _basket.AddProductToBasket("B");
            _basket.AddProductToBasket("B");
            _basket.AddProductToBasket("B");
            _basket.AddProductToBasket("C");

            var result = _sut.CreatePromotedBasketItems(_basket);

            var total = 0M;
            foreach (var promotedBasketItem in result)
            {
                total += promotedBasketItem.Total;
            }
            Assert.Equal(6, result.Count);
            Assert.Equal(130, result[0].Total);
            Assert.Equal(45, result[1].Total);
            Assert.Equal(45, result[2].Total);
            Assert.Equal(100, result[3].Total);
            Assert.Equal(30, result[4].Total);
            Assert.Equal(20, result[5].Total);
            Assert.Equal(370, total);
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

            var total = 0M;
            foreach (var promotedBasketItem in result)
            {
                total += promotedBasketItem.Total;
            }
            Assert.Equal(5, result.Count);
            Assert.Equal(130, result[0].Total);
            Assert.Equal(45, result[1].Total);
            Assert.Equal(45, result[2].Total);
            Assert.Equal(30, result[3].Total);
            Assert.Equal(30, result[4].Total);
            Assert.Equal(280, total);
        }

        [Fact]
        public void CalculateScenarioXCorrectly()
        {
            _basket = new Basket();
            _basket.AddProductToBasket("C");
            _basket.AddProductToBasket("C");
            _basket.AddProductToBasket("C");
            _basket.AddProductToBasket("C");
            _basket.AddProductToBasket("D");
            _basket.AddProductToBasket("D");
            _basket.AddProductToBasket("D");
            _basket.AddProductToBasket("D");
            _basket.AddProductToBasket("D");

            var result = _sut.CreatePromotedBasketItems(_basket);

            var total = 0M;
            foreach (var promotedBasketItem in result)
            {
                total += promotedBasketItem.Total;
            }
            Assert.Equal(5, result.Count);
            Assert.Equal(30, result[0].Total);
            Assert.Equal(30, result[1].Total);
            Assert.Equal(30, result[2].Total);
            Assert.Equal(30, result[3].Total);
            Assert.Equal(15, result[4].Total);
            Assert.Equal(135, total);
        }

    }
}
