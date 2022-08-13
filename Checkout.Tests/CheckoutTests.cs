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
                Price = _productCatalogue.Single(p => p.Sku == productSku).Price
            };

            // Act
            _checkout.Scan(productSku);

            // Assert
            Assert.Contains(expected, _checkout.Scanned);
        }
    }
}