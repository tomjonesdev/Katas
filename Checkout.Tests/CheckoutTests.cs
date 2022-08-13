using Checkout.Abstractions;
using Checkout.Models;

namespace Checkout.Tests
{
    public class CheckoutTests
    {
        private readonly ICheckout _checkout;
        private readonly List<Product> _productCatalogue;
        private readonly List<MultiBuyPromotion> _promotions;

        public CheckoutTests()
        {
            _productCatalogue = new()
            {
                new(){ Sku = "A", Price = 50 },
                new(){ Sku = "B", Price = 30 },
                new(){ Sku = "C", Price = 20 },
                new(){ Sku = "D", Price = 15 },
            };

            _promotions = new()
            {
                new(){ Name = "3 for 130", ProductSku = "A", Quantity = 3, Price = 130 },
                new(){ Name = "2 for 45", ProductSku = "B", Quantity = 2, Price = 45 },
            };

            _checkout = new Models.Checkout(
                _productCatalogue,
                _promotions);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("B")]
        [InlineData("C")]
        [InlineData("D")]
        public void Scan_AddsProductToBasket(
            string productSku)
        {
            // Arrange
            var expected = new Product
            {
                Sku = productSku,
            };

            // Act
            var result = _checkout.Scan(productSku);

            // Assert
            Assert.True(result);
            Assert.Single(_checkout.Basket);
            Assert.Contains(expected.Sku, _checkout.Basket.First().Sku);
        }

        [Fact]
        public void Total_ReturnsCorrectTotal()
        {
            // Arrange
            var basket = new List<Product>
            {
                new() { Sku = "A" },
                new() { Sku = "B" },
                new() { Sku = "C" },
                new() { Sku = "D" },
            };

            // Act & Assert
            foreach (var product in basket)
            {
                var result = _checkout.Scan(product.Sku);
                Assert.True(result);
            }

            Assert.Equal(115, _checkout.Total);
        }

        [Fact]
        public void Total__Discounted_ReturnsCorrectTotal()
        {
            // Arrange
            var basket = new List<Product>
            {
                new() { Sku = "A" },
                new() { Sku = "B" },
                new() { Sku = "A" },
                new() { Sku = "A" },
                new() { Sku = "B" },
                new() { Sku = "C" },
                new() { Sku = "C" },
                new() { Sku = "D" },
                new() { Sku = "A" },
            };

            // Act & Assert
            foreach (var product in basket)
            {
                var result = _checkout.Scan(product.Sku);
                Assert.True(result);
            }

            Assert.Equal(280, _checkout.Total);
        }
    }
}