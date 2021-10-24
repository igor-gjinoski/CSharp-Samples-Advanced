
using DesignPatterns_Factory.Implementation.Shipping.Utils;
using System;
using System.Collections.Generic;

namespace DesignPatterns_Factory.Implementation.Shipping.Factories
{
    public abstract class BaseProviderFactory
    {
        protected IDictionary<string, Func<BaseProvider>> Providers = CreateMap();

        public abstract BaseProvider CreateShippingProvider(string country);

        public BaseProvider GetShippingProvider(string country)
        {
            var provider = CreateShippingProvider(country);
            return provider;
        }

        private static IDictionary<string, Func<BaseProvider>> CreateMap()
        {
            return new Dictionary<string, Func<BaseProvider>>()
            {
                { "Bulgaria", () => new BulgarianShippingProvider(CreateShippingCostCalculator(50)) },
                { "Norway", () => new NorwegianShippingProvider(CreateShippingCostCalculator(100)) },
            };

            ShippingCostCalculator CreateShippingCostCalculator(decimal internationalShippingFee)
                => new(internationalShippingFee);
        }
    }
}
