using DesignPatterns_Factory.Implementation.Models;
using DesignPatterns_Factory.Implementation.Shipping.Utils;
using DesignPatterns_Factory.Implementation.Shipping.Factories;

namespace DesignPatterns_Factory.Implementation
{
    public class ShoppingCart
    {
        private readonly Order _order;
        private readonly BaseProviderFactory _shippingProviderFactory;

        public ShoppingCart(Order order, BaseProviderFactory shippingProviderFactory)
        {
            _order = order;
            _shippingProviderFactory = shippingProviderFactory;
        }

        public string Finalize()
        {
            var shippingProvider = _shippingProviderFactory.CreateShippingProvider(_order.Sender.Country);
            _order.OrderStatus = ShippingStatus.Ready;

            return shippingProvider.GenerateShippingLabelFor(_order);
        }
    }
}
