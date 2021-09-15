using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DesignPatterns.Configurator
{
    public class DefaultDecoratorBuilder<TServiceInterface> : 
        IServiceDecoratorConfigurator<TServiceInterface>, 
        IServiceDecoratorBuilder<TServiceInterface>
    {
        private Type _serviceType;
        private IList<Type> _decorators;

        public IServiceDecoratorConfigurator<TServiceInterface> AddDecorator<TDecorator>()
            where TDecorator : TServiceInterface
        {
            if (_decorators == null)
                _decorators = new List<Type>();

            _decorators.Add(typeof(TDecorator));
            return this;
        }


        public void AddService<TService>()
            where TService : TServiceInterface
        {
            _serviceType = typeof(TService);
        }


        public TServiceInterface Build(IServiceProvider provider)
        {
            
            // Validate _serviceType is not null
            if (_serviceType == null)
            {
                // throw exception
            }

            var serviceInstance = CreateInstance(provider, _serviceType);

            if (_decorators != null)
            {
                for (var i = _decorators.Count - 1; i >= 0; i--)
                {
                    serviceInstance =
                        (TServiceInterface)CreateInstance(provider, _decorators[i], serviceInstance);
                }
            }

            return (TServiceInterface)serviceInstance;
            
        }


        private object CreateInstance(IServiceProvider provider, Type type, params object[] parameters)
        {
            return ActivatorUtilities.CreateInstance(provider, type, parameters);
        }
    }
}
