using DesignPatterns_Factory.Implementation.FactoryMethodPattern;
using DesignPatterns_Factory.Implementation.Models;
using DesignPatterns_Factory.Implementation.Utils;

namespace DesignPatterns_Factory.Implementation
{
    public class ShoppingCart
    {
        private readonly Order _order;
        private readonly IProviderFactory _shippingProviderFactory;

        public ShoppingCart(Order order, IProviderFactory shippingProviderFactory)
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
