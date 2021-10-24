using DesignPatterns_Factory.Implementation;
using DesignPatterns_Factory.Implementation.Abstractions;
using DesignPatterns_Factory.Implementation.Models;
using DesignPatterns_Factory.Implementation.Shipping.Factories;
using System;

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
            ProductFactory.Create<ConcreteFactory>();
        }
    }


    public static class ProductFactory
    {
        public static T Create<T>()
            where T : Factory, new()
        {
            try
            {
                var obj = new T();
                obj.PostConstruction();
                return obj;
            }
            catch (Exception)
            {
                return default;
            }
        }
    }

    public abstract class Factory
    {
        protected internal abstract void PostConstruction();
    }

    public class ConcreteFactory : Factory
    {
        protected internal override void PostConstruction()
        {
            Console.WriteLine($"PostConstruction from: {nameof(ConcreteFactory)}");
        }
    }
}
