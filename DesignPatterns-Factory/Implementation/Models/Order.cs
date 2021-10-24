using DesignPatterns_Factory.Implementation.Shipping.Utils;
using System.Collections.Generic;

namespace DesignPatterns_Factory.Implementation.Models
{
    public class Order
    {
        public Dictionary<Item, int> Items { get; } = new Dictionary<Item, int>();

        public ShippingStatus OrderStatus { get; set; } = ShippingStatus.Waiting;

        public Address Sender { get; set; }

        public Address Recipient { get; set; }
    }

    public class Address
    {
        public string To { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
