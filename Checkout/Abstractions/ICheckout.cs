using Checkout.Models;

namespace Checkout.Abstractions
{
    public interface ICheckout
    {
        Task ScanAsync(
            string productSku);

        List<Product> Scanned { get; set; }

        int Total
            => Scanned
                .Select(p => p.Price)
                .Sum();
    }
}
