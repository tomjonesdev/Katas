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

        public List<Product> Basket { get; set; }
            = new();

        public bool Scan(
            string productSku)
        {
            if (string.IsNullOrWhiteSpace(productSku))
            {
                return false;
            }

            var product = _productCatalogue
                .FirstOrDefault(p => p.Sku == productSku);

            if (product is null)
            {
                return false;
            }

            Basket.Add(product);

            return true;
        }
    }
}