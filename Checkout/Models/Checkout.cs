using Checkout.Abstractions;

namespace Checkout.Models
{
    public class Checkout
        : ICheckout
    {
        private readonly List<Product> _productCatalogue;
        private readonly List<MultiBuyPromotion> _promotions;

        public Checkout(
            List<Product> productCatalogue,
            List<MultiBuyPromotion> promotions)
        {
            _productCatalogue = productCatalogue;
            _promotions = promotions;
        }

        public List<Product> Scanned { get; set; }
            = new();

        public Task ScanAsync(
            string productSku)
        {
            throw new NotImplementedException();
        }
    }
}