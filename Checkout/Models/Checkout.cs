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

        public int Total
        {
            get
            {
                var basketTotal = Basket.Sum(p => p.Price);
                var basketPromotionSkus = Basket
                    .Where(p => _promotions.Any(mbp => mbp.ProductSku == p.Sku))
                    .Distinct()
                    .Select(bps => bps.Sku)
                    .ToList();

                foreach (var sku in basketPromotionSkus)
                {
                    var initialPrice = _productCatalogue.Single(p => p.Sku == sku).Price;
                    var basketQuantity = Basket.Count(p => p.Sku == sku);
                    var promoQuantity = _promotions.Single(mbp => mbp.ProductSku == sku).Quantity;
                    var promoPrice = _promotions.Single(mbp => mbp.ProductSku == sku).Price;

                    var multiBuys = basketQuantity / promoQuantity;

                    for (var i = 0; i < multiBuys; i++)
                    {
                        basketTotal -= (initialPrice * promoQuantity);
                        basketTotal += promoPrice;
                    }
                }

                return basketTotal;
            }
        }

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