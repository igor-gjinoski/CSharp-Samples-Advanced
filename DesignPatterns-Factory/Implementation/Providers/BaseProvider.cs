using DesignPatterns_Factory.Implementation.Models;
using DesignPatterns_Factory.Implementation.Providers.Utils;

namespace DesignPatterns_Factory.Implementation
{
    public abstract class BaseProvider
    {
        public ShippingCostCalculator ShippingCostCalculator { get; protected set; }

        public abstract string GenerateShippingLabelFor(Order order);
    } 
}
