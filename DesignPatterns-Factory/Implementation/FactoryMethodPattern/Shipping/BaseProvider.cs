using DesignPatterns_Factory.Implementation.Models;
using DesignPatterns_Factory.Implementation.Shipping.Utils;

namespace DesignPatterns_Factory.Implementation.Shipping
{
    public abstract class BaseProvider
    {
        protected ShippingCostCalculator ShippingCostCalculator { get; set; }

        public abstract string GenerateShippingLabelFor(Order order);
    } 
}
