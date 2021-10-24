using DesignPatterns_Factory.Implementation.Models;
using DesignPatterns_Factory.Implementation.Shipping.Utils;

namespace DesignPatterns_Factory.Implementation.Shipping
{
    public class NorwegianShippingProvider : BaseProvider
    {
        public NorwegianShippingProvider(
            ShippingCostCalculator shippingCostCalculator)
        {
            ShippingCostCalculator = shippingCostCalculator;
        }

        public override string GenerateShippingLabelFor(Order order)
        {
            var shippingCost = ShippingCostCalculator.CalculateFor(
                order.Recipient.Country,
                order.Sender.Country);

            return $"To: {order.Recipient.To}\n" +
                   $"Shipping Cost: {shippingCost}";
        }
    }
}
