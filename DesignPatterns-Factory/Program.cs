using DesignPatterns_Factory.Implementation;
using DesignPatterns_Factory.Implementation.FactoryMethodPattern;
using DesignPatterns_Factory.Implementation.GenericFactoryPattern;
using DesignPatterns_Factory.Implementation.Models;

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

            /* Factory Method Pattern */
            IProviderFactory providerFactory = new StandardShippingProviderFactory();
            var cart = new ShoppingCart(order, providerFactory);
            var shipping = cart.Finalize();


            /* Generic Factory Pattern */
            var provider = ProviderFactory.Create<BulgarianProvider>();
            var label = provider.GenerateShippingLabelFor(order);
        }
    }
}
