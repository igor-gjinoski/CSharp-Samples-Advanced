using DesignPatterns_Factory.Implementation.Shipping.Providers;
using System;

namespace DesignPatterns_Factory.Implementation.FactoryMethodPattern
{
    public class StandardShippingProviderFactory : BaseProviderFactory, IProviderFactory
    {
        public BaseProvider CreateShippingProvider(string country)
        {
            Providers.TryGetValue(country, out var provider);
            if (provider is null)
                throw new NotSupportedException("No provider found!");

            return provider();
        }
    }
}
