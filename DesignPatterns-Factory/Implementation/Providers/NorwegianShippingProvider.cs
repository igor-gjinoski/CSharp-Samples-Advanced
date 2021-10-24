using DesignPatterns_Factory.Implementation.Models;

namespace DesignPatterns_Factory.Implementation.Providers
{
    public class NorwegianShippingProvider : BaseProvider
    {
        public override string GenerateShippingLabelFor(Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}
