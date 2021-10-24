using DesignPatterns_Factory.Implementation.Models;
using DesignPatterns_Factory.Implementation.Utils;

namespace DesignPatterns_Factory.Implementation.Shipping.Providers
{
    public abstract class BaseProvider
    {
        protected ShippingCostCalculator ShippingCostCalculator { get; set; }

        public abstract string GenerateShippingLabelFor(Order order);
    } 
}
