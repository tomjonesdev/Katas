using Checkout.Abstractions;

namespace Checkout.Models
{
    public class MultiBuyPromotion
        : IPromotion
    {
        public string Name { get; set; }
            = string.Empty;

        public string ProductSku { get; set; }
            = string.Empty;

        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}
