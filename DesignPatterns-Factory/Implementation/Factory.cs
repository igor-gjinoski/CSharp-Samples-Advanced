using DesignPatterns_Factory.Implementation.Models;
using DesignPatterns_Factory.Implementation.Providers;
using DesignPatterns_Factory.Implementation.Providers.Utils;
using System;

namespace DesignPatterns_Factory.Implementation
{
    public class Factory
    {
        private readonly Order _order;

        public Factory(Order order)
            => _order = order;
        
        public string Finalize()
        {
            BaseProvider shippingProvider;

            if (_order.Sender.Country == "Bulgaria")
            {
                var costCalculator = new ShippingCostCalculator(internationalShippingFee: 50);
                var shippingCost = costCalculator.CalculateFor(
                    _order.Recipient.Country,
                    _order.Sender.Country);

                shippingProvider = new BulgarianShippingProvider();
            }
            else if (_order.Sender.Country == "Norway")
            {
                var costCalculator = new ShippingCostCalculator(internationalShippingFee: 100);
                var shippingCost = costCalculator.CalculateFor(
                    _order.Recipient.Country,
                    _order.Sender.Country);

                shippingProvider = new NorwegianShippingProvider();
            }
            else
            {
                throw new NotSupportedException("No provider found!");
            }

            _order.OrderStatus = ShippingStatus.Ready;
            return shippingProvider.GenerateShippingLabelFor(_order);
        }
    }
}
