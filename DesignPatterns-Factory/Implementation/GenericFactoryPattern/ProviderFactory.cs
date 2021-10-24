using DesignPatterns_Factory.Implementation.Shipping.Providers;
using System;

namespace DesignPatterns_Factory.Implementation.GenericFactoryPattern
{
    public static class ProviderFactory
    {
        public static BaseProvider Create<TProvider>()
            where TProvider : IGenericFactory, new()
        {
            try
            {
                var provider = new TProvider();
                return provider.CreateShippingProvider();
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
        }
    }
}
