using Checkout.Models;

namespace Checkout.Abstractions
{
    public interface ICheckout
    {
        bool Scan(
            string productSku);

        List<Product> Basket { get; set; }

        int Total
            => Basket
                .Select(p => p.Price)
                .Sum();
    }
}
