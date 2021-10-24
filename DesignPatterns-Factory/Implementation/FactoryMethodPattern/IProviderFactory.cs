using DesignPatterns_Factory.Implementation.Shipping.Providers;

namespace DesignPatterns_Factory.Implementation.FactoryMethodPattern
{
    public interface IProviderFactory
    {
        BaseProvider CreateShippingProvider(string country);
    }
}
