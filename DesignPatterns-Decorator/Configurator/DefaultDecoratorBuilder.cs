using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

                if (_serviceType == null)
                {
                }

                var serviceInstance = CreateInstance(provider, _serviceType);
                if (serviceInstance is IDisposable disposable && _decorators.Any())
                {
                    disposeRegister.Register(disposable);
                }

                if (_decorators != null)
                {
                    for (var i = _decorators.Count - 1; i >= 0; i--)
                    {
                        serviceInstance =
                            (TServiceInterface)CreateInstance(provider, _decorators[i],
                                serviceInstance);

                        if (serviceInstance is IDisposable disposableInner && i != 0)
                        {
                            disposeRegister.Register(disposableInner);
                        }
                    }
                }

                return (TServiceInterface)serviceInstance;
            }
            finally
            {
                @lock.Release();
            }
        }


        private object CreateInstance(IServiceProvider provider, Type type, params object[] parameters)
        {
            return ActivatorUtilities.CreateInstance(provider, type, parameters);
        }
    }


    public interface IDisposeRegister : IDisposable
    {
        void Register(IDisposable disposable);
    }


    public class DisposeRegister : IDisposeRegister
    {
        private readonly ConcurrentBag<IDisposable> _disposables;

        public DisposeRegister()
        {
            _disposables = new ConcurrentBag<IDisposable>();
        }

        public void Register(IDisposable flowComponent)
        {
            _disposables.Add(flowComponent);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var disposable in _disposables)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
