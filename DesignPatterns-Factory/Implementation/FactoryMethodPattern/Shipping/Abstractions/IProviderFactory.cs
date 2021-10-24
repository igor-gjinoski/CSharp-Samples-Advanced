using DesignPatterns_Factory.Implementation.Shipping;

namespace DesignPatterns_Factory.Implementation.Abstractions
{
    public interface IProviderFactory
    {
        BaseProvider CreateShippingProvider(string country);
    }
}
