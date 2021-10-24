using DesignPatterns_Factory.Implementation;
using DesignPatterns_Factory.Implementation.Models;

namespace DesignPatterns_Factory
{
    class Program
    {
        static void Main(string[] args)
        {
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

            var cart = new Factory(order);
            var shipping = cart.Finalize();
        }
    }
}
