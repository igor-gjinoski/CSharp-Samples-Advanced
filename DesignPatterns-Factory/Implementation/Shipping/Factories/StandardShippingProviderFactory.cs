using DesignPatterns_Factory.Implementation.Shipping.Utils;
using System;

namespace DesignPatterns_Factory.Implementation.Shipping.Factories
{
    public class StandardShippingProviderFactory : BaseProviderFactory
    {
        public override BaseProvider CreateShippingProvider(string country)
        {
            BaseProvider shippingProvider;

            if (country == "Bulgaria")
            {
                var costCalculator = new ShippingCostCalculator(internationalShippingFee: 50);
                shippingProvider = new BulgarianShippingProvider(costCalculator);
            }
            else if (country == "Norway")
            {
                var costCalculator = new ShippingCostCalculator(internationalShippingFee: 100);
                shippingProvider = new NorwegianShippingProvider(costCalculator);
            }
            else
            {
                throw new NotSupportedException("No provider found!");
            }

            return shippingProvider;
        }
    }
}
