using DesignPatterns_Factory.Implementation;
using DesignPatterns_Factory.Implementation.Models;
using DesignPatterns_Factory.Implementation.Shipping.Factories;

namespace DesignPatterns_Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            #region PLACE ORDER
            var senderCountry = "Bulgaria";
            var recipientCountry = "Norway";

            var order = new Order
            {
                Sender = new Address
                {
                    To = "Sender name",
                    Country = senderCountry
                },

                Recipient = new Address
                {
                    To = "Recipient name",
                    Country = recipientCountry
                }
            };

            var item = new Item("Id", "ItemName", 100m);
            order.Items.Add(item, 1);
            #endregion

            var shippingProviderFactory = new StandardShippingProviderFactory();
            var cart = new ShoppingCart(order, shippingProviderFactory);
            var shipping = cart.Finalize();
        }
    }
}
