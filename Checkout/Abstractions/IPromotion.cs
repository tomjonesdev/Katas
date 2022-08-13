namespace Checkout.Abstractions
{
    public interface IPromotion
    {
        string Name { get; set; }

        string ProductSku { get; set; }

        int Price { get; set; }
    }
}
