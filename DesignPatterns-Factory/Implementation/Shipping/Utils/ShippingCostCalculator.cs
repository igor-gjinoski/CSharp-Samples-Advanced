
namespace DesignPatterns_Factory.Implementation.Shipping.Utils
{
    public class ShippingCostCalculator
    {
        private readonly decimal _internationalShippingFee;

        public ShippingCostCalculator(decimal internationalShippingFee)
        {
            _internationalShippingFee = internationalShippingFee;
        }

        public decimal CalculateFor(
            string destinationCountry,
            string originCountry)
        {
            decimal total = 10m; // Default shipping cost $10

            if (destinationCountry != originCountry)
            {
                total += _internationalShippingFee;
            }
            return total;
        }
    }
}
