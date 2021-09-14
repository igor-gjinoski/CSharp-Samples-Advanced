using System;
using System.Collections.Generic;

namespace DesignPatterns.Configurator
{
    public class DefaultDecoratorBuilder<TServiceInterface> : IServiceDecoratorConfigurator<TServiceInterface>
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
    }
}