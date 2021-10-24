using System;

namespace DesignPatterns_Factory.Implementation.Shipping.Factories
{
    public class StandardShippingProviderFactory : BaseProviderFactory
    {
        public override BaseProvider CreateShippingProvider(string country)
        {
            Providers.TryGetValue(country, out var provider);
            if (provider is null)
                throw new NotSupportedException("No provider found!");

            return provider();
        }
    }
}
