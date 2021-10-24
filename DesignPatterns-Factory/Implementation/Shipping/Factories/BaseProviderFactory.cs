
namespace DesignPatterns_Factory.Implementation.Shipping.Factories
{
    public abstract class BaseProviderFactory
    {
        public abstract BaseProvider CreateShippingProvider(string country);

        public BaseProvider GetShippingProvider(string country)
        {
            var provider = CreateShippingProvider(country);
            return provider;
        }
    }
}
