using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using DesignPatterns.DisposableRegister;

namespace DesignPatterns.Configurator
{
    public class DefaultDecoratorBuilder<TServiceInterface> : 
        IServiceDecoratorConfigurator<TServiceInterface>, 
        IServiceDecoratorBuilder<TServiceInterface>
    {
        private Type _serviceType;
        private IList<Type> _decorators;

        private static readonly ConcurrentDictionary<Type, SemaphoreSlim> _lockService =
            new ConcurrentDictionary<Type, SemaphoreSlim>();

        private static readonly object BuildLock = new object();

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
            if (!_lockService.TryGetValue(_serviceType, out var @lock))
            {
                lock (BuildLock)
                {
                    if (!_lockService.TryGetValue(_serviceType, out @lock))
                    {
                        @lock = new SemaphoreSlim(1, 1);
                        _lockService.TryAdd(_serviceType, @lock);
                    }
                }
            }

            try
            {
                var disposeRegister = provider.GetService<IDisposeRegister>();

                @lock.Wait();

                if (_serviceType is null) // throw exception
                {
                }

                var serviceInstance = CreateInstance(provider, _serviceType);
                if (serviceInstance is IDisposable disposableService && _decorators.Any())
                {
                    disposeRegister.Register(disposableService);
                }

                if (_decorators is not null)
                {
                    for (var index = _decorators.Count - 1; index >= 0; index--)
                    {
                        serviceInstance =
                            (TServiceInterface)CreateInstance(provider, _decorators[index], serviceInstance);

                        if (serviceInstance is IDisposable disposableDecorator && index != 0)
                        {
                            disposeRegister.Register(disposableDecorator);
                        }
                    }
                }

                object CreateInstance(IServiceProvider provider, Type type, params object[] parameters)
                    =>
                    ActivatorUtilities.CreateInstance(provider, type, parameters);
                

                return (TServiceInterface)serviceInstance;
            }
            finally
            {
                @lock.Release();
            }
        }
    }
}
