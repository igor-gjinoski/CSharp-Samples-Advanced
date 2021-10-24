using DesignPatterns_Factory.Implementation.Shipping.Providers;

namespace DesignPatterns_Factory.Implementation.GenericFactoryPattern
{
    public interface IGenericFactory
    {
        BaseProvider CreateShippingProvider();
    }
}
