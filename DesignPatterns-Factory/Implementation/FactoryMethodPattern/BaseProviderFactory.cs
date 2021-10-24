using DesignPatterns_Factory.Implementation.Shipping.Providers;
using DesignPatterns_Factory.Implementation.Utils;
using System;
using System.Collections.Generic;

namespace DesignPatterns_Factory.Implementation.FactoryMethodPattern
{
    public abstract class BaseProviderFactory
    {
        protected IDictionary<string, Func<BaseProvider>> Providers = CreateMap();

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
