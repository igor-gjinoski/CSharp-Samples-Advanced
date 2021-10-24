using DesignPatterns_Factory.Implementation.Shipping.Providers;
using DesignPatterns_Factory.Implementation.Utils;

namespace DesignPatterns_Factory.Implementation.GenericFactoryPattern
{
    public class BulgarianProvider : IGenericFactory
    {
        public BaseProvider CreateShippingProvider()
        {
            return new BulgarianShippingProvider(CreateShippingCostCalculator(50));
        }

        ShippingCostCalculator CreateShippingCostCalculator(decimal internationalShippingFee)
            => new(internationalShippingFee);
    }
}
